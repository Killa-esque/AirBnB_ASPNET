// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

namespace AirBnBWebApi.Core.Entities;

public class RefreshTokenUsed
{
    public int UserId { get; set; }                  // Primary key và Foreign key trỏ đến User
    public string RefreshToken { get; set; }         // RefreshToken đã được sử dụng
    public DateTime UsedAt { get; set; }             // Thời gian sử dụng RefreshToken
    public virtual KeyToken KeyToken { get; set; }   // Liên kết với bảng KeyToken qua UserId
}
