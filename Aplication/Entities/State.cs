namespace Aplication.Entities;

public partial class State : BaseEntity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Delivery> Deliveries { get; } = new List<Delivery>();
}
