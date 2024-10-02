// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;

namespace AirBnBWebApi.Core.Entities;

public class KeyToken
{
    public int UserId { get; set; }                  // Primary key và Foreign key trỏ đến bảng User
    public string PublicKey { get; set; }            // Public key
    public string PrivateKey { get; set; }           // Private key
    public DateTime Timestamp { get; set; }          // Timestamp cho thời gian cập nhật

    // Navigation properties
    public virtual User User { get; set; }           // Liên kết với bảng User

    // Mối quan hệ 1-n với bảng RefreshTokenUsed
    public virtual ICollection<RefreshTokenUsed> RefreshTokensUsed { get; set; }
}
