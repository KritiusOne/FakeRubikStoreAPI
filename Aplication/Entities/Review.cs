using System;
using System.Collections.Generic;

namespace Aplication.Entities;

public partial class Review : BaseEntity
{
    public int ProductId { get; set; }

    public int UserId { get; set; }

    public string? Description { get; set; }

    public int Rate { get; set; }

    public virtual Product Product { get; } = new Product();
    public virtual User Usuario { get; } = new User();
}
