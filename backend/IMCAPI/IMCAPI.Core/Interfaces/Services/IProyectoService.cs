using IMCAPI.Core.DTO;
using IMCAPI.Core.Entities;
namespace IMCAPI.Core.Interfaces.Services
{
    public interface IProyectoService
    {
        Task<IEnumerable<ProyectoDto>> GetProyectosAsync();
        Task<ProyectoDto?> GetProyectoIdAsync(int id);
        Task AddProyectoAsync(ProyectoDto proyectodto);
        Task UpdateProyectoAsync(ProyectoDto proyectodto);
        Task DeleteProyectoAsync(int id);
    }
}
