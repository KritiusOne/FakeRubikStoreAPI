using System;
using System.Collections.Generic;

namespace Infraestructure.Data;

public partial class ProductosProveedore
{
    public int Id { get; set; }

    public int IdProveedores { get; set; }

    public int IdProductos { get; set; }

    public virtual Producto IdProductosNavigation { get; set; } = null!;

    public virtual Proveedore IdProveedoresNavigation { get; set; } = null!;
}
