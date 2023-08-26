using System;
using System.Collections.Generic;

namespace ProyectoAPI.Models;

public partial class UsuarioTipo
{
    public int UsuarioTipoId { get; set; }

    public string UsuarioTipoDescripcion { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
