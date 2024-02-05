using System;
using System.Collections.Generic;

namespace ConexionAppWeb_Apigateway.Models.DB;

public partial class RegistroEvento
{
    public int RegistroEventos { get; set; }

    public int? IdEvento { get; set; }

    public int? IdUsuario { get; set; }

    public virtual Evento? IdEventoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
