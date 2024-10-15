// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirBnBWebApi.Core.Entities;
using AirBnBWebApi.Infrastructure.Data;
using AirBnBWebApi.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AirBnBWebApi.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly AirBnBDbContext _context;

    public UserRepository(AirBnBDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.Where(u => !u.IsDeleted).ToListAsync();
    }

    // public async Task<User> GetByIdAsync(int id)
    // {
    //     return await _context.Users.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
    // }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted);
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public Task<User> GetByIdAsync(Guid id)
    {
        throw new System.NotImplementedException();
    }

    public Task DeleteAsync(Guid id)
    {
        throw new System.NotImplementedException();
    }

    // public async Task DeleteAsync(int id)
    // {
    //     var user = await GetByIdAsync(id);
    //     if (user != null)
    //     {
    //         user.IsDeleted = true;
    //         _context.Users.Update(user);
    //         await _context.SaveChangesAsync();
    //     }
    // }
}

