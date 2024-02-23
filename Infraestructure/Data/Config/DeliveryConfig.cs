using Aplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Config
{
    public class DeliveryConfig : IEntityTypeConfiguration<Delivery>
    {
        public void Configure(EntityTypeBuilder<Delivery> entity)
        {
            entity.HasKey(x => x.Id);
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
        }
    }
}
