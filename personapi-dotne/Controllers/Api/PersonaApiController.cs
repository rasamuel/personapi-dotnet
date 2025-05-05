using Microsoft.AspNetCore.Mvc;
using personapi_dotne.Models.Entities;
using personapi_dotne.Repositories.Interfaces;

namespace personapi_dotne.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaApiController : ControllerBase
    {
        private readonly IPersonaRepository _personaRepository;

        public PersonaApiController(IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> GetAll()
        {
            var personas = await _personaRepository.GetAllPersonasAsync();
            return Ok(personas);
        }

        [HttpGet("{cc}")]
        public async Task<ActionResult<Persona>> Get(int cc)
        {
            var persona = await _personaRepository.GetPersonaByIdAsync(cc);
            return persona == null ? NotFound() : Ok(persona);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Persona persona)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _personaRepository.CreatePersonaAsync(persona);
            return CreatedAtAction(nameof(Get), new { cc = persona.Cc }, persona);
        }

        [HttpPut("{cc}")]
        public async Task<IActionResult> Update(int cc, Persona persona)
        {
            if (cc != persona.Cc)
                return BadRequest("El parámetro de ruta no coincide con el modelo.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _personaRepository.GetPersonaByIdAsync(cc);
            if (existing == null)
                return NotFound();

            await _personaRepository.UpdatePersonaAsync(persona);
            return NoContent();
        }

        [HttpDelete("{cc}")]
        public async Task<IActionResult> Delete(int cc)
        {
            var existing = await _personaRepository.GetPersonaByIdAsync(cc);
            if (existing == null)
                return NotFound();

            await _personaRepository.DeletePersonaAsync(cc);
            return NoContent();
        }
    }
}
