using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using ConexionAppWeb_Apigateway.Models.DB;

namespace ConexionAppWeb_Apigateway.Controllers
{
    public class EstanteRecompensasController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EstanteRecompensasController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: EstanteRecompensa
        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("http://localhost:5278/EstanteRecompensa/ListarEstanteRecompensas");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var estantes = JsonConvert.DeserializeObject<List<EstanteRecompensa>>(content);

                return View(estantes);
            }
            else
            {
                return View(new List<EstanteRecompensa>());
            }
        }

        // GET: EstanteRecompensa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"http://localhost:5278/EstanteRecompensa/BuscarEstanteRecompensa?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var estante = JsonConvert.DeserializeObject<EstanteRecompensa>(content);

                if (estante == null)
                {
                    return NotFound();
                }

                return View(estante);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: EstanteRecompensa/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstanteRecompensa/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string IdRecompensa, string IdUsuario)
        {
            try
            {
                var queryString = $"?IdRecompensa={IdRecompensa}&IdUsuario={IdUsuario}";
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.PostAsync($"http://localhost:5278/EstanteRecompensa/RegistrarEstanteRecompensa{queryString}", null);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: EstanteRecompensa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"http://localhost:5278/EstanteRecompensa/BuscarEstanteRecompensa?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var estante = JsonConvert.DeserializeObject<EstanteRecompensa>(content);

                if (estante == null)
                {
                    return NotFound();
                }

                return View(estante);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: EstanteRecompensa/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string IdRecompensa, string IdUsuario, EstanteRecompensa estanteRecompensa)
        {
            if (id != estanteRecompensa.IdEstanteRecompensas)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var queryString = $"?id={id}&IdRecompensa={IdRecompensa}&IdUsuario={IdUsuario}";
                    var httpClient = _httpClientFactory.CreateClient();
                    var response = await httpClient.PutAsync($"http://localhost:5278/EstanteRecompensa/EditarEstanteRecompensa{queryString}", null);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return View(estanteRecompensa);
                    }
                }
                catch (Exception)
                {
                    return View(estanteRecompensa);
                }
            }
            return View(estanteRecompensa);
        }

        // GET: EstanteRecompensa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"http://localhost:5278/EstanteRecompensa/BuscarEstanteRecompensa?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var estante = JsonConvert.DeserializeObject<EstanteRecompensa>(content);

                if (estante == null)
                {
                    return NotFound();
                }

                return View(estante);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: EstanteRecompensa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.DeleteAsync($"http://localhost:5278/EstanteRecompensa/EliminarEstanteRecompensa?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}
