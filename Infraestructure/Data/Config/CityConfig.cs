using Aplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Config
{
    public class CityConfig : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("Ciudad");

            entity.Property(e => e.Id)
                .HasColumnName("IdCiudad");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("Nombre");
            entity.Property(e => e.IdDepartament)
                .HasColumnName("IdDepartamento");

            entity.HasOne(e => e.Departament).WithMany(x => x.Cities)
                .HasForeignKey(e => e.IdDepartament)
                .HasConstraintName("FK_Ciudad_Departamento");
                
        }
    }
}
