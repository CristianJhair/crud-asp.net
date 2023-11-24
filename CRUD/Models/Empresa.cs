using System;
using System.Collections.Generic;

namespace CRUD.Models;

public partial class Empresa
{
    public int CodigoEmpresa { get; set; }

    public string? Ruc { get; set; }

    public string? RazonSocial { get; set; }

    public DateTime? FechaFundacion { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Personal> Personals { get; set; } = new List<Personal>();
}
