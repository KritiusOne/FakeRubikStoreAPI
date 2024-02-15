using System;
using System.Collections.Generic;

namespace Infraestructure.Data;

public partial class Usuario
{
    public int Id { get; set; }

    public int IdRol { get; set; }

    public int IdDireccion { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string Email { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public virtual ICollection<Envio> Envios { get; } = new List<Envio>();

    public virtual Direccion IdDireccionNavigation { get; set; } = null!;

    public virtual Rol IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Ordene> Ordenes { get; } = new List<Ordene>();
}
