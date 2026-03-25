using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PizzaData.Models
{
    public class PizzaDbContext : DbContext
    {
        public PizzaDbContext()
        {
        }

        public PizzaDbContext(DbContextOptions<PizzaDbContext> options) : base(options)
        {
        }

        public DbSet<Pizzas> Pizzas => Set<Pizzas>();
        public DbSet<PizzaType> PizzaTypes => Set<PizzaType>();
        public DbSet<Orders> Orders => Set<Orders>();
        public DbSet<OrderDetails> OrderDetails => Set<OrderDetails>();


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Server=LAPTOP-H70BIPE0;Database=pizzaDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pizzas>()
                .HasIndex(p => p.pizza_id)
                .IsUnique();

            modelBuilder.Entity<PizzaType>()
                .HasIndex(pt => pt.pizza_type_id)
                .IsUnique();

            modelBuilder.Entity<Orders>()
                .HasIndex(o => o.order_id)
                .IsUnique();

            modelBuilder.Entity<OrderDetails>()
                .HasIndex(od => od.order_details_id)
                .IsUnique();
        }
    }

    //public class PizzaDbContext : DbContext
    //{
    //    public DbSet<Pizzas> Pizzas { get; set; }
    //    public DbSet<PizzaType> PizzaTypes { get; set; }
    //    public DbSet<Orders> Orders { get; set; }
    //    public DbSet<OrderDetails> OrderDetails { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder options)
    //    {
    //        options.UseSqlServer(@"Server=LAPTOP-H70BIPE0;Database=pizzaDB;Trusted_Connection=True;TrustServerCertificate=True;");
    //    }

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
