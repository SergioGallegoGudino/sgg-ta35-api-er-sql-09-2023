using System;
using System.Collections.Generic;

namespace TA35_4_sgallego.Models;

public partial class Equipo
{
    public string NumSerie { get; set; } = null!;

    public string? Nombre { get; set; }

    public int? Facultad { get; set; }

    public virtual Facultad? FacultadNavigation { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
