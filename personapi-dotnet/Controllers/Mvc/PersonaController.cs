using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using personapi_dotne.Models.Entities;
using personapi_dotne.Repositories.Interfaces;

namespace personapi_dotne.Controllers.Mvc
{
    public class PersonaController : Controller
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonaController(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        public async Task<IActionResult> Index()
        {
            var personas = await _personaRepository.GetAllPersonasAsync();
            return View(personas);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return BadRequest();

            var persona = await _personaRepository.GetPersonaByIdAsync(id.Value);
            return persona == null ? NotFound() : View(persona);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Persona persona)
        {
            if (!ModelState.IsValid)
                return View(persona);

            await _personaRepository.CreatePersonaAsync(persona);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return BadRequest();

            var persona = await _personaRepository.GetPersonaByIdAsync(id.Value);
            return persona == null ? NotFound() : View(persona);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Persona persona)
        {
            if (id != persona.Cc)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(persona);

            try
            {
                await _personaRepository.UpdatePersonaAsync(persona);
            }
            catch (DbUpdateConcurrencyException)
            {
                var exists = await _personaRepository.GetPersonaByIdAsync(id);
                if (exists == null)
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var persona = await _personaRepository.GetPersonaByIdAsync(id.Value);
            return persona == null ? NotFound() : View(persona);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _personaRepository.DeletePersonaAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
