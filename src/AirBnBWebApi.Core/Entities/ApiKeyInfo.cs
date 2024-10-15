// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Collections.Generic;

namespace AirBnBWebApi.Core.Entities;

public class ApiKeyInfo
{
    public string Key { get; set; }
    public List<string> Permissions { get; set; }
    public List<string> Roles { get; set; }
}
