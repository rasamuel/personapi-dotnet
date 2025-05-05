using Microsoft.AspNetCore.Mvc;
using personapi_dotne.Models.Entities;
using personapi_dotne.Repositories.Interfaces;

namespace personapi_dotne.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudioApiController : ControllerBase
    {
        private readonly IEstudioRepository _estudioRepository;
        private readonly IPersonaRepository _personaRepository;
        private readonly IProfesionRepository _profesionRepository;

        public EstudioApiController(
            IEstudioRepository estudioRepository,
            IPersonaRepository personaRepository,
            IProfesionRepository profesionRepository)
        {
            _estudioRepository = estudioRepository;
            _personaRepository = personaRepository;
            _profesionRepository = profesionRepository;
        }

        // GET: api/Estudio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estudio>>> GetAll()
        {
            var estudios = await _estudioRepository.GetAllEstudiosAsync();
            return Ok(estudios);
        }

        // GET: api/Estudio/1/1234
        [HttpGet("{idProf:int}/{ccPer:int}")]
        public async Task<ActionResult<Estudio>> Get(int idProf, int ccPer)
        {
            var estudio = await _estudioRepository.GetEstudioByIdsAsync(idProf, ccPer);
            if (estudio == null)
                return NotFound($"No se encontró el estudio con IdProf: {idProf} y CcPer: {ccPer}");

            return Ok(estudio);
        }

        // POST: api/Estudio
        [HttpPost]
        public async Task<ActionResult> Create(Estudio estudio)
        {
            if (estudio == null)
                return BadRequest("Datos inválidos");

            ModelState.Remove(nameof(estudio.CcPerNavigation));
            ModelState.Remove(nameof(estudio.IdProfNavigation));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            estudio.CcPerNavigation = await _personaRepository.GetPersonaByIdAsync(estudio.CcPer);
            estudio.IdProfNavigation = await _profesionRepository.GetProfesionByIdAsync(estudio.IdProf);

            if (estudio.CcPerNavigation == null || estudio.IdProfNavigation == null)
                return NotFound("Persona o profesión no encontrada");

            await _estudioRepository.CreateEstudioAsync(estudio);
            return CreatedAtAction(nameof(Get), new { idProf = estudio.IdProf, ccPer = estudio.CcPer }, estudio);
        }

        // PUT: api/Estudio/1/1234
        [HttpPut("{idProf:int}/{ccPer:int}")]
        public async Task<IActionResult> Update(int idProf, int ccPer, Estudio estudio)
        {
            if (idProf != estudio.IdProf || ccPer != estudio.CcPer)
                return BadRequest("Los identificadores no coinciden");

            ModelState.Remove(nameof(estudio.CcPerNavigation));
            ModelState.Remove(nameof(estudio.IdProfNavigation));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existente = await _estudioRepository.GetEstudioByIdsAsync(idProf, ccPer);
            if (existente == null)
                return NotFound("El estudio no existe");

            estudio.CcPerNavigation = await _personaRepository.GetPersonaByIdAsync(estudio.CcPer);
            estudio.IdProfNavigation = await _profesionRepository.GetProfesionByIdAsync(estudio.IdProf);

            if (estudio.CcPerNavigation == null || estudio.IdProfNavigation == null)
                return NotFound("Persona o profesión no encontrada");

            await _estudioRepository.UpdateEstudioAsync(estudio);
            return NoContent();
        }

        // DELETE: api/Estudio/1/1234
        [HttpDelete("{idProf:int}/{ccPer:int}")]
        public async Task<IActionResult> Delete(int idProf, int ccPer)
        {
            var estudio = await _estudioRepository.GetEstudioByIdsAsync(idProf, ccPer);
            if (estudio == null)
                return NotFound("El estudio no existe");

            await _estudioRepository.DeleteEstudioAsync(idProf, ccPer);
            return NoContent();
        }
    }
}
