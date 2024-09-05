using System;
using System.Collections.Generic;
using AirBnBWebApi.Core.Enums;

namespace AirBnBWebApi.Core.Entities
{
  public class Review
  {
    public int Id { get; set; }
    public RatingEnum Rating { get; set; }   // Sử dụng enum RatingEnum
    public string Comment { get; set; }
    public DateTime ReviewDate { get; set; }

    public int PropertyId { get; set; }
    public Property Property { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
  }
}
