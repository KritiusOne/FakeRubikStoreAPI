namespace Aplication.Entities;

public partial class Delivery : BaseEntity
{
    public int IdState { get; set; }

    public int IdUser { get; set; }

    public string Code { get; set; } = null!;

    public virtual State IdStateNav { get; set; } = null!;

    public virtual User IdUserNav { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
