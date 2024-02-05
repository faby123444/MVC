using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using ConexionAppWeb_Apigateway.Models.DB;
using ConexionAppWeb_Apigateway.Utilities;

namespace ConexionAppWeb_Apigateway.Controllers
{
    public class OportunidadesVoluntariadoesController : Controller
    {
       
        public async Task<IActionResult> Index()
        {
            var httpClient = HttpClientSingleton.Instance;
            var response = await httpClient.GetAsync("http://localhost:5278/OportunidadesVoluntariado/ListarOportunidadesVoluntariado");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var oportunidades = JsonConvert.DeserializeObject<List<OportunidadesVoluntariado>>(content);
                return View(oportunidades);
            }
            else
            {
                return View(new List<OportunidadesVoluntariado>());
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var httpClient = HttpClientSingleton.Instance;
            var response = await httpClient.GetAsync($"http://localhost:5278/OportunidadesVoluntariado/BuscarOportunidadVoluntariado?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var oportunidad = JsonConvert.DeserializeObject<OportunidadesVoluntariado>(content);

                if (oportunidad == null)
                {
                    return NotFound();
                }

                return View(oportunidad);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? IdVoluntariado, int? IdUsuario)
        {
            try
            {
                var queryString = $"?IdVoluntariado={IdVoluntariado}&IdUsuario={IdUsuario}";

                var httpClient = HttpClientSingleton.Instance;
                var response = await httpClient.PostAsync($"http://localhost:5278/OportunidadesVoluntariado/RegistrarOportunidadVoluntariado{queryString}", null);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(new OportunidadesVoluntariado());
                }
            }
            catch (Exception)
            {
                return View(new OportunidadesVoluntariado());
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var httpClient = HttpClientSingleton.Instance;
            var response = await httpClient.GetAsync($"http://localhost:5278/OportunidadesVoluntariado/BuscarOportunidadVoluntariado?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var oportunidad = JsonConvert.DeserializeObject<OportunidadesVoluntariado>(content);

                if (oportunidad == null)
                {
                    return NotFound();
                }

                return View(oportunidad);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, int? IdVoluntariado, int? IdUsuario, OportunidadesVoluntariado oportunidad)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var queryString = $"?id={id}&IdVoluntariado={IdVoluntariado}&IdUsuario={IdUsuario}";

                    var httpClient = HttpClientSingleton.Instance;
                    var response = await httpClient.PutAsync($"http://localhost:5278/OportunidadesVoluntariado/EditarOportunidadVoluntariado{queryString}", null);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return View(oportunidad);
                    }
                }
                catch (Exception)
                {
                    return View(oportunidad);
                }
            }
            return View(oportunidad);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var httpClient = HttpClientSingleton.Instance;
            var response = await httpClient.GetAsync($"http://localhost:5278/OportunidadesVoluntariado/BuscarOportunidadVoluntariado?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var oportunidad = JsonConvert.DeserializeObject<OportunidadesVoluntariado>(content);

                if (oportunidad == null)
                {
                    return NotFound();
                }

                return View(oportunidad);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var httpClient = HttpClientSingleton.Instance;
                var response = await httpClient.DeleteAsync($"http://localhost:5278/OportunidadesVoluntariado/EliminarOportunidadVoluntariado?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(new OportunidadesVoluntariado());
                }
            }
            catch (Exception)
            {
                return View(new OportunidadesVoluntariado());
            }
        }

        private async Task<bool> OportunidadExistsAsync(int id)
        {
            var httpClient = HttpClientSingleton.Instance;
            var response = await httpClient.GetAsync($"http://localhost:5278/OportunidadesVoluntariado/BuscarOportunidadVoluntariado?id={id}");

            return response.IsSuccessStatusCode;
        }
    }
}
