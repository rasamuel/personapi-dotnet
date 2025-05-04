using personapi_dotne.Models.Entities;

namespace personapi_dotne.Repositories
{
    public interface IProfesionRepository
    {
        Task<IEnumerable<Profesion>> GetAllProfesionesAsync();
        Task<Profesion> GetProfesionByIdAsync(int id);
        Task CreateProfesionAsync(Profesion profesion);
        Task UpdateProfesionAsync(Profesion profesion);
        Task DeleteProfesionAsync(int id);
    }
}
