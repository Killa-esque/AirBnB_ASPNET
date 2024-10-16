// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AirBnBWebApi.Api.DTOs;
using AirBnBWebApi.Api.Helpers;
using AirBnBWebApi.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirBnBWebApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{

    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
    {
        if (!ModelState.IsValid)
        {
            return ResponseHelper.BadRequest("Invalid Data", ModelState);
        }

        var (status, email, fullName, accessToken, refreshToken, message) = await _authService.Register(registerDto.Email, registerDto.FullName, registerDto.Password, registerDto.PhoneNumber);

        if (!status)
        {
            return ResponseHelper.BadRequest(message);
        }

        return ResponseHelper.Success(new
        {
            email,
            fullName,
            token = new
            {
                accessToken,
                refreshToken
            }
        }, message);
    }
}
