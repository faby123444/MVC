using System;
using System.Collections.Generic;

namespace ConexionAppWeb_Apigateway.Models.DB;

public partial class OportunidadesVoluntariado
{
    public int OportunidadesVoluntariado1 { get; set; }

    public int? IdVoluntariado { get; set; }

    public int? IdUsuario { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual Voluntariado? IdVoluntariadoNavigation { get; set; }
}
