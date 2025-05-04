using personapi_dotne.Models.Entities;
using personapi_dotne.Repositories.Interfaces;
using personapi_dotne.Services.Interfaces;

namespace personapi_dotne.Services
{
    public class TelefonoService : ITelefonoService
    {
        private readonly ITelefonoRepository _telefonoRepository;

        public TelefonoService(ITelefonoRepository telefonoRepository)
        {
            _telefonoRepository = telefonoRepository;
        }

        public async Task<IEnumerable<Telefono>> GetAllAsync()
        {
            return await _telefonoRepository.GetAllTelefonosAsync();
        }

        public async Task<Telefono?> GetByIdAsync(string num)
        {
            return await _telefonoRepository.GetTelefonoByIdAsync(num);
        }

        public async Task AddAsync(Telefono telefono)
        {
            await _telefonoRepository.CreateTelefonoAsync(telefono);
        }

        public async Task UpdateAsync(Telefono telefono)
        {
            await _telefonoRepository.UpdateTelefonoAsync(telefono);
        }

        public async Task DeleteAsync(string num)
        {
            await _telefonoRepository.DeleteTelefonoAsync(num);
        }
    }
}
