namespace Aplication.Entities;

public partial class UserDirection : BaseEntity
{
    public string? Address { get; set; }
    public int IdCity { get; set; } 
    public string? Description { get; set; }
    public virtual User? User { get; set; }
    public virtual City UserCity { get; set; } = null!;
}
