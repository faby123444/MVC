using ConexionAppWeb_Apigateway.Models.DB;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ConexionAppWeb_Apigateway.Controllers
{
    public class HomeController : Controller
    {
        // Acción para mostrar el formulario de login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Login model)
        {
            if (ModelState.IsValid)
            {
                // Verifica el usuario y la contraseña
                if (model.Usuario == "usuario" && model.Contraseña == "contraseña")
                {
                    // Si las credenciales son válidas, redirecciona a Eventos/Index
                    return RedirectToAction("Index", "Eventoes");
                }
                else
                {
                    // Si las credenciales son incorrectas, muestra un mensaje de error
                    ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
                    return View(model);
                }
            }
            else
            {
                // Si el modelo no es válido, vuelve a mostrar el formulario de login con errores de validación
                return View(model);
            }
        }

        // Acción para la página principal después de iniciar sesión
        public ActionResult Dashboard()
        {
            return View();
        }

    }
}
