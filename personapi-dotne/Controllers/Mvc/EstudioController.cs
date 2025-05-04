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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Estudio estudio)
        {
            Console.WriteLine("Intentando crear estudio:");
            Console.WriteLine($"IdProf: {estudio.IdProf}, CcPer: {estudio.CcPer}, Fecha: {estudio.Fecha}, Univer: {estudio.Univer}");

            // Quitar validación de propiedades de navegación que no vienen del formulario
            ModelState.Remove(nameof(estudio.CcPerNavigation));
            ModelState.Remove(nameof(estudio.IdProfNavigation));

            if (ModelState.IsValid)
            {
                // Cargar propiedades de navegación manualmente
                estudio.CcPerNavigation = await _personaRepository.GetPersonaByIdAsync(estudio.CcPer);
                estudio.IdProfNavigation = await _profesionRepository.GetProfesionByIdAsync(estudio.IdProf);

                Console.WriteLine("Persona encontrada: " + (estudio.CcPerNavigation != null ? "Sí" : "No"));
                Console.WriteLine("Profesión encontrada: " + (estudio.IdProfNavigation != null ? "Sí" : "No"));

                await _estudioRepository.CreateEstudioAsync(estudio);
                Console.WriteLine("Estudio creado correctamente.");
                return RedirectToAction(nameof(Index));
            }

            Console.WriteLine("ModelState inválido:");
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($" - {error.ErrorMessage}");
            }

            // Recargar SelectList en caso de error
            ViewData["CcPer"] = new SelectList(await _personaRepository.GetAllPersonasAsync(), "Cc", "Cc", estudio.CcPer);
            ViewData["IdProf"] = new SelectList(await _profesionRepository.GetAllProfesionesAsync(), "Id", "Nom", estudio.IdProf);
            return View(estudio);
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
            Console.WriteLine("Intentando editar estudio:");
            Console.WriteLine($"IdProf: {estudio.IdProf}, CcPer: {estudio.CcPer}, Fecha: {estudio.Fecha}, Univer: {estudio.Univer}");

            // Quitar validación de propiedades de navegación que no vienen del formulario
            ModelState.Remove(nameof(estudio.CcPerNavigation));
            ModelState.Remove(nameof(estudio.IdProfNavigation));

            if (idProf != estudio.IdProf || ccPer != estudio.CcPer)
            {
                Console.WriteLine("IDs en URL no coinciden con el modelo.");
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                estudio.CcPerNavigation = await _personaRepository.GetPersonaByIdAsync(estudio.CcPer);
                estudio.IdProfNavigation = await _profesionRepository.GetProfesionByIdAsync(estudio.IdProf);

                Console.WriteLine("ModelState válido. Actualizando estudio...");
                await _estudioRepository.UpdateEstudioAsync(estudio);
                return RedirectToAction(nameof(Index));
            }

            Console.WriteLine("ModelState inválido:");
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($" - {error.ErrorMessage}");
            }

            // Recargar SelectList en caso de error
            ViewData["CcPer"] = new SelectList(await _personaRepository.GetAllPersonasAsync(), "Cc", "Cc", estudio.CcPer);
            ViewData["IdProf"] = new SelectList(await _profesionRepository.GetAllProfesionesAsync(), "Id", "Nom", estudio.IdProf);
            return View(estudio);
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
