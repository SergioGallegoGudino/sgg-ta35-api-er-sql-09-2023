using System;
using System.Collections.Generic;

namespace TA35_3_sgallego.Models;

public partial class Ventum
{
    public int Cajero { get; set; }

    public int Producto { get; set; }

    public int Maquina { get; set; }

    public virtual Cajero CajeroNavigation { get; set; } = null!;

    public virtual Maquina MaquinaNavigation { get; set; } = null!;

    public virtual Producto ProductoNavigation { get; set; } = null!;
}
