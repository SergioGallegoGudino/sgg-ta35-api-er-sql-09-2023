using System;
using System.Collections.Generic;

namespace TA35_2_sgallego.Models;

public partial class Proyecto
{
    public string Id { get; set; } = null!;

    public string? Nombre { get; set; }

    public int? Horas { get; set; }

    public virtual ICollection<Cientifico> Cientificos { get; set; } = new List<Cientifico>();
}
