using System;
using System.Collections.Generic;

namespace Infraestructure.Data;

public partial class Review
{
    public int Id { get; set; }

    public string? Descripcion { get; set; }

    public int Rate { get; set; }

    public virtual ICollection<Producto> Productos { get; } = new List<Producto>();
}
