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
        DbSet<OrderDetails> OrderDetails { get; set; }

        DbSet<PizzaCost> PizzaCost { get; set; }

        DbSet<FinalOrders> FinalOrders { get; set; }

        DatabaseFacade Db { get; }

        int SaveChanges();
    }
}
