using Microsoft.EntityFrameworkCore;  
using personapi_dotne.Models.Entities;
using personapi_dotne.Repositories.Interfaces;

namespace personapi_dotne.Repositories
{
    public class ProfesionRepository : IProfesionRepository
    {
        private readonly PersonaDbContext _context;

        public ProfesionRepository(PersonaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Profesion>> GetAllProfesionesAsync()
        {
            return await _context.Profesions.ToListAsync();
        }

        public async Task<Profesion> GetProfesionByIdAsync(int id)
        {
            return await _context.Profesions.FindAsync(id);
        }

        public async Task CreateProfesionAsync(Profesion profesion)
        {
            _context.Profesions.Add(profesion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProfesionAsync(Profesion profesion)
        {
            _context.Profesions.Update(profesion);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProfesionAsync(int id)
        {
            var profesion = await _context.Profesions.FindAsync(id);
            if (profesion != null)
            {
                _context.Profesions.Remove(profesion);
                await _context.SaveChangesAsync();
            }
        }
    }
}
