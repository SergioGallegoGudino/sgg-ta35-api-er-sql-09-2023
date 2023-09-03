using System;
using System.Collections.Generic;

namespace TA35_3_sgallego.Models;

public partial class Producto
{
    public int Codigo { get; set; }

    public string? Nombre { get; set; }

    public int? Precio { get; set; }

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
