using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using personapi_dotne.Models.Entities;
using personapi_dotne.Repositories.Interfaces;

namespace personapi_dotne.Controllers.Mvc
{
    public class EstudioController : Controller
    {
        private readonly IEstudioRepository _estudioRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly IProfesionRepository _profesionRepository;

        public EstudioController(IEstudioRepository estudioRepo, IPersonaRepository personaRepo, IProfesionRepository profesionRepo)
        {
            _estudioRepository = estudioRepo;
            _personaRepository = personaRepo;
            _profesionRepository = profesionRepo;
        }

        public async Task<IActionResult> Index()
        {
            var estudios = await _estudioRepository.GetAllEstudiosAsync();
            return View(estudios);
        }

        public async Task<IActionResult> Details(int idProf, int ccPer)
        {
            var estudio = await _estudioRepository.GetEstudioByIdsAsync(idProf, ccPer);
            if (estudio == null) return NotFound();
            return View(estudio);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["CcPer"] = new SelectList(await _personaRepository.GetAllPersonasAsync(), "Cc", "Cc");
            ViewData["IdProf"] = new SelectList(await _profesionRepository.GetAllProfesionesAsync(), "Id", "Nom");
            return View();
        }

        [HttpPost]XmlConfigurationExtensions|
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Estudio estudio)
        {
            if (ModelState.IsValid)
            {
                await _estudioRepository.CreateEstudioAsync(estudio);
                return RedirectToAction(nameof(Index));
            }
            return await Create(); // recarga los ViewData
        }

        public async Task<IActionResult> Edit(int idProf, int ccPer)
        {
            var estudio = await _estudioRepository.GetEstudioByIdsAsync(idProf, ccPer);
            if (estudio == null) return NotFound();

            ViewData["CcPer"] = new SelectList(await _personaRepository.GetAllPersonasAsync(), "Cc", "Cc", estudio.CcPer);
            ViewData["IdProf"] = new SelectList(await _profesionRepository.GetAllProfesionesAsync(), "Id", "Nom", estudio.IdProf);
            return View(estudio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int idProf, int ccPer, Estudio estudio)
        {
            if (idProf != estudio.IdProf || ccPer != estudio.CcPer) return BadRequest();

            if (ModelState.IsValid)
            {
                await _estudioRepository.UpdateEstudioAsync(estudio);
                return RedirectToAction(nameof(Index));
            }
            return await Edit(idProf, ccPer);
        }

        public async Task<IActionResult> Delete(int idProf, int ccPer)
        {
            var estudio = await _estudioRepository.GetEstudioByIdsAsync(idProf, ccPer);
            if (estudio == null) return NotFound();
            return View(estudio);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int idProf, int ccPer)
        {
            await _estudioRepository.DeleteEstudioAsync(idProf, ccPer);
            return RedirectToAction(nameof(Index));
        }
    }
}
