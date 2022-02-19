using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDeliverOrder2.EntityModels
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }
        public int CustomerId {get; set; }
        public int OrderQuantity { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
