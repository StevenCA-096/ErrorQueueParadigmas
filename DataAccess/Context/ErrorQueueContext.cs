using DataAccess.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class ErrorQueueContext : DbContext
    {
        public ErrorQueueContext(DbContextOptions<ErrorQueueContext> options)
           : base(options)
        {

        }
        public DbSet<ShoppingCart> shoppingCart { get; set; } = default!;
        //public DbSet<Product> product{ get; set; } = default!;
        //public DbSet<Cart_Products> cart_Products{ get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShoppingCart>().HasNoKey();

        }
    }
}
