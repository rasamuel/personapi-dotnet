using Microsoft.AspNetCore.Mvc;
using personapi_dotne.Models.Entities;
using personapi_dotne.Repositories.Interfaces;

namespace personapi_dotne.Controllers.Mvc
{
    public class ProfesionController : Controller
    {
        private readonly IProfesionRepository _profesionRepository;

        public ProfesionController(IProfesionRepository profesionRepository)
        {
            _profesionRepository = profesionRepository;
        }

        public async Task<IActionResult> Index()
        {
            var profesiones = await _profesionRepository.GetAllProfesionesAsync();
            return View(profesiones);
        }

        public async Task<IActionResult> Details(int id)
        {
            var profesion = await _profesionRepository.GetProfesionByIdAsync(id);
            if (profesion == null) return NotFound();
            return View(profesion);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Profesion profesion)
        {
            if (ModelState.IsValid)
            {
                await _profesionRepository.CreateProfesionAsync(profesion);
                return RedirectToAction(nameof(Index));
            }
            return View(profesion);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var profesion = await _profesionRepository.GetProfesionByIdAsync(id);
            if (profesion == null) return NotFound();
            return View(profesion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Profesion profesion)
        {
            if (id != profesion.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                await _profesionRepository.UpdateProfesionAsync(profesion);
                return RedirectToAction(nameof(Index));
            }
            return View(profesion);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var profesion = await _profesionRepository.GetProfesionByIdAsync(id);
            if (profesion == null) return NotFound();
            return View(profesion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _profesionRepository.DeleteProfesionAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
