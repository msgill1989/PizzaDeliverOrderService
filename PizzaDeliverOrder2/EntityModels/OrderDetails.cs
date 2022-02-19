using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDeliverOrder2.EntityModels
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int PizzaId { get; set; }

        //Comma seperated ToppingIds
        public string Toppings { get; set; }
    }
}
