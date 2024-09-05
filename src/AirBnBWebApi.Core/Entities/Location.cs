using System;
using System.Collections.Generic;

namespace AirBnBWebApi.Core.Entities
{
  public class Location
  {
    public int Id { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string ImageUrl { get; set; }

    // Relationship to Properties
    public ICollection<Property> Properties { get; set; }
  }
}
