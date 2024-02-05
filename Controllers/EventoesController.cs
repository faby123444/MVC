using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ConexionAppWeb_Apigateway.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ConexionAppWeb_Apigateway.Controllers
{
    public class EventoesController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EventoesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: Eventos
        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("http://localhost:5278/Evento/ListarEventos");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var eventos = JsonConvert.DeserializeObject<List<Evento>>(content);

                return View(eventos);
            }
            else
            {
                return View(new List<Evento>());
            }
        }

        // GET: Eventos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"http://localhost:5278/Evento/BuscarEvento?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var evento = JsonConvert.DeserializeObject<Evento>(content);

                if (evento == null)
                {
                    return NotFound();
                }

                return View(evento);
            }
            else
            {
                return View(new Evento());
            }
        }

        // GET: Eventos/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Evento evento)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.PostAsync($"http://localhost:5278/Evento/RegistrarEvento?" +
                                                           $"Título={Uri.EscapeDataString(evento.Título)}" +
                                                           $"&Fecha={evento.Fecha}" +
                                                           $"&Lugar={Uri.EscapeDataString(evento.Lugar)}" +
                                                           $"&Descripcion={Uri.EscapeDataString(evento.Descripcion)}" +
                                                           $"&Hora={evento.Hora}", null);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(evento);
                }
            }
            catch (Exception)
            {
                return View(evento);
            }
        }

        // GET: Eventos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"http://localhost:5278/Evento/BuscarEvento?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var evento = JsonConvert.DeserializeObject<Evento>(content);

                if (evento == null)
                {
                    return NotFound();
                }

                return View(evento);
            }
            else
            {
                return View(new Evento());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Evento evento)
        {
            if (id != evento.IdEvento)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = _httpClientFactory.CreateClient();
                    var response = await httpClient.PutAsync($"http://localhost:5278/Evento/EditarEvento?" +
                                                              $"id={id}" +
                                                              $"&Título={Uri.EscapeDataString(evento.Título)}" +
                                                              $"&Fecha={evento.Fecha}" +
                                                              $"&Lugar={Uri.EscapeDataString(evento.Lugar)}" +
                                                              $"&Descripcion={Uri.EscapeDataString(evento.Descripcion)}" +
                                                              $"&Hora={evento.Hora}", null);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return View(evento);
                    }
                }
                catch (Exception)
                {
                    return View(evento);
                }
            }
            return View(evento);
        }

        // GET: Eventos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"http://localhost:5278/Evento/BuscarEvento?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var evento = JsonConvert.DeserializeObject<Evento>(content);

                if (evento == null)
                {
                    return NotFound();
                }

                return View(evento);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.DeleteAsync($"http://localhost:5278/Evento/EliminarEvento?id={id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(new Evento());
                }
            }
            catch (Exception)
            {
                return View(new Evento());
            }
        }

        private async Task<bool> EventoExists(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"http://localhost:5278/Evento/BuscarEvento?id={id}");

            return response.IsSuccessStatusCode;
        }
    }
}
