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

            entity.HasOne(d => d.DeliveryNav).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdDelivery)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ordenes_Envios");

            entity.HasOne(d => d.UserNav).WithMany(p => p.Orders)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ordenes_Usuarios");
            
        }
    }
}
