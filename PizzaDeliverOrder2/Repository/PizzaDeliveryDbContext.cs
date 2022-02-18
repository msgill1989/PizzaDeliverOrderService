using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PizzaDeliverOrder2.Models;
using Microsoft.Extensions.Options;
using PizzaDeliverOrder2.EntityModels;

namespace PizzaDeliverOrder2.Repository
{
    public class PizzaDeliveryDbContext : DbContext, IPizzaDeliveryDBContext
    {
        private readonly IOptions<DbOptions> dbOptions;
        public PizzaDeliveryDbContext()
        { }

        public PizzaDeliveryDbContext(DbContextOptions<PizzaDeliveryDbContext> options, IOptions<DbOptions> dbOptions = null)
            : base(options)
        {
            this.dbOptions = dbOptions;
        }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        public DbSet<PizzaCost> PizzaCost { get; set; }

        public DbSet<FinalOrders> FinalOrders { get; set; }

        public DatabaseFacade Db => Database;
    }
}
