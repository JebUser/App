using IMCAPI.Core.DTO;
using IMCAPI.Core.Entities;
namespace IMCAPI.Core.Interfaces.Services
{
    public interface IProyectoService
    {
        bool TheIdsAreCorrect(Proyecto proyecto);
        Task<IEnumerable<ProyectoDto>> GetProyectosAsync();
        Task<ProyectoDto?> GetProyectoIdAsync(int id);
        Task<IEnumerable<Beneficiario>> GetBeneficiariosInProyectoAsync(int proyectoid);
        Task AddProyectoAsync(Proyecto proyecto);
        Task UpdateProyectoAsync(Proyecto proyecto);
        Task DeleteProyectoAsync(int id);
    }
}
