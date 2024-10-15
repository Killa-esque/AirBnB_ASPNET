// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Threading.Tasks;
using AirBnBWebApi.Core.Entities;
using AirBnBWebApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AirBnBWebApi.Services.Services;

public class KeyTokenService
{
    private readonly AirBnBDbContext _context;

    public KeyTokenService(AirBnBDbContext context)
    {
        _context = context;
    }

    // Create keyToken
    public async Task<(bool status, int code, KeyToken keyToken)> CreateKeyTokenAsync(Guid userId, string publicKey, string privateKey)
    {
        var token = new KeyToken
        {
            UserId = userId,
            PublicKey = publicKey,
            PrivateKey = privateKey,
            Timestamp = DateTime.UtcNow
        };

        try
        {
            await _context.KeyTokens.AddAsync(token);
            int result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return (true, 201, token);
            }
            else
            {
                return (false, 500, null);
            }
        }
        catch (Exception ex)
        {
            // Bắt ngoại lệ nếu có lỗi xảy ra và trả về mã trạng thái lỗi
            return (false, 500, null);
        }
    }
    public async Task<(bool status, string publicKey)> GetUserPublicKeyAsync(Guid userId)
    {
        var keyToken = await _context.KeyTokens.AsNoTracking().FirstOrDefaultAsync(k => k.UserId == userId);

        if (keyToken == null)
        {
            return (false, null);
        }

        return (true, keyToken.PublicKey);
    }
}

