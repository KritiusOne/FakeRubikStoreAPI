using Aplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Config
{
    public class ProductCategoryConfig : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> entity)
        {
            //Esto va a tener problemas con la relación
            entity.ToTable("Categorias_Productos");
            entity.Property(e => e.IdProduct).HasColumnName("IdProducto");
            entity.Property(e => e.IdCategory).HasColumnName("IdCategoria");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.ProductCategories)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Categorias_Productos_Categorias");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.ProductCategories)
                .HasForeignKey(d => d.IdProduct)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Categorias_Productos_Productos");
        }
    }
}
