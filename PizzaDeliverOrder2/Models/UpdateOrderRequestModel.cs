using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDeliverOrder2.Models
{
    public class UpdateOrderRequestModel
    {
        public int OrderId { get; set; }
        public List<Pizza> AddPizza { get; set; }
        public List<int> RemovePizza { get; set; }

        public List<Pizza> UpdatePizza { get; set; }
    }
}
