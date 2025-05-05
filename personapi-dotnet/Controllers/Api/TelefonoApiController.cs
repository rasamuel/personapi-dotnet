using Microsoft.AspNetCore.Mvc;
using personapi_dotne.Models.Entities;
using personapi_dotne.Repositories.Interfaces;

namespace personapi_dotne.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelefonoApiController : ControllerBase
    {
        private readonly ITelefonoRepository _telefonoRepository;
        private readonly IPersonaRepository _personaRepository;

        public TelefonoApiController(
            ITelefonoRepository telefonoRepository,
            IPersonaRepository personaRepository)
        {
            _telefonoRepository = telefonoRepository;
            _personaRepository = personaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Telefono>>> GetAll()
        {
            var telefonos = await _telefonoRepository.GetAllTelefonosAsync();
            return Ok(telefonos);
        }

        [HttpGet("{num}")]
        public async Task<ActionResult<Telefono>> Get(string num)
        {
            var telefono = await _telefonoRepository.GetTelefonoByIdAsync(num);
            return telefono == null ? NotFound($"No se encontró el teléfono con número {num}") : Ok(telefono);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Telefono telefono)
        {
            if (telefono == null || string.IsNullOrWhiteSpace(telefono.Num))
                return BadRequest("Número de teléfono inválido");

            ModelState.Remove(nameof(telefono.DuenioNavigation));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var persona = await _personaRepository.GetPersonaByIdAsync(telefono.Duenio);
            if (persona == null)
                return NotFound("La persona asociada al teléfono no existe");

            telefono.DuenioNavigation = persona;

            await _telefonoRepository.CreateTelefonoAsync(telefono);
            return CreatedAtAction(nameof(Get), new { num = telefono.Num }, telefono);
        }

        [HttpPut("{num}")]
        public async Task<IActionResult> Update(string num, Telefono telefono)
        {
            if (telefono == null || num != telefono.Num)
                return BadRequest("Número en la ruta no coincide con el del modelo");

            ModelState.Remove(nameof(telefono.DuenioNavigation));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var persona = await _personaRepository.GetPersonaByIdAsync(telefono.Duenio);
            if (persona == null)
                return NotFound("La persona asociada al teléfono no existe");

            var existingTelefono = await _telefonoRepository.GetTelefonoByIdAsync(num);
            if (existingTelefono == null)
                return NotFound($"El teléfono {num} no existe");

            telefono.DuenioNavigation = persona;
            await _telefonoRepository.UpdateTelefonoAsync(telefono);

            return NoContent();
        }

        [HttpDelete("{num}")]
        public async Task<IActionResult> Delete(string num)
        {
            var telefono = await _telefonoRepository.GetTelefonoByIdAsync(num);
            if (telefono == null)
                return NotFound($"No se encontró el teléfono con número {num}");

            await _telefonoRepository.DeleteTelefonoAsync(num);
            return NoContent();
        }
    }
}
