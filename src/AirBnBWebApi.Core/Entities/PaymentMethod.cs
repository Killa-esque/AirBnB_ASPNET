using System;
using System.Collections.Generic;
using AirBnBWebApi.Core.Enum;

namespace AirBnBWebApi.Core.Entities
{
  public class PaymentMethod
  {
    public int Id { get; set; }
    public PaymentMethodEnum Name { get; set; }   // Sử dụng enum PaymentMethodEnum

    public ICollection<Payment> Payments { get; set; }
  }
}
