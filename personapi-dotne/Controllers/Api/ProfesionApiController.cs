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
            if (profesion == null) return NotFound();
            return Ok(profesion);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Profesion profesion)
        {
            await _profesionRepository.CreateProfesionAsync(profesion);
            return CreatedAtAction(nameof(Get), new { id = profesion.Id }, profesion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Profesion profesion)
        {
            if (id != profesion.Id) return BadRequest();
            await _profesionRepository.UpdateProfesionAsync(profesion);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _profesionRepository.DeleteProfesionAsync(id);
            return NoContent();
        }
    }
}
