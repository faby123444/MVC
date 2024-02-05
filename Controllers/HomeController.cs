using ConexionAppWeb_Apigateway.Models.DB;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ConexionAppWeb_Apigateway.Controllers
{
    public class HomeController : Controller
    {
        // Acci�n para mostrar el formulario de login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Login model)
        {
            if (ModelState.IsValid)
            {
                // Verifica el usuario y la contrase�a
                if (model.Usuario == "usuario" && model.Contrase�a == "contrase�a")
                {
                    // Si las credenciales son v�lidas, redirecciona a Eventos/Index
                    return RedirectToAction("Index", "Eventoes");
                }
                else
                {
                    // Si las credenciales son incorrectas, muestra un mensaje de error
                    ModelState.AddModelError("", "Usuario o contrase�a incorrectos.");
                    return View(model);
                }
            }
            else
            {
                // Si el modelo no es v�lido, vuelve a mostrar el formulario de login con errores de validaci�n
                return View(model);
            }
        }

        // Acci�n para la p�gina principal despu�s de iniciar sesi�n
        public ActionResult Dashboard()
        {
            return View();
        }

    }
}
