namespace Aplication.Entities;

public partial class User : BaseEntity
{
    public int IdRole { get; set; }

    public int IdAddress { get; set; }

    public string? Name { get; set; }

    public string? SecondName { get; set; }

    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public virtual ICollection<Delivery> Deliveries { get; } = new List<Delivery>();

    public virtual UserDirection AdressInfo { get; set; } = null!;

    public virtual Role RoleNav { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
    public virtual ICollection<Review> Reviews { get; } = new List<Review>();
}
