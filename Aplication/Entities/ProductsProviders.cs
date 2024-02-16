namespace Aplication.Entities;

public partial class ProductsProviders : BaseEntity
{
    public int IdProveedores { get; set; }

    public int IdProductos { get; set; }

    public virtual Product IdProductosNavigation { get; set; } = null!;

    public virtual Provider IdProveedoresNavigation { get; set; } = null!;
}
