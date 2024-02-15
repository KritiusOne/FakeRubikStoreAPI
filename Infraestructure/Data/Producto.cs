using System;
using System.Collections.Generic;

namespace Infraestructure.Data;

public partial class Producto
{
    public int Id { get; set; }

    public string NombreProducto { get; set; } = null!;

    public double Precio { get; set; }

    public int Stock { get; set; }

    public string? Imagen { get; set; }

    public string Descripcion { get; set; } = null!;

    public int IdReview { get; set; }

    public string Miniatura { get; set; } = null!;

    public virtual ICollection<CategoriasProducto> CategoriasProductos { get; } = new List<CategoriasProducto>();

    public virtual Review IdReviewNavigation { get; set; } = null!;

    public virtual ICollection<ProductosOrdene> ProductosOrdenes { get; } = new List<ProductosOrdene>();

    public virtual ICollection<ProductosProveedore> ProductosProveedores { get; } = new List<ProductosProveedore>();
}
