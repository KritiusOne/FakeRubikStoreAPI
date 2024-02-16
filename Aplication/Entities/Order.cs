namespace Aplication.Entities;

public partial class Order : BaseEntity
{
    public int IdUser { get; set; }

    public int IdDelivery { get; set; }

    public DateTime Date { get; set; }

    public double FinalPrice { get; set; }

    public virtual Delivery DeliveryNav { get; set; } = null!;

    public virtual User UserNav { get; set; } = null!;

    public virtual ICollection<OrdersProducts> OrderProducts { get; } = new List<OrdersProducts>();
}
