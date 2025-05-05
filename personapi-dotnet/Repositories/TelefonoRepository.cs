using Microsoft.EntityFrameworkCore;
using personapi_dotne.Models.Entities;
using personapi_dotne.Repositories.Interfaces;

namespace personapi_dotne.Repositories
{
    public class TelefonoRepository : ITelefonoRepository
    {
        private readonly PersonaDbContext _context;

        public TelefonoRepository(PersonaDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Telefono>> GetAllTelefonosAsync()
        {
            return await _context.Telefonos.ToListAsync();
        }

        public async Task<Telefono> GetTelefonoByIdAsync(string num)
        {
            return await _context.Telefonos.FindAsync(num);
        }

        public async Task CreateTelefonoAsync(Telefono telefono)
        {
            _context.Telefonos.Add(telefono);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTelefonoAsync(Telefono telefono)
        {
            _context.Telefonos.Update(telefono);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTelefonoAsync(string num)
        {
            var telefono = await _context.Telefonos.FindAsync(num);
            if (telefono != null)
            {
                _context.Telefonos.Remove(telefono);
                await _context.SaveChangesAsync();
            }
        }
    }
}
