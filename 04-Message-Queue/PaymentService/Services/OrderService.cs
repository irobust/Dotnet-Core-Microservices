using System.Collections.Generic;

namespace PaymentService{
    public class OrderService{
        private readonly IList<Order> Orders = new List<Order>();

        public void Add(Order Order){
            this.Orders.Add(Order);
        }

        public IEnumerable<Order> Get(){
            return this.Orders;
        }
    }
}