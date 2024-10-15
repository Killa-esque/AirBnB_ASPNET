// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Net;
using AirBnBWebApi.Api.Constants;
using AirBnBWebApi.Core.Entities;

namespace AirBnBWebApi.Api.Middlewares;

public class ApiKeyAuthMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    public ApiKeyAuthMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;

    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            context.Response.ContentType = "application/json";
            if (!context.Request.Headers.TryGetValue(AuthConstant.ApiKeyHeaderName, out var extractedApiKey))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                var response = new
                {
                    statusCode = StatusCodes.Status401Unauthorized,
                    message = "API Key is missing",
                };

                Console.WriteLine("Response Object: " + System.Text.Json.JsonSerializer.Serialize(response));

                await context.Response.WriteAsJsonAsync(response).ConfigureAwait(false);
                return;
            }

            var apiKeys = _configuration.GetSection(AuthConstant.ApiKeySectionName).Get<List<ApiKeyInfo>>();

            var apiKeyInfo = apiKeys.FirstOrDefault(x => x.Key == extractedApiKey);

            if (apiKeyInfo == null)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                var response = new
                {
                    statusCode = StatusCodes.Status401Unauthorized,
                    message = "Invalid API Key",
                };

                Console.WriteLine("Response Object: " + System.Text.Json.JsonSerializer.Serialize(response));

                await context.Response.WriteAsJsonAsync(response).ConfigureAwait(false);
                return;
            }

            var requestPath = $"{context.Request.Method} {context.Request.Path}";

            if (!apiKeyInfo.Permissions.Contains(requestPath))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                var response = new
                {
                    statusCode = StatusCodes.Status403Forbidden,
                    message = "You do not have permission to access this resource.",
                };

                Console.WriteLine("Response Object: " + System.Text.Json.JsonSerializer.Serialize(response));

                await context.Response.WriteAsJsonAsync(response).ConfigureAwait(false);
                return;
            }

            await _next(context).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync($"An error occurred: {ex.Message}").ConfigureAwait(false);
        }
    }

}
