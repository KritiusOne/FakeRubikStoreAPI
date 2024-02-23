using Aplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasKey(x => x.Id);
            entity.ToTable("Usuarios");

            entity.Property(e => e.SecondName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Apellido");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password).HasColumnName("Contrasena").HasMaxLength(150);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre");
            entity.Property(e => e.Phone)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("Telefono");
            entity.Property(e => e.IdRole).HasColumnName("IdRol");
            entity.Property(e => e.IdAddress).HasColumnName("IdDireccion");

            entity.HasOne(d => d.RoleNav).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_Rol");
            entity.HasOne(x => x.UserDirectionNav).WithOne(x => x.User)
                .HasForeignKey<User>(x => x.IdAddress)
                .HasPrincipalKey<UserDirection>(x => x.Id)
                .HasConstraintName("FK_Usuarios_Direccion");
        }
    }
}
