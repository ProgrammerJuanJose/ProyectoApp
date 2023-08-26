using System;
using System.Collections.Generic;

namespace ProyectoAPI.Models;

public partial class Puntaje
{
    public int PuntajeId { get; set; }

    public int UsuarioId { get; set; }

    public int Puntos { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
