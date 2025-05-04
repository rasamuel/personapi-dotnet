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

        public EstudioApiController(IEstudioRepository estudioRepository)
        {
            _estudioRepository = estudioRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estudio>>> GetAll()
        {
            var estudios = await _estudioRepository.GetAllEstudiosAsync();
            return Ok(estudios);
        }

        [HttpGet("{idProf}/{ccPer}")]
        public async Task<ActionResult<Estudio>> Get(int idProf, int ccPer)
        {
            var estudio = await _estudioRepository.GetEstudioByIdsAsync(idProf, ccPer);
            if (estudio == null) return NotFound();
            return Ok(estudio);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Estudio estudio)
        {
            await _estudioRepository.CreateEstudioAsync(estudio);
            return CreatedAtAction(nameof(Get), new { idProf = estudio.IdProf, ccPer = estudio.CcPer }, estudio);
        }

        [HttpPut("{idProf}/{ccPer}")]
        public async Task<IActionResult> Update(int idProf, int ccPer, Estudio estudio)
        {
            if (idProf != estudio.IdProf || ccPer != estudio.CcPer) return BadRequest();
            await _estudioRepository.UpdateEstudioAsync(estudio);
            return NoContent();
        }

        [HttpDelete("{idProf}/{ccPer}")]
        public async Task<IActionResult> Delete(int idProf, int ccPer)
        {
            await _estudioRepository.DeleteEstudioAsync(idProf, ccPer);
            return NoContent();
        }
    }
}
