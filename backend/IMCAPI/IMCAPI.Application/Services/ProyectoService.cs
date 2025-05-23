using IMCAPI.Core.Interfaces.Services;
using IMCAPI.Core.Interfaces.Repositories;
using IMCAPI.Core.DTO;
using IMCAPI.Core.Entities;
using System.Text.Json;
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

            return proyectos.Select(p => new ProyectoDto(p.Id, p.Nombre, p.Fechainicio, p.FechaFinal, p.tipoproyecto != null ? new TipoproyectoDto(p.tipoproyecto.Id, p.tipoproyecto.Nombre) : null,
                p.actividades.Select(a => new ActividadDto(
                    a.Id,
                    a.Nombre,
                    a.FechaInicio,
                    a.FechaFinal,
                    new LugarDto(a.lugar.Id, a.lugar.Nombre),
                    null
                )).ToList()
            )).OrderByDescending(p => p.Fechainicio);
        }
        public async Task<ProyectoDto?> GetProyectoIdAsync(int id)
        {
            var proyecto = await _proyectoRepository.GetProyectoIdAsync(id);

            if (proyecto == null) return null;
            return new ProyectoDto(proyecto.Id, proyecto.Nombre, proyecto.Fechainicio, proyecto.FechaFinal, proyecto.tipoproyecto != null ? new TipoproyectoDto(proyecto.tipoproyecto.Id, proyecto.tipoproyecto.Nombre) : null, proyecto.actividades
                .Select(
                a => new ActividadDto(a.Id, a.Nombre, a.FechaInicio, a.FechaFinal, new LugarDto(a.lugar.Id, a.lugar.Nombre),null)
                ).ToList()
                );
        }
        public async Task AddProyectoAsync(ProyectoDto proyectodto)
        {
            var proyecto = new Proyecto
            {
                Id = proyectodto.Id,
                Nombre = proyectodto.Nombre,
                Fechainicio = proyectodto.Fechainicio,
                FechaFinal = proyectodto.Fechafinal,
                Tipoid = proyectodto.tipoproyecto != null ? proyectodto.tipoproyecto.Id : null,
                actividades = proyectodto.actividades.Select(a => new Actividad
                {
                    Id=a.Id,
                    Nombre = a.Nombre,
                    FechaInicio = a.FechaInicio,
                    FechaFinal = a.FechaFinal,
                    Lugares_id = a.lugar.Id
                }).ToList()
            };
            await _proyectoRepository.AddProyectoAsync(proyecto); // Agrega el proyecto.
        }
        public async Task UpdateProyectoAsync(ProyectoDto proyectodto)
        {
            var proyecto = await _proyectoRepository.GetProyectoIdAsync(proyectodto.Id);
            if (proyecto != null)
            {
                proyecto.Nombre = proyectodto.Nombre;
                proyecto.Fechainicio = proyectodto.Fechainicio;
                proyecto.FechaFinal = proyectodto.Fechafinal;
                proyecto.Tipoid = proyectodto.tipoproyecto != null ? proyectodto.tipoproyecto.Id : null;
                var actividadesIds = proyectodto.actividades.Select(a => a.Id).ToList();

                await _proyectoRepository.UpdateProyectoAsync(proyecto, actividadesIds); // Actualiza el proyecto.
            }
        }
        public async Task DeleteProyectoAsync(int id)
        {
            await _proyectoRepository.DeleteProyectoAsync(id);
        }
    }
}
