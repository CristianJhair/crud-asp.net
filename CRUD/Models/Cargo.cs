using System;
using System.Collections.Generic;

namespace CRUD.Models;

public partial class Cargo
{
    public int CodigoCargo { get; set; }

    public string? Descripcion { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public bool? FlagModificado { get; set; }

    public virtual ICollection<Personal> Personals { get; set; } = new List<Personal>();
}
