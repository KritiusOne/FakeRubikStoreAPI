using System;
using System.Collections.Generic;

namespace Infraestructure.Data;

public partial class ProductosOrdene
{
    public int Id { get; set; }

    public int? IdProducto { get; set; }

    public int IdOrden { get; set; }

    public int Cantidad { get; set; }

    public double Precio { get; set; }

    public virtual Ordene IdOrdenNavigation { get; set; } = null!;

    public virtual Producto? IdProductoNavigation { get; set; }
}
