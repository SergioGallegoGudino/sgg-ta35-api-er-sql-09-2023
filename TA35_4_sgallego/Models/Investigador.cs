using System;
using System.Collections.Generic;

namespace TA35_4_sgallego.Models;

public partial class Investigador
{
    public string Dni { get; set; } = null!;

    public string? NomApels { get; set; }

    public int? Facultad { get; set; }

    public virtual Facultad? FacultadNavigation { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
