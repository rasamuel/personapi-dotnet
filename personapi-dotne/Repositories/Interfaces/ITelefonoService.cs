using personapi_dotne.Models.Entities;

namespace personapi_dotne.Services.Interfaces
{
    public interface ITelefonoService
    {
        Task<IEnumerable<Telefono>> GetAllAsync();
        Task<Telefono?> GetByIdAsync(string num);
        Task AddAsync(Telefono telefono);
        Task UpdateAsync(Telefono telefono);
        Task DeleteAsync(string num);
    }
}
