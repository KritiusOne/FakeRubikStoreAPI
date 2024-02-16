namespace Aplication.Entities;

public partial class Category : BaseEntity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<ProductCategory> ProductCategories { get; } = new List<ProductCategory>();
}
