using Microsoft.AspNetCore.Mvc;
using personapi_dotne.Models.Entities;
using personapi_dotne.Repositories.Interfaces;

namespace personapi_dotne.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesionApiController : ControllerBase
    {
        private readonly IProfesionRepository _profesionRepository;

        public ProfesionApiController(IProfesionRepository profesionRepository)
        {
            _profesionRepository = profesionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesion>>> GetAll()
        {
            var profesiones = await _profesionRepository.GetAllProfesionesAsync();
            return Ok(profesiones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Profesion>> Get(int id)
        {
            var profesion = await _profesionRepository.GetProfesionByIdAsync(id);
            return profesion == null ? NotFound() : Ok(profesion);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Profesion profesion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _profesionRepository.CreateProfesionAsync(profesion);
            return CreatedAtAction(nameof(Get), new { id = profesion.Id }, profesion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Profesion profesion)
        {
            if (id != profesion.Id)
                return BadRequest("El ID de la URL no coincide con el del modelo.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = await _profesionRepository.GetProfesionByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _profesionRepository.UpdateProfesionAsync(profesion);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var profesion = await _profesionRepository.GetProfesionByIdAsync(id);
            if (profesion == null)
                return NotFound();

            await _profesionRepository.DeleteProfesionAsync(id);
            return NoContent();
        }
    }
}
