using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDeliverOrder2.EntityModels
{
    public class Customer
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
    }
}
