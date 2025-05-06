using IMCAPI.Core.Interfaces.Services;
using IMCAPI.Core.Interfaces.Repositories;
using IMCAPI.Core.DTO;
using IMCAPI.Core.Entities;
namespace IMCAPI.Application.Services
{
    public class ProyectoService : IProyectoService
    {
        private readonly IProyectoRepository _proyectoRepository;

        public ProyectoService(IProyectoRepository proyectoRepository)
        {
            _proyectoRepository = proyectoRepository;
        }

        public async Task<IEnumerable<ProyectoDto>> GetProyectosAsync()
        {
            var proyectos = await _proyectoRepository.GetProyectosAsync();

            return proyectos.Select(p => new ProyectoDto(p.Id, p.Nombre, p.Fechainicio, p.FechaFinal, p.tipoproyecto, p.actividades)).OrderByDescending(p => p.Fechainicio);
        }
        public async Task<ProyectoDto?> GetProyectoIdAsync(int id)
        {
            var proyecto = await _proyectoRepository.GetProyectoIdAsync(id);
            if (proyecto == null) return null;
            return new ProyectoDto(proyecto.Id, proyecto.Nombre, proyecto.Fechainicio, proyecto.FechaFinal, proyecto.tipoproyecto, proyecto.actividades);
        }
        public async Task AddProyectoAsync(ProyectoDto proyectodto)
        {
            var proyecto = new Proyecto
            {
                Id = proyectodto.Id,
                Nombre = proyectodto.Nombre,
                Fechainicio = proyectodto.Fechainicio,
                FechaFinal = proyectodto.Fechafinal,
                Tipoid = proyectodto.tipoproyecto.Id,
                tipoproyecto = proyectodto.tipoproyecto,
                actividades = proyectodto.actividades
            };
            await _proyectoRepository.AddProyectoAsync(proyecto); // Agrega el proyecto.
        }
        public async Task UpdateProyectoAsync(ProyectoDto proyectodto)
        {
            var proyecto = new Proyecto
            {
                Id = proyectodto.Id,
                Nombre = proyectodto.Nombre,
                Fechainicio = proyectodto.Fechainicio,
                FechaFinal = proyectodto.Fechafinal,
                Tipoid = proyectodto.tipoproyecto.Id,
                tipoproyecto = proyectodto.tipoproyecto,
                actividades = proyectodto.actividades
            };
            await _proyectoRepository.UpdateProyectoAsync(proyecto); // Actualiza el proyecto.
        }
        public async Task DeleteProyectoAsync(int id)
        {
            await _proyectoRepository.DeleteProyectoAsync(id);
        }
    }
}
