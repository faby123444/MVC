using System.ComponentModel.DataAnnotations;

namespace ConexionAppWeb_Apigateway.Models.DB
{
    public class Login
    {
        [Required(ErrorMessage = "El campo Usuario es obligatorio.")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
        [DataType(DataType.Password)]
        public string Contraseña { get; set; }
    }
}
