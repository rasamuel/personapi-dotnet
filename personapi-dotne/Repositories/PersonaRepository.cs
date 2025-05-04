using Microsoft.EntityFrameworkCore;
using personapi_dotne.Models.Entities;

namespace personapi_dotne.Repositories
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly PersonaDbContext _context;

        public PersonaRepository(PersonaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Persona>> GetAllPersonasAsync()
        {
            return await _context.Personas.Include(p => p.Telefonos).Include(p => p.Estudios).ToListAsync();
        }

        public async Task<Persona> GetPersonaByIdAsync(int id)
        {
            return await _context.Personas.FindAsync(id);
        }

        public async Task CreatePersonaAsync(Persona persona)
        {
            _context.Personas.Add(persona);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePersonaAsync(Persona persona)
        {
            _context.Personas.Update(persona);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePersonaAsync(int id)
        {
            var persona = await _context.Personas.FindAsync(id);
            if (persona != null)
            {
                _context.Personas.Remove(persona);
                await _context.SaveChangesAsync();
            }
        }
    }
}
