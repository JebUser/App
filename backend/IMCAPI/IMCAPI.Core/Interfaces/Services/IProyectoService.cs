using IMCAPI.Core.DTO;
using IMCAPI.Core.Entities;
namespace IMCAPI.Core.Interfaces.Services
{
    public interface IProyectoService
    {
        Task<IEnumerable<ProyectoDto>> GetProyectosAsync();
        Task<IEnumerable<Beneficiario>> GetBeneficiariosInProyectoAsync(int proyectoid);
    }
}
