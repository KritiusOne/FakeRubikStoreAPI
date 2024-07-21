using Aplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Config
{
    public class OrdersProductsConfig : IEntityTypeConfiguration<OrdersProducts>
    {
        public void Configure(EntityTypeBuilder<OrdersProducts> entity)
        {
            entity.ToTable("Productos_Ordenes");
            entity.Property(e => e.IdProduct)
                .HasColumnName("IdProducto");
            entity.Property(e => e.IdOrder)
                .HasColumnName("IdOrden");
            entity.Property(e => e.ProductsNumber)
                .HasColumnName("Cantidad");
            entity.Property(e => e.Price)
                .HasColumnName("Precio");

            entity.HasOne(d => d.OrderNav).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.IdOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos_Ordenes_Ordenes");

            entity.HasOne(d => d.ProductNav).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK_Productos_Ordenes_Productos");
        }
    }
}
