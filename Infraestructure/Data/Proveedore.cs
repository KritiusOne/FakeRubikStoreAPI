using System;
using System.Collections.Generic;

namespace Infraestructure.Data;

public partial class Proveedore
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<ProductosProveedore> ProductosProveedores { get; } = new List<ProductosProveedore>();
}
