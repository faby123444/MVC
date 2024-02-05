using System;
using System.Collections.Generic;

namespace ConexionAppWeb_Apigateway.Models.DB;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string NombreUsuario { get; set; } = null!;

    public string Contrasenia { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public string Correo { get; set; } = null!;

    public string Numero { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public virtual ICollection<EstanteRecompensa> EstanteRecompensas { get; set; } = new List<EstanteRecompensa>();

    public virtual ICollection<OportunidadesVoluntariado> OportunidadesVoluntariados { get; set; } = new List<OportunidadesVoluntariado>();

    public virtual ICollection<RegistroEvento> RegistroEventos { get; set; } = new List<RegistroEvento>();
}
