using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDeliverOrder2.Models
{
    public class OrderDetails
    {
        public string UserEmailId { get; set; }
        public decimal OrderAmount { get; set; }
        public int OrderQuantity { get; set; }
        public List<Pizza> Pizzas { get; set; }
    }
}
