using System;
using System.Collections.Generic;

namespace Aplication.Entities;

public partial class Review
{
    public int Id { get; set; }
    public int ProductId { get; set; }

    public string? Description { get; set; }

    public int Rate { get; set; }

    public virtual Product Product { get; } = new Product();
}
