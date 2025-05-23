using IMCAPI.Core.Entities;

namespace IMCAPI.Core.Interfaces.Repositories
{
    public interface IProyectoRepository
    {
        Task<IEnumerable<Proyecto>> GetProyectosAsync();
        Task<Proyecto?> GetProyectoIdAsync(int id);
        Task AddProyectoAsync(Proyecto proyecto);
        Task UpdateProyectoAsync(Proyecto proyecto, List<int> actividadesIds);
        Task DeleteProyectoAsync(int id);
    }
}
