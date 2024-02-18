using Aplication.Entities;
using Microsoft.EntityFrameworkCore;

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
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false).HasColumnName("Nombre");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.ToTable("Categorias_Productos");
            entity.Property(e => e.IdProduct).HasColumnName("IdProducto");
            entity.Property(e => e.IdCategory).HasColumnName("IdCategoria");
            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.ProductCategories)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Categorias_Productos_Categorias");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductCategories)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Categorias_Productos_Productos");
        });

        modelBuilder.Entity<UserDirection>(entity =>
        {
            entity.ToTable("Direccion");

            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ciudad");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Direccion");
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pais");
        });

        modelBuilder.Entity<Delivery>(entity =>
        {
            entity.ToTable("Envios");
            entity.Property(e => e.Code)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Codigo");
            entity.Property(e => e.IdState).HasColumnName("IdEstado");
            entity.Property(e => e.IdUser).HasColumnName("IdUsuario");
            entity.HasOne(d => d.IdStateNav).WithMany(p => p.Deliveries)
                .HasForeignKey(d => d.IdState)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Envios_Estado");

            entity.HasOne(d => d.IdUserNav).WithMany(p => p.Deliveries)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Envios_Usuarios");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.ToTable("State");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false).HasColumnName("Nombre");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.Date).HasColumnType("datetime").HasColumnName("Fecha");
            entity.Property(e => e.FinalPrice).HasColumnName("PrecioFinal");
            entity.Property(e => e.IdDelivery).HasColumnName("IdEnvio");
            entity.HasOne(d => d.DeliveryNav).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdDelivery)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ordenes_Envios");

            entity.HasOne(d => d.UserNav).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ordenes_Usuarios");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false).HasColumnName("Descripcion");
            entity.Property(e => e.Image)
                .HasColumnName("Imagen")
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Thumbnail)
                .HasColumnName("Miniatura")
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasColumnName("NombreProducto ")
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.ReviewNav).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdReview)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Reviews");
        });

        modelBuilder.Entity<OrdersProducts>(entity =>
        {
            entity.ToTable("Productos_Ordenes");
            entity.Property(e => e.IdProduct).HasColumnName("IdProducto");
            entity.Property(e => e.IdOrder).HasColumnName("IdOrden");
            entity.Property(e => e.ProductsNumber).HasColumnName("Cantidad");
            entity.Property(e => e.Price).HasColumnName("Precio");
            entity.HasOne(d => d.OrderNav).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.IdOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Ordenes_Ordenes");

            entity.HasOne(d => d.ProductNav).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK_Productos_Ordenes_Productos");
        });

        modelBuilder.Entity<ProductsProviders>(entity =>
        {
            entity.ToTable("Productos_Proveedores");
            entity.Property(e => e.IdProduct).HasColumnName("IdProducto");
            entity.Property(e => e.IdProvider).HasColumnName("IdProveedor");
            entity.HasOne(d => d.IdProductosNavigation).WithMany(p => p.ProvidersProducts)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Proveedores_Productos");

            entity.HasOne(d => d.IdProveedoresNavigation).WithMany(p => p.ProvidersProducts)
                .HasForeignKey(d => d.IdProvider)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Proveedores_Proveedores");
        });

        modelBuilder.Entity<Provider>(entity =>
        {
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre");
            entity.Property(e => e.Phone)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Telefono");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Descripcion");
            entity.Property(e => e.Rate).HasColumnName("rate");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("Rol");
            entity.Property(e => e.Id).HasColumnName("IdRol");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Nombre");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Usuarios");
            entity.Property(e => e.SecondName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Apellido");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre");
            entity.Property(e => e.Phone)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Telefono");
            entity.Property(e => e.IdRole).HasColumnName("IdRol");
            entity.Property(e => e.IdAddress).HasColumnName("IdDireccion");
            entity.HasOne(d => d.UserDirectionNav).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdAddress)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_Direccion");

            entity.HasOne(d => d.RoleNav).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_Rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
