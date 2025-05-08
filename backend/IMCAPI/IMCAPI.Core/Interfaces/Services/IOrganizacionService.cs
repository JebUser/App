using IMCAPI.Core.DTO;

namespace IMCAPI.Core.Interfaces.Services
{
    public interface IOrganizacionService
    {
        Task<IEnumerable<OrganizacionDto>> GetOrganizacionesAsync();
        Task<OrganizacionDto?> GetOrganizacionIdAsync(int id);
        Task AddOrganizacionAsync(OrganizacionDto organizacion);
        Task UpdateOrganizacionAsync(OrganizacionDto organizacion);
        Task DeleteOrganizacionAsync(int id);
    }
}
