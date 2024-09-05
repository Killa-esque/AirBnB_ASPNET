using System;
using System.Collections.Generic;

namespace AirBnBWebApi.Core.Entities
{
  public class User
  {
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsAdmin { get; set; }  // True nếu người dùng là Admin
    public bool IsHost { get; set; }   // True nếu là Host, False nếu là Guest
    public string Avatar { get; set; }

    // Tự động cập nhật các trường ngày tháng và trạng thái
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }

    // Quan hệ: Một người dùng có thể có nhiều đặt phòng và nhiều property
    public ICollection<Reservation> Reservations { get; set; }
    public ICollection<Property> Properties { get; set; }
    public ICollection<Review> Reviews { get; set; }
  }
}
