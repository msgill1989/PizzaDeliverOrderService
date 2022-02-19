using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDeliverOrder2.EntityModels
{
    public class PizzaDetails
    {
        public int Id { get; set; }
        public string PizzaName { get; set; }
        public string Ingredients { get; set; }
        public decimal PizzaCost { get; set; }
    }
}
