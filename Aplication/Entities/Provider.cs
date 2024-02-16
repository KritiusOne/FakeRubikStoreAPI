namespace Aplication.Entities;

public partial class Provider : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<ProductsProviders> ProvidersProducts { get; } = new List<ProductsProviders>();
}
