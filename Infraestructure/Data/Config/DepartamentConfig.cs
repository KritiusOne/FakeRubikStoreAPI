using Aplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Config
{
    public class DepartamentConfig : IEntityTypeConfiguration<Departament>
    {
        public void Configure(EntityTypeBuilder<Departament> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("Departamento");

            entity.Property(e => e.IdCountry)
                .HasColumnName("IdPais");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnName("Nombre")
                .HasMaxLength(50);

            entity.HasOne(e => e.Country).WithMany(x => x.Departaments)
                .HasForeignKey(e => e.IdCountry)
                .HasConstraintName("FK_Departamento_Pais")
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
