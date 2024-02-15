using System;
using System.Collections.Generic;

namespace Infraestructure.Data;

public partial class Categoria
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<CategoriasProducto> CategoriasProductos { get; } = new List<CategoriasProducto>();
}
