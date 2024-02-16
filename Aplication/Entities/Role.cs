namespace Aplication.Entities;

public partial class Role : BaseEntity
{
    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}
