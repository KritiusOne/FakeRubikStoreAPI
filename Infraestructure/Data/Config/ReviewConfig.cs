using Aplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Config
{
    public class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> entity)
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("Reviews");

            entity.Property(e => e.Rate).HasColumnName("rate");
            entity.Property(e => e.Description).HasColumnName("Descripcion");


            entity.HasOne(e => e.Product).WithMany(x => x.Reviews)
                .HasForeignKey(e => e.ProductId)
                .HasConstraintName("FK_Review_Productos");
        }
    }
}
