using System;
using System.Collections.Generic;

namespace TA35_1_sgallego.Models;

public partial class Suministra
{
    public int CodigoPieza { get; set; }

    public string ProveedorId { get; set; } = null!;

    public double? Precio { get; set; }

    public virtual Pieza CodigoPiezaNavigation { get; set; } = null!;
}
