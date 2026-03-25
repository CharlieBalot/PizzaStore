using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PizzaData.Models;

namespace PizzaAPI.Models
{
    //public class PizzaAPIDbContext : DbContext
    //{
    //    public PizzaAPIDbContext(DbContextOptions<PizzaAPIDbContext> options) : base(options)
    //    {
    //    }

    //    public DbSet<Pizzas> Pizzas => Set<Pizzas>();
    //    public DbSet<PizzaType> PizzaTypes => Set<PizzaType>();
    //    public DbSet<Orders> Orders => Set<Orders>();
    //    public DbSet<OrderDetails> OrderDetails => Set<OrderDetails>();
        

    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        modelBuilder.Entity<Pizzas>()
    //            .HasIndex(p => p.pizza_id)
    //            .IsUnique();

    //        modelBuilder.Entity<PizzaType>()
    //            .HasIndex(pt => pt.pizza_type_id)
    //            .IsUnique();

    //        modelBuilder.Entity<Orders>()
    //            .HasIndex(o => o.order_id)
    //            .IsUnique();

    //        modelBuilder.Entity<OrderDetails>()
    //            .HasIndex(od => od.order_details_id)
    //            .IsUnique();
    //    }
    //}
}
