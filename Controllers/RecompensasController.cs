using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ConexionAppWeb_Apigateway.Models.DB;
using Newtonsoft.Json;
using System.Net.Http;
using ConexionAppWeb_Apigateway.Utilities;

namespace ConexionAppWeb_Apigateway.Controllers
{
    public class RecompensasController : Controller
    {
        // GET: Recompensas
        public async Task<IActionResult> Index()
        {
            var httpClient = HttpClientSingleton.Instance;
            var response = await httpClient.GetAsync("http://localhost:5278/Recompensa/ListarRecompensas");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var recompensas = JsonConvert.DeserializeObject<List<Recompensa>>(content);

                return View(recompensas);
            }
            else
            {
                return View(new List<Recompensa>());
            }
        }

        // GET: Recompensas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var httpClient = HttpClientSingleton.Instance;
            var response = await httpClient.GetAsync($"http://localhost:5278/Recompensa/BuscarRecompensa?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var recompensa = JsonConvert.DeserializeObject<Recompensa>(content);

                if (recompensa == null)
                {
                    return NotFound();
                }

                return View(recompensa);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: Recompensas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recompensas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string Título, string Detalle)
        {
            try
            {
                var queryString = $"?Título={Uri.EscapeDataString(Título)}&Detalle={Uri.EscapeDataString(Detalle)}";

                var httpClient = HttpClientSingleton.Instance;
                var response = await httpClient.PostAsync($"http://localhost:5278/Recompensa/RegistrarRecompensa{queryString}", null);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(new Recompensa());
                }
            }
            catch (Exception)
            {
                return View(new Recompensa());
            }
        }

        // GET: Recompensas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var httpClient = HttpClientSingleton.Instance;
            var response = await httpClient.GetAsync($"http://localhost:5278/Recompensa/BuscarRecompensa?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var recompensa = JsonConvert.DeserializeObject<Recompensa>(content);

                if (recompensa == null)
                {
                    return NotFound();
                }

                return View(recompensa);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: Recompensas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string Título, string Detalle, Recompensa recompensa)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var queryString = $"?id={id}&Título={Uri.EscapeDataString(Título)}&Detalle={Uri.EscapeDataString(Detalle)}";

                    var httpClient = HttpClientSingleton.Instance;
                    var response = await httpClient.PutAsync($"http://localhost:5278/Recompensa/EditarRecompensa{queryString}", null);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return View(recompensa);
                    }
                }
                catch (Exception)
                {
                    return View(recompensa);
                }
            }
            return View(recompensa);
        }

        // GET: Recompensas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var httpClient = HttpClientSingleton.Instance;
            var response = await httpClient.GetAsync($"http://localhost:5278/Recompensa/BuscarRecompensa?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var recompensa = JsonConvert.DeserializeObject<Recompensa>(content);

                if (recompensa == null)
                {
                    return NotFound();
                }

                return View(recompensa);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: Recompensas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var httpClient = HttpClientSingleton.Instance;
                var response = await httpClient.DeleteAsync($"http://localhost:5278/Recompensa/EliminarRecompensa?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(new Recompensa());
                }
            }
            catch (Exception)
            {
                return View(new Recompensa());
            }
        }

        private async Task<bool> RecompensaExistsAsync(int id)
        {
            var httpClient = HttpClientSingleton.Instance;
            var response = await httpClient.GetAsync($"http://localhost:5278/Recompensa/BuscarRecompensa?id={id}");

            return response.IsSuccessStatusCode;
        }
    }
}
