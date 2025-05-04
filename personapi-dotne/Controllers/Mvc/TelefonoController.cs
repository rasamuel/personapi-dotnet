using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using personapi_dotne.Models.Entities;
using personapi_dotne.Repositories.Interfaces;

namespace personapi_dotne.Controllers.Mvc
{
    public class TelefonoController : Controller
    {
        private readonly ITelefonoRepository _telefonoRepository;
        private readonly IPersonaRepository _personaRepository;

        public TelefonoController(ITelefonoRepository telefonoRepository, IPersonaRepository personaRepository)
        {
            _telefonoRepository = telefonoRepository;
            _personaRepository = personaRepository;
        }

        public async Task<IActionResult> Index()
        {
            var telefonos = await _telefonoRepository.GetAllTelefonosAsync();
            return View(telefonos);
        }

        public async Task<IActionResult> Details(string num)
        {
            var telefono = await _telefonoRepository.GetTelefonoByIdAsync(num);
            if (telefono == null) return NotFound();
            return View(telefono);
        }

        public async Task<IActionResult> Create()
        {
            var personas = await _personaRepository.GetAllPersonasAsync();
            ViewData["Duenio"] = new SelectList(personas, "Cc", "Cc");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Telefono telefono)
        {
            if (ModelState.IsValid)
            {
                await _telefonoRepository.CreateTelefonoAsync(telefono);
                return RedirectToAction(nameof(Index));
            }

            var personas = await _personaRepository.GetAllPersonasAsync();
            ViewData["Duenio"] = new SelectList(personas, "Cc", "Cc", telefono.Duenio);
            return View(telefono);
        }

        public async Task<IActionResult> Edit(string num)
        {
            var telefono = await _telefonoRepository.GetTelefonoByIdAsync(num);
            if (telefono == null) return NotFound();

            var personas = await _personaRepository.GetAllPersonasAsync();
            ViewData["Duenio"] = new SelectList(personas, "Cc", "Cc", telefono.Duenio);
            return View(telefono);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string num, Telefono telefono)
        {
            if (num != telefono.Num) return BadRequest();

            if (ModelState.IsValid)
            {
                await _telefonoRepository.UpdateTelefonoAsync(telefono);
                return RedirectToAction(nameof(Index));
            }

            var personas = await _personaRepository.GetAllPersonasAsync();
            ViewData["Duenio"] = new SelectList(personas, "Cc", "Cc", telefono.Duenio);
            return View(telefono);
        }

        public async Task<IActionResult> Delete(string num)
        {
            var telefono = await _telefonoRepository.GetTelefonoByIdAsync(num);
            if (telefono == null) return NotFound();
            return View(telefono);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string num)
        {
            await _telefonoRepository.DeleteTelefonoAsync(num);
            return RedirectToAction(nameof(Index));
        }
    }
}
