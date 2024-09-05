using System;
using System.Collections.Generic;

namespace AirBnBWebApi.Core.Entities
{
  public class Payment
  {
    public int Id { get; set; }
    public decimal Amount { get; set; }       // Số tiền thanh toán
    public DateTime PaymentDate { get; set; } // Ngày thanh toán

    // Quan hệ với Reservation và PaymentMethod
    public int ReservationId { get; set; }
    public Reservation Reservation { get; set; }

    public int PaymentMethodId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }

    // Thêm các trường về thời gian và trạng thái
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
  }
}
