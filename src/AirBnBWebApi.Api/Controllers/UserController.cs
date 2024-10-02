// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Mvc;
using AirBnBWebApi.Services.Services;
using AirBnBWebApi.Api.Helpers;

namespace AirBnBWebApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _userService.GetUserByIdAsync(id).ConfigureAwait(false);
        if (user == null)
        {
            return ResponseHelper.NotFound("User not found.");
        }

        var userDto = new
        {
            user.Id,
            user.FullName,
            user.Email,
            user.PhoneNumber,
            user.Avatar,
            user.IsHost
        };

        return ResponseHelper.Success(userDto, "User fetched successfully.");
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetAllUsersAsync().ConfigureAwait(false);
        return ResponseHelper.SuccessList(users, "Users fetched successfully.");
    }
}
