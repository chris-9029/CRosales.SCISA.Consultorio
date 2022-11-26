using System;
using System.Collections.Generic;

namespace AccesoDatos;

public partial class Doctor
{
    public int IdDoctor { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public string? Cedula { get; set; }

    public string? Foto { get; set; }

    public virtual ICollection<Citum> Cita { get; } = new List<Citum>();
}
