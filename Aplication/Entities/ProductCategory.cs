namespace Aplication.Entities;
public partial class ProductCategory : BaseEntity
{

    public int IdCategory { get; set; }

    public int IdProduct { get; set; }

    public virtual Category IdCategoriaNavigation { get; set; } = null!;

    public virtual Product IdProductoNavigation { get; set; } = null!;
}
