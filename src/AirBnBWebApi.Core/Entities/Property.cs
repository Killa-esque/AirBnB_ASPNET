using System;
using System.Collections.Generic;
using AirBnBWebApi.Core.Enum;


namespace AirBnBWebApi.Core.Entities
{
  public class Property
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal PricePerNight { get; set; }

    public PropertyTypeEnum PropertyType { get; set; }   // Sử dụng enum PropertyTypeEnum

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

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }

    public int LocationId { get; set; }
    public Location Location { get; set; }

    public int HostId { get; set; }
    public User Host { get; set; }

    public ICollection<Reservation> Reservations { get; set; }
    public ICollection<Review> Reviews { get; set; }
  }
}
