using IMCAPI.Core.Interfaces.Services;
using IMCAPI.Core.Interfaces.Repositories;
using IMCAPI.Core.DTO;
using IMCAPI.Core.Entities;
namespace IMCAPI.Application.Services
{
    public class ProyectoService : IProyectoService
    {
        private readonly IProyectoRepository _proyectoRepository;
        private readonly IBeneficiarioRepository _beneficiarioRepository;

        public ProyectoService(IProyectoRepository proyectoRepository, IBeneficiarioRepository beneficiarioRepository)
        {
            _proyectoRepository = proyectoRepository;
            _beneficiarioRepository = beneficiarioRepository;
        }

        public async Task<IEnumerable<ProyectoDto>> GetProyectosAsync()
        {
            var proyectos = await _proyectoRepository.GetProyectosAsync();

            return proyectos.Select(p => new ProyectoDto(p.Id, p.Nombre, p.municipio.Nombre, p.Fechainicio, p.FechaFinal));
        }

        public async Task<IEnumerable<Beneficiario>> GetBeneficiariosInProyectoAsync(int proyectoid)
        {
            var beneficiarios = await _beneficiarioRepository.GetBeneficiariosAsync();
            var proyecto = await _proyectoRepository.GetBeneficiariosInProyectoAsync(proyectoid);
            if (beneficiarios == null || proyecto == null) return null; // No se puede hacer el join si no hay datos.
            var beneproys = proyecto.beneficiarioProyectos;
            var resultado = from bp in beneproys
                            join b in beneficiarios
                            on bp.Beneficiarios_id equals b.Id
                            select b;
            return resultado;
        }
    }
}
