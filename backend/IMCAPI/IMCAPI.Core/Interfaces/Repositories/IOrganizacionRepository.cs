using IMCAPI.Core.Entities;

namespace IMCAPI.Core.Interfaces.Repositories
{
    public interface IOrganizacionRepository
    {
        Task<IEnumerable<Organizacion>> GetOrganizacionesAsync();
        Task<Organizacion?> GetOrganizacionIdAsync(int id);
        Task AddOrganizacionAsync(Organizacion organizacion);
        Task UpdateOrganizacionAsync(Organizacion organizacion);
        Task DeleteOrganizacionAsync(int id);
    }
}
