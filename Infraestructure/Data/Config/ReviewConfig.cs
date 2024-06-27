using Aplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Config
{
    public class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> entity)
        {
            entity.ToTable("Reviews");

            entity.Property(e => e.Rate).HasColumnName("rate");
            entity.Property(e => e.Description).HasColumnName("Descripcion");
            entity.Property(e => e.ProductId).HasColumnName("IdProducto");
            entity.Property(e => e.UserId).HasColumnName("UsuarioId");

            entity.HasOne(e => e.Product).WithMany(x => x.Reviews)
                .HasForeignKey(e => e.ProductId)
                .HasConstraintName("FK_Review_Productos");
            entity.HasOne(e => e.Usuario).WithMany(x => x.Reviews)
                .HasForeignKey(e => e.UserId)
                .HasConstraintName("FK_Reviews_Usuarios");
        }
    }
}
