using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConexionAppWeb_Apigateway.Models.DB;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using ConexionAppWeb_Apigateway.Utilities;

namespace ConexionAppWeb_Apigateway.Controllers
{
    public class UsuariosController : Controller
    {
        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var httpClient = HttpClientSingleton.Instance;
            var response = await httpClient.GetAsync("http://localhost:5278/Usuario/ListarUsuarios");

            if (response.IsSuccessStatusCode)
            {
                // Leer y convertir la respuesta a una lista de usuarios
                var content = await response.Content.ReadAsStringAsync();
                var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(content);

                return View(usuarios);
            }
            else
            {
                // Manejar el error de la solicitud a la API
                // Puedes agregar lógica adicional según tus necesidades
                return View(new List<Usuario>());
            }
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Hacer la solicitud a la API Gateway para obtener el usuario por ID
            var httpClient = HttpClientSingleton.Instance;
            var response = await httpClient.GetAsync($"http://localhost:5278/Usuario/BuscarUsuario?id={id}");

            if (response.IsSuccessStatusCode)
            {
                // Leer y convertir la respuesta a un objeto Usuario
                var content = await response.Content.ReadAsStringAsync();
                var usuario = JsonConvert.DeserializeObject<Usuario>(content);

                if (usuario == null)
                {
                    return NotFound();
                }

                return View(usuario);
            }
            else
            {
                // Manejar el error de la solicitud a la API
                // Puedes agregar lógica adicional según tus necesidades
                return View(new Usuario()); // Puedes cambiar esto según tus necesidades
            }
        }

        
        //GET: Usuarios/Create
        public IActionResult Create()
         {
             return View();
         }


        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string Nombre, string NombreUsuario, string Contrasenia, string FechaNacimiento, string Correo, string Numero, string Direccion)
        {
            try
            {
                // Formatear la fecha en el formato "MM/dd/yyyy"
                var formattedFechaNacimiento = DateTime.Parse(FechaNacimiento).ToString("MM/dd/yyyy");

                // Construir la cadena de consulta con los parámetros del usuario
                var queryString = $"?Nombre={Uri.EscapeDataString(Nombre)}" +
                                  $"&NombreUsuario={Uri.EscapeDataString(NombreUsuario)}" +
                                  $"&Contrasenia={Uri.EscapeDataString(Contrasenia)}" +
                                  $"&FechaNacimiento={Uri.EscapeDataString(formattedFechaNacimiento)}" +
                                  $"&Correo={Uri.EscapeDataString(Correo)}" +
                                  $"&Numero={Uri.EscapeDataString(Numero)}" +
                                  $"&Direccion={Uri.EscapeDataString(Direccion)}";

                // Hacer la solicitud a la API Gateway para registrar el usuario
                var httpClient = HttpClientSingleton.Instance;
                var stri = $"http://localhost:5278/Usuario/RegistrarUsuario{queryString}";
                var response = await httpClient.PostAsync($"http://localhost:5278/Usuario/RegistrarUsuario{queryString}", null);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Manejar el error de la solicitud a la API
                    // Puedes agregar lógica adicional según tus necesidades
                    return View(new Usuario()); // Puedes cambiar esto según tus necesidades
                }
            }
            catch (Exception)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la solicitud
                return View(new Usuario()); // Puedes cambiar esto según tus necesidades
            }
        }



        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Hacer la solicitud a la API Gateway para obtener el usuario por ID
            var httpClient = HttpClientSingleton.Instance;
            var response = await httpClient.GetAsync($"http://localhost:5278/Usuario/BuscarUsuario?id={id}");

            if (response.IsSuccessStatusCode)
            {
                // Leer y convertir la respuesta a un objeto Usuario
                var content = await response.Content.ReadAsStringAsync();
                var usuario = JsonConvert.DeserializeObject<Usuario>(content);

                if (usuario == null)
                {
                    return NotFound();
                }

                return View(usuario);
            }
            else
            {
                // Manejar el error de la solicitud a la API
                // Puedes agregar lógica adicional según tus necesidades
                return View(new Usuario()); // Puedes cambiar esto según tus necesidades
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string? Nombre, string? NombreUsuario, string? Contrasenia, string? FechaNacimiento, string? Correo, string? Numero, string? Direccion, Usuario usuario)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Formatear la fecha en el formato "MM/dd/yyyy"
                    var formattedFechaNacimiento = DateTime.Parse(FechaNacimiento).ToString("MM/dd/yyyy");

                    // Construir la cadena de consulta con los parámetros del usuario
                    var queryString = $"?id={id}&Nombre={Uri.EscapeDataString(Nombre)}" +
                                      $"&NombreUsuario={Uri.EscapeDataString(NombreUsuario)}" +
                                      $"&Contrasenia={Uri.EscapeDataString(Contrasenia)}" +
                                      $"&FechaNacimiento={Uri.EscapeDataString(formattedFechaNacimiento)}" +
                                      $"&Correo={Uri.EscapeDataString(Correo)}" +
                                      $"&Numero={Uri.EscapeDataString(Numero)}" +
                                      $"&Direccion={Uri.EscapeDataString(Direccion)}";

                    // Hacer la solicitud a la API Gateway para actualizar el usuario
                    var httpClient = HttpClientSingleton.Instance;
                    var stri = $"http://localhost:5278/Usuario/EditarUsuario{queryString}";
                    var response = await httpClient.PutAsync($"http://localhost:5278/Usuario/EditarUsuario{queryString}", null);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        // Manejar el error de la solicitud a la API
                        // Puedes agregar lógica adicional según tus necesidades
                        return View(usuario);
                    }
                }
                catch (Exception)
                {
                    // Manejar cualquier excepción que pueda ocurrir durante la solicitud
                    return View(usuario);
                }
            }
            return View(usuario);
        }



        
        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Hacer la solicitud a la API Gateway para obtener los detalles del usuario
            var httpClient = HttpClientSingleton.Instance;
            var response = await httpClient.GetAsync($"http://localhost:5278/Usuario/BuscarUsuario?id={id}");

            if (response.IsSuccessStatusCode)
            {
                // Leer y convertir la respuesta a un objeto Usuario
                var content = await response.Content.ReadAsStringAsync();
                var usuario = JsonConvert.DeserializeObject<Usuario>(content);

                if (usuario == null)
                {
                    return NotFound();
                }

                return View(usuario);
            }
            else
            {
                // Manejar el error de la solicitud a la API
                // Puedes agregar lógica adicional según tus necesidades
                return NotFound();
            }
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                // Hacer la solicitud a la API Gateway para eliminar el usuario
                var httpClient = HttpClientSingleton.Instance;
                var response = await httpClient.DeleteAsync($"http://localhost:5278/Usuario/EliminarUsuario?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Manejar el error de la solicitud a la API
                    // Puedes agregar lógica adicional según tus necesidades
                    return View(new Usuario()); // Puedes cambiar esto según tus necesidades
                }
            }
            catch (Exception)
            {
                // Manejar cualquier excepción que pueda ocurrir durante la solicitud
                return View(new Usuario()); // Puedes cambiar esto según tus necesidades
            }
        }


        private async Task<bool> UsuarioExistsAsync(int id)
        {
            // Hacer la solicitud a la API Gateway para verificar la existencia del usuario por ID
            var httpClient = HttpClientSingleton.Instance;
            var response = await httpClient.GetAsync($"http://localhost:5278/Usuario/BuscarUsuario?id={id}");

            return response.IsSuccessStatusCode;
        }

    }
}
