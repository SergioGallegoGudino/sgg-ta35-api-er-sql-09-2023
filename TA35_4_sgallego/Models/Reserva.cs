using System;
using System.Collections.Generic;

namespace TA35_4_sgallego.Models;

public partial class Reserva
{
    public string Dni { get; set; } = null!;

    public string NumSerie { get; set; } = null!;

    public DateOnly? Comienzo { get; set; }

    public DateOnly? Fin { get; set; }

    public virtual Investigador DniNavigation { get; set; } = null!;

    public virtual Equipo NumSerieNavigation { get; set; } = null!;
}
