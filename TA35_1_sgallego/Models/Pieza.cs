using System;
using System.Collections.Generic;

namespace TA35_1_sgallego.Models;

public partial class Pieza
{
    public int Codigo { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Suministra> Suministras { get; set; } = new List<Suministra>();
}
