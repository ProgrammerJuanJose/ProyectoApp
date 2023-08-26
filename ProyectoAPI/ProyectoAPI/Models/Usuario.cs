using System;
using System.Collections.Generic;

namespace ProyectoAPI.Models;

public partial class Usuario
{
    public int UsuarioId { get; set; }

    public string UsuarioCorreo { get; set; } = null!;

    public string UsuarioContrasennia { get; set; } = null!;

    public string UsuarioNombre { get; set; } = null!;

    public string UsuarioCedula { get; set; } = null!;

    public string? UsuarioTelefono { get; set; }

    public string? UsuarioDireccion { get; set; }

    public bool? Activo { get; set; }

    public virtual ICollection<Puntaje> Puntajes { get; set; } = new List<Puntaje>();

    public virtual UsuarioTipo UsuarioTipo { get; set; } = null!;
}
