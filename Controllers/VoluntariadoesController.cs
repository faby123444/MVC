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

namespace ConexionAppWeb_Apigateway.Controllers
{
    public class VoluntariadoesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public VoluntariadoesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: Voluntariados
        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("http://localhost:5278/Voluntariado/ListarVoluntariados");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var voluntariados = JsonConvert.DeserializeObject<List<Voluntariado>>(content);

                return View(voluntariados);
            }
            else
            {
                return View(new List<Voluntariado>());
            }
        }

        // GET: Voluntariados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"http://localhost:5278/Voluntariado/BuscarVoluntariado?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var voluntariado = JsonConvert.DeserializeObject<Voluntariado>(content);

                if (voluntariado == null)
                {
                    return NotFound();
                }

                return View(voluntariado);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: Voluntariados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Voluntariados/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Voluntariado voluntariado)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.PostAsJsonAsync("http://localhost:5278/Voluntariado/RegistrarVoluntariado", voluntariado);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(voluntariado);
                }
            }
            catch (Exception)
            {
                return View(voluntariado);
            }
        }

        // GET: Voluntariados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"http://localhost:5278/Voluntariado/BuscarVoluntariado?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var voluntariado = JsonConvert.DeserializeObject<Voluntariado>(content);

                if (voluntariado == null)
                {
                    return NotFound();
                }

                return View(voluntariado);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: Voluntariados/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Voluntariado voluntariado)
        {
            if (id != voluntariado.IdVoluntariado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = _httpClientFactory.CreateClient();
                    var response = await httpClient.PutAsJsonAsync($"http://localhost:5278/Voluntariado/EditarVoluntariado?id={id}", voluntariado);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return View(voluntariado);
                    }
                }
                catch (Exception)
                {
                    return View(voluntariado);
                }
            }
            return View(voluntariado);
        }

        // GET: Voluntariados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"http://localhost:5278/Voluntariado/BuscarVoluntariado?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var voluntariado = JsonConvert.DeserializeObject<Voluntariado>(content);

                if (voluntariado == null)
                {
                    return NotFound();
                }

                return View(voluntariado);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: Voluntariados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.DeleteAsync($"http://localhost:5278/Voluntariado/EliminarVoluntariado?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        private async Task<bool> VoluntariadoExistsAsync(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"http://localhost:5278/Voluntariado/BuscarVoluntariado?id={id}");

            return response.IsSuccessStatusCode;
        }
    }
}
