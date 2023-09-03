using System;
using System.Collections.Generic;

namespace TA35_3_sgallego.Models;

public partial class Maquina
{
    public int Codigo { get; set; }

    public int? Piso { get; set; }

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
