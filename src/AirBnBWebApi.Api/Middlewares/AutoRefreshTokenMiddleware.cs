// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AirBnBWebApi.Services.Services;

namespace AirBnBWebApi.Api.Middlewares;

public class AutoRefreshTokenMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JwtService _jwtService;
    private readonly AuthService _authService;

    public AutoRefreshTokenMiddleware(RequestDelegate next, JwtService jwtService, AuthService authService)
    {
        _next = next;
        _jwtService = jwtService;
        _authService = authService;
    }

    // public async Task Invoke(HttpContext context)
    // {
    //     // Lấy accessToken và refreshToken từ header
    //     var accessToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
    //     var refreshToken = context.Request.Headers["RefreshToken"].ToString();

    //     // Nếu cả accessToken và refreshToken đều có giá trị
    //     if (!string.IsNullOrEmpty(accessToken) && !string.IsNullOrEmpty(refreshToken))
    //     {
    //         // Kiểm tra accessToken đã hết hạn hay chưa
    //         if (_jwtService.IsTokenExpired(accessToken))
    //         {
    //             // Nếu accessToken hết hạn, kiểm tra refreshToken và tự động cấp lại accessToken mới
    //             (string newAccessToken, string newRefreshToken, bool isSuspicious) = await _authService.HandleTokenRefreshAsync(accessToken, refreshToken, GetIpAddress(context), GetUserAgent(context)).ConfigureAwait(false);

    //             if (!isSuspicious && newAccessToken != null)
    //             {
    //                 // Cập nhật token mới trong response header và tiếp tục request
    //                 context.Response.Headers["New-AccessToken"] = newAccessToken;
    //                 context.Response.Headers["New-RefreshToken"] = newRefreshToken;

    //                 // Lưu lại accessToken mới trong context
    //                 context.Request.Headers["Authorization"] = $"Bearer {newAccessToken}";
    //             }
    //             else
    //             {
    //                 // Hành vi bất thường hoặc refreshToken không hợp lệ
    //                 context.Response.StatusCode = StatusCodes.Status401Unauthorized;
    //                 await context.Response.WriteAsync("Suspicious activity detected or invalid token. Please re-login.").ConfigureAwait(false);
    //                 return;
    //             }
    //         }
    //     }

    //     // Tiếp tục xử lý request
    //     await _next(context).ConfigureAwait(false);
    // }

    // private static string? GetIpAddress(HttpContext context)
    // {
    //     return context.Connection.RemoteIpAddress?.ToString();
    // }

    // private static string GetUserAgent(HttpContext context)
    // {
    //     return context.Request.Headers["User-Agent"].ToString();
    // }
}
