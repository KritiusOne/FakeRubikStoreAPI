using Aplication.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infraestructure.Data;

public partial class FakeRubikStoreContext : DbContext
{
    public FakeRubikStoreContext()
    {
    }

    public FakeRubikStoreContext(DbContextOptions<FakeRubikStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<UserDirection> Directions { get; set; }

    public virtual DbSet<Delivery> Deliveries { get; set; }

    public virtual DbSet<Departament> Departaments { get; set; }
    public virtual DbSet<City> Cities { get; set; }
    public virtual DbSet<Country> Countries { get; set; }


    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<OrdersProducts> ProductsOrders { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Card> Cards { get; set; }
    public virtual DbSet<CardType> CardTypes { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductCategory>()
            .HasKey(e => new
            {
                e.IdProduct,
                e.IdCategory
            });
        modelBuilder.Entity<OrdersProducts>()
            .HasKey(e => new
            {
                e.IdProduct,
                e.IdOrder
            });
        modelBuilder.Entity<Review>()
            .HasKey(e => new
            {
                e.ProductId,
                e.UserId
            });
        modelBuilder.Ignore<Category>();
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}
