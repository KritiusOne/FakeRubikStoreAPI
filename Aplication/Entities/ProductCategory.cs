namespace Aplication.Entities;
public partial class ProductCategory
{

    public int IdCategory { get; set; }

    public int IdProduct { get; set; }

    public virtual Category CategoryNav { get; set; } = null!;

    public virtual Product ProductNav { get; set; } = null!;
}
