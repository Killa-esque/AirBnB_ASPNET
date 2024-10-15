// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using AirBnBWebApi.Services.Utils;
using System.Threading.Tasks;
using AirBnBWebApi.Core.Entities;
using AirBnBWebApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace AirBnBWebApi.Services.Services;

public class AuthService
{
    private readonly AirBnBDbContext _context;
    private readonly JwtService _jwtService;
    private readonly KeyTokenService _keyTokenService;

    public AuthService(AirBnBDbContext context, JwtService jwtService, KeyTokenService keyTokenService)
    {
        _context = context;
        _jwtService = jwtService;
        _keyTokenService = keyTokenService;
    }

    private static string GenerateRandomHexString(int length)
    {
        byte[] randomBytes = new byte[length];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }
        return BitConverter.ToString(randomBytes).Replace("-", "").ToLower();
    }

    public async Task<(bool status, string email, string FullName, string accessToken, string refreshToken, string message)> Register(string email, string fullName, string password, string phoneNumber)
    {
        var userExist = await _context.Users.AnyAsync(u => u.Email == email);
        if (userExist)
        {
            return (false, null, null, null, null, "Email is already in use.");
        }

        var user = new User
        {
            Id = Guid.NewGuid(), // Nếu Id là kiểu Guid, tạo Guid mới.
            Email = email,
            FullName = fullName,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            PhoneNumber = phoneNumber,
            IsHost = false,
            IsAdmin = false,
            isUser = true,
            IsDeleted = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        try
        {
            var newUser = _context.Users.Add(user);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                string privateKey = GenerateRandomHexString(64);
                string publicKey = GenerateRandomHexString(64);

                var (status, code, keyToken) = await _keyTokenService.CreateKeyTokenAsync(newUser.Entity.Id, publicKey, privateKey);

                if (!status && keyToken == null)
                {
                    return (false, null, null, null, null, "KeyToken creation failed.");
                }

                var (accessToken, refreshToken) = _jwtService.GenerateTokens(newUser.Entity.Id, newUser.Entity.Email, newUser.Entity.IsHost, newUser.Entity.IsAdmin, newUser.Entity.isUser, keyToken.PublicKey, keyToken.PrivateKey);

                return (true, user.Email, user.FullName, accessToken, refreshToken, "User registered successfully.");
            }
            else
            {
                return (false, null, null, null, null, "User registration failed. No changes were made.");
            }
        }
        catch (Exception ex)
        {
            return (false, null, null, null, null, $"An error occurred: {ex.Message}");
        }
    }

}
