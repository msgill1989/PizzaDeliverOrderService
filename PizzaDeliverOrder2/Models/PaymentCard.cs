using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDeliverOrder2.Models
{
    public class PaymentCard
    {
        public long CardNumber { get; set; }
        public string CustomerName { get; set; }
        public string ExpiryDate { get; set; }
    }
}
