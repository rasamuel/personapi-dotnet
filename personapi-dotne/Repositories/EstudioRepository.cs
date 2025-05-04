using Microsoft.EntityFrameworkCore;
using personapi_dotne.Models.Entities;

namespace personapi_dotne.Repositories
{
    public class EstudioRepository : IEstudioRepository
    {
        private readonly PersonaDbContext _context;

        public EstudioRepository(PersonaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Estudio>> GetAllEstudiosAsync()
        {
            return await _context.Estudios.Include(e => e.CcPerNavigation).Include(e => e.IdProfNavigation).ToListAsync();
        }

        public async Task<Estudio> GetEstudioByIdAsync(int id)
        {
            return await _context.Estudios.FindAsync(id);
        }

        public async Task CreateEstudioAsync(Estudio estudio)
        {
            _context.Estudios.Add(estudio);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEstudioAsync(Estudio estudio)
        {
            _context.Estudios.Update(estudio);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEstudioAsync(int id)
        {
            var estudio = await _context.Estudios.FindAsync(id);
            if (estudio != null)
            {
                _context.Estudios.Remove(estudio);
                await _context.SaveChangesAsync();
            }
        }
    }
}
