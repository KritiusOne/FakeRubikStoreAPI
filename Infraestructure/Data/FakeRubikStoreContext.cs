using System;
using System.Collections.Generic;
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

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<CategoriasProducto> CategoriasProductos { get; set; }

    public virtual DbSet<Direccion> Direccions { get; set; }

    public virtual DbSet<Envio> Envios { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Ordene> Ordenes { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductosOrdene> ProductosOrdenes { get; set; }

    public virtual DbSet<ProductosProveedore> ProductosProveedores { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-519269S\\SQLEXPRESS;Database=FakeRubikStore;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<CategoriasProducto>(entity =>
        {
            entity.ToTable("Categorias_Productos");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.CategoriasProductos)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Categorias_Productos_Categorias");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.CategoriasProductos)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Categorias_Productos_Productos");
        });

        modelBuilder.Entity<Direccion>(entity =>
        {
            entity.ToTable("Direccion");

            entity.Property(e => e.Ciudad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ciudad");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Direccion1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Direccion");
            entity.Property(e => e.Estado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.Pais)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pais");
        });

        modelBuilder.Entity<Envio>(entity =>
        {
            entity.Property(e => e.Codigo)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEstadoNavigation).WithMany(p => p.Envios)
                .HasForeignKey(d => d.IdEstado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Envios_Estado");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Envios)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Envios_Usuarios");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.ToTable("Estado");

            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Ordene>(entity =>
        {
            entity.Property(e => e.Fecha).HasColumnType("datetime");

            entity.HasOne(d => d.IdEnvioNavigation).WithMany(p => p.Ordenes)
                .HasForeignKey(d => d.IdEnvio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ordenes_Envios");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Ordenes)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ordenes_Usuarios");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Imagen)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Miniatura)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdReviewNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdReview)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Reviews");
        });

        modelBuilder.Entity<ProductosOrdene>(entity =>
        {
            entity.ToTable("Productos_Ordenes");

            entity.HasOne(d => d.IdOrdenNavigation).WithMany(p => p.ProductosOrdenes)
                .HasForeignKey(d => d.IdOrden)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Ordenes_Ordenes");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductosOrdenes)
                .HasForeignKey(d => d.IdProducto)
                .HasConstraintName("FK_Productos_Ordenes_Productos");
        });

        modelBuilder.Entity<ProductosProveedore>(entity =>
        {
            entity.ToTable("Productos_Proveedores");

            entity.HasOne(d => d.IdProductosNavigation).WithMany(p => p.ProductosProveedores)
                .HasForeignKey(d => d.IdProductos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Proveedores_Productos");

            entity.HasOne(d => d.IdProveedoresNavigation).WithMany(p => p.ProductosProveedores)
                .HasForeignKey(d => d.IdProveedores)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Proveedores_Proveedores");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Rate).HasColumnName("rate");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol);

            entity.ToTable("Rol");

            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.IdDireccionNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdDireccion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_Direccion");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_Rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
