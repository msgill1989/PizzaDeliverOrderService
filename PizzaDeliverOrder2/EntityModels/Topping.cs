using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDeliverOrder2.Models
{
    public class Topping
    {
        public int Id { get; set; }
        public int ToppingName { get; set; }
        public decimal ToppingPrice { get; set; }
    }
}
