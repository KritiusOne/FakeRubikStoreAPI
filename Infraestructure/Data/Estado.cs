using System;
using System.Collections.Generic;

namespace Infraestructure.Data;

public partial class Estado
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Envio> Envios { get; } = new List<Envio>();
}
