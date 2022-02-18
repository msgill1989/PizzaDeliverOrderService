using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDeliverOrder2.EntityModels
{
    public class FinalOrders
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public int CustomerId {get; set; }
        public string OrderDetails { get; set; }
        public decimal OrderCost { get; set; }
    }
}
