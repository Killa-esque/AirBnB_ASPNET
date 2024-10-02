// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using AirBnBWebApi.Core.Enums;

namespace AirBnBWebApi.Core.Entities;

public class Property
{
    public int Id { get; set; }
    public string PropertyName { get; set; }
    public string PropertyDescription { get; set; }
    public decimal PropertyPricePerNight { get; set; }
    public int PropertyHostId { get; set; }
    public string PropertyThumbnailUrl { get; set; }
    public PropertyStatusEnum PropertyStatus { get; set; }
    public PropertyTypeEnum PropertyType { get; set; }
    public int Guests { get; set; }
    public int Bedrooms { get; set; }
    public int Beds { get; set; }
    public int Bathrooms { get; set; }
    public bool Wifi { get; set; }
    public bool AirConditioning { get; set; }
    public bool Kitchen { get; set; }
    public bool Parking { get; set; }
    public bool SwimmingPool { get; set; }
    public string ImageUrl { get; set; }
    public bool IsDraft { get; set; }
    public bool IsPublished { get; set; }
    public bool UnPublished { get; set; }
    public int LocationId { get; set; }
    public int HostId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
