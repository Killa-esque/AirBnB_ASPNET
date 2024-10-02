// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AirBnBWebApi.Core.Entities;

public class ApiPermission
{
    public int ApiPermissionId { get; set; }
    public string Permission { get; set; }
    public int ApiKeyId { get; set; }
    public virtual ApiKey ApiKey { get; set; }
}

