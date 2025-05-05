using personapi_dotne.Models.Entities;

namespace personapi_dotne.Repositories.Interfaces
{
    public interface ITelefonoRepository
    {
        Task<IEnumerable<Telefono>> GetAllTelefonosAsync();
        Task<Telefono> GetTelefonoByIdAsync(string num);
        Task CreateTelefonoAsync(Telefono telefono);
        Task UpdateTelefonoAsync(Telefono telefono);
        Task DeleteTelefonoAsync(string num);
    }
}
