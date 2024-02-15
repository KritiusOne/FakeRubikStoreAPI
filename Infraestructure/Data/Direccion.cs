using System;
using System.Collections.Generic;

namespace Infraestructure.Data;

public partial class Direccion
{
    public int Id { get; set; }

    public string Direccion1 { get; set; } = null!;

    public string Ciudad { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; } = new List<Usuario>();
}
