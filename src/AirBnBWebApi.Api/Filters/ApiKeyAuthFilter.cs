// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Net;
using AirBnBWebApi.Api.Constants;
using AirBnBWebApi.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace AirBnBWebApi.Api.Filters;

public class ApiKeyAuthFilter : IAuthorizationFilter
{
    private readonly IConfiguration _configuration;
    public ApiKeyAuthFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        try
        {
            // Thiết lập Content-Type cho response là application/json
            context.HttpContext.Response.ContentType = "application/json";

            // Kiểm tra `X-API-KEY` có trong header hay không
            if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstant.ApiKeyHeaderName, out var extractedApiKey))
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                var response = new
                {
                    status = false,
                    statusCode = StatusCodes.Status401Unauthorized,
                    message = "API Key is missing",
                    dateTime = DateTime.UtcNow
                };

                context.Result = new JsonResult(response)
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };

                return;
            }

            var apiKeys = _configuration.GetSection(AuthConstant.ApiKeySectionName).Get<List<ApiKeyInfo>>();

            var apiKeyInfo = apiKeys.FirstOrDefault(x => x.Key == extractedApiKey);

            if (apiKeyInfo == null)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

                var response = new
                {
                    status = false,
                    statusCode = StatusCodes.Status401Unauthorized,
                    message = "Invalid API Key",
                    dateTime = DateTime.UtcNow
                };

                context.Result = new JsonResult(response)
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };

                return;
            }

            var requestPath = $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

            if (!apiKeyInfo.Permissions.Contains(requestPath))
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;

                var response = new
                {
                    status = false,
                    statusCode = StatusCodes.Status403Forbidden,
                    message = "You do not have permission to access this resource.",
                    dateTime = DateTime.UtcNow
                };

                context.Result = new JsonResult(response)
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };

                return;
            }
        }
        catch (Exception ex)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                status = false,
                statusCode = StatusCodes.Status500InternalServerError,
                message = $"An error occurred: {ex.Message}",
                dateTime = DateTime.UtcNow
            };

            context.Result = new JsonResult(response)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
