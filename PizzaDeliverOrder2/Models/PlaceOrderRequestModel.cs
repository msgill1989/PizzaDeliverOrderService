using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDeliverOrder2.Models
{
    public class PlaceOrderRequestModel
    {
        public List<Pizza> Pizzas { get; set; }
        public int CustomerId { get; set; }
        public PaymentCard Payment { get; set; }
    }
}
