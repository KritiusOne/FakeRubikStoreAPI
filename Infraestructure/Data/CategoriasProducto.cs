using System;
using System.Collections.Generic;

namespace Infraestructure.Data;

public partial class CategoriasProducto
{
    public int Id { get; set; }

    public int IdCategoria { get; set; }

    public int IdProducto { get; set; }

    public virtual Categoria IdCategoriaNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
