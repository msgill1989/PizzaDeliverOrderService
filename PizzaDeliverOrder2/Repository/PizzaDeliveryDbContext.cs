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

        public DbSet<PizzaDetails> PizzaDetails { get; set; }

        public DbSet<Orders> Orders { get; set; }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<Payment> Payment { get; set; }
        public DbSet<Topping> Topping { get; set; }

        public DbSet<OrderDetails> OrderDetails { get; set; }

        public DatabaseFacade Db => Database;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Orders>()
                .HasKey(b => b.Id)
                .HasName("PrimaryKey_OrderId");
        }
    }
}
