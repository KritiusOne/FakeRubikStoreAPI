namespace Aplication.Entities;

public partial class OrdersProducts
{
    public int IdProduct { get; set; }

    public int IdOrder { get; set; }

    public int ProductsNumber { get; set; }

    public double Price { get; set; }

    public virtual Order OrderNav { get; set; } = null!;

    public virtual Product? ProductNav { get; set; }
}
