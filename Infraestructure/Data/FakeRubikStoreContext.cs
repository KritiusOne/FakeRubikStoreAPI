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

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<OrdersProducts> ProductsOrders { get; set; }

    public virtual DbSet<ProductsProviders> ProductsProviders { get; set; }

    public virtual DbSet<Provider> Providers { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

}
