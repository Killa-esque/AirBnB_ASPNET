using System;
using System.Collections.Generic;

namespace AirBnBWebApi.Core.Entities
{
  public class Reservation
  {
    public int Id { get; set; }

    // Thông tin đặt phòng
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public int NumberOfGuests { get; set; }

    // Quan hệ với Property và User
    public int PropertyId { get; set; }
    public Property Property { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    // Thêm các trường về thời gian và trạng thái
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }

    // Quan hệ với Payment
    public Payment Payment { get; set; }
  }
}
