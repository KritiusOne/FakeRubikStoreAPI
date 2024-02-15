using System;
using System.Collections.Generic;

namespace Infraestructure.Data;

public partial class Ordene
{
    public int Id { get; set; }

    public int IdUser { get; set; }

    public int IdEnvio { get; set; }

    public DateTime Fecha { get; set; }

    public double PrecioFinal { get; set; }

    public virtual Envio IdEnvioNavigation { get; set; } = null!;

    public virtual Usuario IdUserNavigation { get; set; } = null!;

    public virtual ICollection<ProductosOrdene> ProductosOrdenes { get; } = new List<ProductosOrdene>();
}
