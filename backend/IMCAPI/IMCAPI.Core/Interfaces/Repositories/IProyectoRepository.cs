using IMCAPI.Core.Entities;

namespace IMCAPI.Core.Interfaces.Repositories
{
    public interface IProyectoRepository
    {
        Task<IEnumerable<Proyecto>> GetProyectosAsync();
        Task<Proyecto?> GetBeneficiariosInProyectoAsync(int proyectoid);
    }
}
