using personapi_dotne.Models.Entities;

namespace personapi_dotne.Repositories.Interfaces
{
    public interface IPersonaRepository
    {
        Task<IEnumerable<Persona>> GetAllPersonasAsync();
        Task<Persona?> GetPersonaByIdAsync(int id);

        Task CreatePersonaAsync(Persona persona);
        Task UpdatePersonaAsync(Persona persona);
        Task DeletePersonaAsync(int id);
    }
}