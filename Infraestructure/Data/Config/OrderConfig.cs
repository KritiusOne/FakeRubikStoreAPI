using Aplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Config
{
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> entity)
        {
            entity.HasKey(x => x.Id);
            entity.ToTable("Ordenes");
            entity.Property(e => e.Date).HasColumnType("datetime").HasColumnName("Fecha");
            entity.Property(e => e.FinalPrice).HasColumnName("PrecioFinal");
            entity.Property(e => e.IdDelivery).HasColumnName("IdEnvio");
            entity.Property(e => e.NumberCard).HasColumnName("numerotarjeta");

            entity.HasOne(d => d.DeliveryInfo).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdDelivery)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Ordenes_Envios");

            entity.HasOne(d => d.UserInfo).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ordenes_Usuarios");

            entity.HasMany(d => d.OrderProducts)
                .WithOne(h => h.OrderNav)
                .HasForeignKey(op => op.IdOrder)
                .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
