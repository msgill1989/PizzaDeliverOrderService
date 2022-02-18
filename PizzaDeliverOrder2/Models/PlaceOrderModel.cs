using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDeliverOrder2.Models
{
    public class PlaceOrderModel
    {
        public List<Pizza> Pizzas { get; set; }
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
        public int CustomerPhoneNumber { get; set; }

    }
}
