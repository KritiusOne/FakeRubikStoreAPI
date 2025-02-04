using Aplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Config
{
    public class CountryConfig : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("Pais");

            entity.Property(e => e.Name)
                .HasColumnName("Nombre")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
