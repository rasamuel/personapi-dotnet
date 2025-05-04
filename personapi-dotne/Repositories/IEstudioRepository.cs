using personapi_dotne.Models.Entities;

namespace personapi_dotne.Repositories
{
    public interface IEstudioRepository
    {
        Task<IEnumerable<Estudio>> GetAllEstudiosAsync();
        Task<Estudio> GetEstudioByIdAsync(int id);
        Task CreateEstudioAsync(Estudio estudio);
        Task UpdateEstudioAsync(Estudio estudio);
        Task DeleteEstudioAsync(int id);
    }
}
