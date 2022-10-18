using System;

namespace PaymentService
{
    public class Order
    {
        public DateTime OrderDate { get; set; }

        public Guid OrderID { get; set; }
    }
}