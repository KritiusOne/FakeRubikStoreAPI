namespace Aplication.Entities;

public partial class ProductsProviders : BaseEntity
{
    public int IdProvider { get; set; }

    public int IdProduct { get; set; }

    public virtual Product IdProductosNavigation { get; set; } = null!;

    public virtual Provider IdProveedoresNavigation { get; set; } = null!;
}
