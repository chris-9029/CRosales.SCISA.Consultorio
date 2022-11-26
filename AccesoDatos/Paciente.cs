using System;
using System.Collections.Generic;

namespace AccesoDatos;

public partial class Paciente
{
    public int IdPaciente { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public decimal? Peso { get; set; }

    public decimal? Altura { get; set; }

    public string? Foto { get; set; }

    public virtual ICollection<Citum> Cita { get; } = new List<Citum>();
}
