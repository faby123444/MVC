using System;
using System.Collections.Generic;

namespace ConexionAppWeb_Apigateway.Models.DB;

public partial class Evento
{
    public int IdEvento { get; set; }

    public string Título { get; set; } = null!;

    public DateOnly Fecha { get; set; }

    public string Lugar { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public TimeOnly? Hora { get; set; }

    public virtual ICollection<RegistroEvento> RegistroEventos { get; set; } = new List<RegistroEvento>();
}
