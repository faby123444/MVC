using System;
using System.Collections.Generic;

namespace ConexionAppWeb_Apigateway.Models.DB;

public partial class Voluntariado
{
    public int IdVoluntariado { get; set; }

    public string Nombre { get; set; } = null!;

    public int DuracionMeses { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime FechaFin { get; set; }

    public string Lugar { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<OportunidadesVoluntariado> OportunidadesVoluntariados { get; set; } = new List<OportunidadesVoluntariado>();
}
