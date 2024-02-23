namespace Aplication.Entities;

public partial class UserDirection : BaseEntity
{
    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; } 

    public string? Country { get; set; } 

    public string? Description { get; set; }
    public virtual User? User { get; set; }

}
