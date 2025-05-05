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
        private readonly ITipoProyectoService _tipoProyectoService;
        private readonly IMunicipioService _municipioService;

        public ProyectoService(IProyectoRepository proyectoRepository, IBeneficiarioRepository beneficiarioRepository, ITipoProyectoService tipoProyectoService, IMunicipioService municipioService)
        {
            _proyectoRepository = proyectoRepository;
            _beneficiarioRepository = beneficiarioRepository;
            _tipoProyectoService = tipoProyectoService;
            _municipioService = municipioService;
        }

        public bool TheIdsAreCorrect(Proyecto proyecto)
        {
            var tipoproyecto = _tipoProyectoService.GetTipoProyectoIdAsync(proyecto.Tipoid);
            if (tipoproyecto == null) return false;
            var municipio = _municipioService.GetMunicipioIdAsync(proyecto.Municipios_id);
            if (municipio == null) return false;
            return true;
        }

        public async Task<IEnumerable<ProyectoDto>> GetProyectosAsync()
        {
            var proyectos = await _proyectoRepository.GetProyectosAsync();

            return proyectos.Select(p => new ProyectoDto(p.Id, p.Nombre, p.municipio.Nombre, p.Fechainicio, p.FechaFinal)).OrderByDescending(p => p.Fechainicio);
        }
        public async Task<ProyectoDto?> GetProyectoIdAsync(int id)
        {
            var proyecto = await _proyectoRepository.GetProyectoIdAsync(id);
            if (proyecto == null) return null;
            return new ProyectoDto(proyecto.Id, proyecto.Nombre, proyecto.municipio.Nombre, proyecto.Fechainicio, proyecto.FechaFinal);
        }

        public async Task<IEnumerable<Beneficiario>> GetBeneficiariosInProyectoAsync(int proyectoid)
        {
            var beneficiarios = await _beneficiarioRepository.GetBeneficiariosAsync();
            var proyecto = await _proyectoRepository.GetBeneficiariosInProyectoAsync(proyectoid);
            if (beneficiarios == null || proyecto == null) return null; // No se puede hacer el join si no hay datos.
            var beneproys = proyecto.BeneficiarioActividads;
            var resultado = from bp in beneproys
                            join b in beneficiarios
                            on bp.Beneficiarios_id equals b.Id
                            select b;
            return resultado;
        }
        public async Task AddProyectoAsync(Proyecto proyecto)
        {
            await _proyectoRepository.AddProyectoAsync(proyecto); // Agrega el proyecto.
        }
        public async Task UpdateProyectoAsync(Proyecto proyecto)
        {
            await _proyectoRepository.UpdateProyectoAsync(proyecto); // Actualiza el proyecto.
        }
        public async Task DeleteProyectoAsync(int id)
        {
            await _proyectoRepository.DeleteProyectoAsync(id);
        }
    }
}
