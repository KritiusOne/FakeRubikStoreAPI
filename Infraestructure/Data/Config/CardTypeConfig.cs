using Aplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Config
{
    public class CardTypeConfig : IEntityTypeConfiguration<CardType>
    {
        public void Configure(EntityTypeBuilder<CardType> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("TipoTarjeta");

            entity.Property(e => e.Name)
                .HasColumnName("Nombre")
                .HasMaxLength(100);
            entity.Property(e => e.Code)
                .HasColumnName("Codigo");
        }
    }
}
