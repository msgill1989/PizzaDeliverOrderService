using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDeliverOrder2.EntityModels
{
    public class PizzaCost
    {
        public int PizzaId { get; set; }
        public string PizzaName { get; set; }
        public decimal PizzaPrice { get; set; }
    }
}
