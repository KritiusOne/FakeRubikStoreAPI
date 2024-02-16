﻿using System;
using System.Collections.Generic;

namespace Aplication.Entities;

public partial class Review
{
    public int Id { get; set; }

    public string? Descripcion { get; set; }

    public int Rate { get; set; }

    public virtual ICollection<Product> Productos { get; } = new List<Product>();
}
