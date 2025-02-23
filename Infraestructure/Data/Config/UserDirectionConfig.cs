using Aplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Config
{
    public class UserDirectionConfig : IEntityTypeConfiguration<UserDirection>
    {
        public void Configure(EntityTypeBuilder<UserDirection> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("Direccion");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Address)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Direccion");
            entity.Property(e => e.IdCity)
                .HasColumnName("IdCiudad");

            entity.HasOne(e => e.UserCity).WithMany(x => x.UserDirections)
                .HasForeignKey(e => e.IdCity)
                .HasConstraintName("FK_Usuario_Direccion");
            
        }
    }
}
