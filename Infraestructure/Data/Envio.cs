using System;
using System.Collections.Generic;

namespace Infraestructure.Data;

public partial class Envio
{
    public int Id { get; set; }

    public int IdEstado { get; set; }

    public int IdUsuario { get; set; }

    public string Codigo { get; set; } = null!;

    public virtual Estado IdEstadoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<Ordene> Ordenes { get; } = new List<Ordene>();
}
