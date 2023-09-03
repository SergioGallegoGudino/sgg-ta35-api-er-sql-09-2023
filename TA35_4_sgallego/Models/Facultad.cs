using System;
using System.Collections.Generic;

namespace TA35_4_sgallego.Models;

public partial class Facultad
{
    public int Codigo { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();

    public virtual ICollection<Investigador> Investigadors { get; set; } = new List<Investigador>();
}
