using Aplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Config
{
    public class CardConfig : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("Tarjeta");

            entity.Property(e => e.IdCardType)
                .HasColumnName("IdTipoTarjeta");
            entity.Property(e => e.CardNumber)
                .HasMaxLength(20)
                .HasColumnName("NumeroTarjeta");

            entity.HasOne(e => e.Type).WithMany(x => x.Cards)
                .HasForeignKey(e => e.IdCardType)
                .HasConstraintName("FK_Tarjeta_TipoTarjeta")
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
