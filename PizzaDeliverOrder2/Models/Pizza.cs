using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDeliverOrder2.Models
{
    public class Pizza
    {
        public int Id { get; set; }
        public string PizzaName { get; set; }
        public string PizzaBase { get; set; }
        public string CheeseType { get; set; }
        public List<Topping> Toppings { get; set; }
        public string Seasoning { get; set; }
        public string OilUsed { get; set; }
        public string Pickle { get; set; }
    }
}
