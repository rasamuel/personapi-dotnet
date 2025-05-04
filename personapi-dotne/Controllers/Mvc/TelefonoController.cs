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
            Console.WriteLine("Intentando crear teléfono:");
            Console.WriteLine($"Num: {telefono.Num}, Oper: {telefono.Oper}, Duenio: {telefono.Duenio}");

            // Quita la validación de la propiedad de navegación
            ModelState.Remove(nameof(telefono.DuenioNavigation));

            if (ModelState.IsValid)
            {
                telefono.DuenioNavigation = await _personaRepository.GetPersonaByIdAsync(telefono.Duenio);

                await _telefonoRepository.CreateTelefonoAsync(telefono);
                Console.WriteLine("Teléfono creado correctamente.");
                return RedirectToAction(nameof(Index));
            }

            Console.WriteLine("ModelState inválido:");
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($" - {error.ErrorMessage}");
            }

            var personas = await _personaRepository.GetAllPersonasAsync();
            ViewData["Duenio"] = new SelectList(personas, "Cc", "Cc", telefono.Duenio);
            return View(telefono);
        }




        public async Task<IActionResult> Edit(string num)
        {
            var telefono = await _telefonoRepository.GetTelefonoByIdAsync(num);
            if (telefono == null)
            {
                Console.WriteLine($"❌ No se encontró el teléfono con número: {num}");
                return NotFound();
            }

            var personas = await _personaRepository.GetAllPersonasAsync();
            ViewData["Duenio"] = new SelectList(personas, "Cc", "Cc", telefono.Duenio);
            return View(telefono);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string num, Telefono telefono)
        {
            Console.WriteLine("Intentando editar teléfono...");
            Console.WriteLine($"Num: {telefono.Num}, Oper: {telefono.Oper}, Duenio: {telefono.Duenio}");

            if (num != telefono.Num)
            {
                Console.WriteLine("❌ El número de teléfono en la URL no coincide con el del modelo.");
                return BadRequest();
            }

            // Evita validar la propiedad de navegación
            ModelState.Remove(nameof(telefono.DuenioNavigation));

            if (ModelState.IsValid)
            {
                // Carga explícita de la navegación
                telefono.DuenioNavigation = await _personaRepository.GetPersonaByIdAsync(telefono.Duenio);

                await _telefonoRepository.UpdateTelefonoAsync(telefono);
                Console.WriteLine("✅ Teléfono actualizado correctamente.");
                return RedirectToAction(nameof(Index));
            }

            Console.WriteLine("❌ ModelState inválido:");
            foreach (var entry in ModelState)
            {
                foreach (var error in entry.Value.Errors)
                {
                    Console.WriteLine($" - Campo: {entry.Key}, Error: {error.ErrorMessage}");
                }
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
