// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Mvc;
using AirBnBWebApi.Core.Entities;
using AirBnBWebApi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using AirBnBWebApi.Api.Helpers;

namespace AirBnBWebApi.Api.Controllers;
[Authorize()]
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var userList = await _userService.GetAllUsersAsync();

        return ResponseHelper.Success(userList);
    }

    // [HttpGet("{id}")]
    // public async Task<IActionResult> GetUserById(Guid id)
    // {
    // }

    // [HttpPost]
    // public async Task<IActionResult> CreateUser([FromBody] User user)
    // {
    // }
    // [HttpPut("{id}")]
    // public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User user)
    // {
    // }
    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeleteUser(Guid id)
    // {
    // }
}
