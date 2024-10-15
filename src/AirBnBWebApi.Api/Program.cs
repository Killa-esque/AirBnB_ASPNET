// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using AirBnBWebApi.Infrastructure.Data;
using AirBnBWebApi.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using AirBnBWebApi.Infrastructure.Interfaces;
using AirBnBWebApi.Infrastructure.Repository;
using AirBnBWebApi.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using AirBnBWebApi.Api.Middlewares;
using AirBnBWebApi.Api.Indetity;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Đăng ký DbContext
builder.Services.AddDbContext<AirBnBDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Đăng ký Repository và Service
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<KeyTokenService>();

// JWT
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = config["JwtSettings:Issuer"],
        ValidAudience = config["Jwt:Audience"],
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
    };
    x.Events = new JwtBearerEvents
    {
        OnMessageReceived = async context =>
        {
            try
            {
                var authorizationHeader = context.Request.Headers["Authorization"].ToString();
                Console.WriteLine("Authorization Header: " + authorizationHeader);

                if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    context.Fail("No valid Authorization header found.");
                    return;
                }

                // Lấy token từ Authorization header và gán cho context.Token
                context.Token = authorizationHeader.Substring("Bearer ".Length).Trim();
                Console.WriteLine("Extracted Token: " + context.Token);

                var scopeFactory = context.HttpContext.RequestServices.GetRequiredService<IServiceScopeFactory>();
                using var scope = scopeFactory.CreateScope();

                Console.WriteLine("OnMessageReceived");

                var keyTokenService = scope.ServiceProvider.GetRequiredService<KeyTokenService>();

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(context.Token);

                var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier) ??
                                jwtToken.Claims.FirstOrDefault(c => c.Type == "sub");

                Console.WriteLine("userIdClaim: " + userIdClaim?.Value);

                if (userIdClaim == null)
                {
                    context.Fail("Invalid token: userId claim is missing.");
                    return;
                }

                if (!Guid.TryParse(userIdClaim.Value, out var userId))
                {
                    context.Fail("Invalid user ID in token.");
                    return;
                }

                Console.WriteLine("userId: " + userId);

                var (status, publicKey) = await keyTokenService.GetUserPublicKeyAsync(userId);

                Console.WriteLine("publicKey: " + publicKey);

                if (!status || string.IsNullOrEmpty(publicKey))
                {
                    context.Fail("Failed to retrieve valid public key for the user ID in token.");
                    return;
                }

                context.Options.TokenValidationParameters.IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(publicKey));

            }
            catch (Exception ex)
            {
                context.Fail($"Token validation failed due to an exception: {ex.Message}");
            }
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine("Token has been successfully validated.");
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("Authentication failed: " + context.Exception.Message);
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    options.AddPolicy(IdentityData.Policies.AdminPolicy, policy =>
    {
        policy.RequireClaim(IdentityData.Claims.Role, IdentityData.Roles.Admin);
    });
    options.AddPolicy(IdentityData.Policies.HostPolicy, policy =>
    {
        policy.RequireClaim(IdentityData.Claims.Role, IdentityData.Roles.Host);
    });
    options.AddPolicy(IdentityData.Policies.UserPolicy, policy =>
    {
        policy.RequireClaim(IdentityData.Claims.Role, IdentityData.Roles.User);
    });
});

// Thêm các dịch vụ cần thiết như Controllers và Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Cấu hình ứng dụng
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
