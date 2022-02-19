using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PizzaDeliverOrder2.EntityModels;
using PizzaDeliverOrder2.Models;

namespace PizzaDeliverOrder2.Repository
{
    public interface IPizzaDeliveryDBContext
    {
        DbSet<PizzaDetails> PizzaDetails { get; set; }

        DbSet<Orders> Orders { get; set; }

        DbSet<Customer> Customer { get; set; }

        DbSet<Payment> Payment { get; set; }
        DbSet<OrderDetails> OrderDetails { get; set; }
        DbSet<Topping> Topping { get; set; }

        DatabaseFacade Db { get; }

        int SaveChanges();
    }
}
