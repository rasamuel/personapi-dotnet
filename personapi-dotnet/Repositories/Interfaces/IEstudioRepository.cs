using personapi_dotne.Models.Entities;

namespace personapi_dotne.Repositories.Interfaces
{
    public interface IEstudioRepository
    {
        Task<IEnumerable<Estudio>> GetAllEstudiosAsync();
        Task<Estudio?> GetEstudioByIdsAsync(int idProf, int ccPer);
        Task CreateEstudioAsync(Estudio estudio);
        Task UpdateEstudioAsync(Estudio estudio);
        Task DeleteEstudioAsync(int idProf, int ccPer);
    }
}
