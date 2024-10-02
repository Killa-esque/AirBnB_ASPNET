// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;

namespace AirBnBWebApi.Core.Entities;

public class ApiKey
{
    public int ApiKeyId { get; set; }
    public string ApiKeyString { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }

    // Define a collection of permissions for the API key
    public virtual ICollection<ApiPermission> ApiPermissions { get; set; }

    public int UserId { get; set; }
    public virtual User User { get; set; }
}

