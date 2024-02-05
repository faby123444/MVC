using System;
using System.Collections.Generic;

namespace ConexionAppWeb_Apigateway.Models.DB;

public partial class Recompensa
{
    public int IdRecompensa { get; set; }

    public string Título { get; set; } = null!;

    public string Detalle { get; set; } = null!;

    public virtual ICollection<EstanteRecompensa> EstanteRecompensas { get; set; } = new List<EstanteRecompensa>();
}
