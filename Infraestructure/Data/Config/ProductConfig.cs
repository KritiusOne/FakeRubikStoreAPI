using Aplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("Productos");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("Descripcion");
            entity.Property(e => e.Image)
                .IsUnicode(false)
                .HasColumnName("Imagen");
            entity.Property(e => e.Thumbnail)
                .IsUnicode(false)
                .HasColumnName("Miniatura");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NombreProducto");
            entity.Property(e => e.Price).HasColumnName("Precio");
        }
    }
}
