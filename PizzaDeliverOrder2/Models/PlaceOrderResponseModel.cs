using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDeliverOrder2.Models
{
    public class PlaceOrderResponseModel
    {
        public int orderId { get; set; }
        public decimal cost { get; set; }
        public string message { get; set; }
    }
}
