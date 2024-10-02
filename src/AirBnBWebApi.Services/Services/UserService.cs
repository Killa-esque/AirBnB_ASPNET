// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AirBnBWebApi.Core.Entities;
using AirBnBWebApi.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirBnBWebApi.Services.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync().ConfigureAwait(false);
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id).ConfigureAwait(false);
    }

    public async Task CreateUserAsync(User newUser)
    {
        await _userRepository.AddAsync(newUser).ConfigureAwait(false);
    }

    public async Task<bool> UpdateUserAsync(User updateUser)
    {
        return await _userRepository.UpdateAsync(updateUser).ConfigureAwait(false);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        return await _userRepository.DeleteAsync(id).ConfigureAwait(false);
    }
}
