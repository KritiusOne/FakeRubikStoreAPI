using System;
using System.Collections.Generic;

namespace Aplication.Entities;

public partial class Product : BaseEntity
{
    public string Name { get; set; } = null!;

    public double Price { get; set; }

    public int Stock { get; set; }

    public string? Image { get; set; }

    public string Description { get; set; } = null!;

    public int IdReview { get; set; }

    public string Thumbnail { get; set; } = null!;

    public virtual ICollection<ProductCategory> ProductCategories { get; } = new List<ProductCategory>();

    public virtual ICollection<Review> ReviewNav { get; set; } = new List<Review>();

    public virtual ICollection<OrdersProducts> OrderProducts { get; } = new List<OrdersProducts>();

    public virtual ICollection<ProductsProviders> ProvidersProducts { get; } = new List<ProductsProviders>();
}
