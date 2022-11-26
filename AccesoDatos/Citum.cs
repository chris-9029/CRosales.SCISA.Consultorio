using System;
using System.Collections.Generic;

namespace AccesoDatos;

public partial class Citum
{
    public int IdCita { get; set; }

    public int? IdDoctor { get; set; }

    public int? IdPaciente { get; set; }

    public string? Detalle { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual Doctor? IdDoctorNavigation { get; set; }

    public virtual Paciente? IdPacienteNavigation { get; set; }
}
