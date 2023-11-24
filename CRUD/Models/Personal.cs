using System;
using System.Collections.Generic;

namespace CRUD.Models;

public partial class Personal
{
    public int CodigoEmpleado { get; set; }

    public string? NumeroDocumento { get; set; }

    public string? Nombres { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public DateTime? FechaIngreso { get; set; }

    public int? CodigoEmpresa { get; set; }

    public int? CodigoCargo { get; set; }

    public virtual Cargo? CodigoCargoNavigation { get; set; }

    public virtual Empresa? CodigoEmpresaNavigation { get; set; }
}
