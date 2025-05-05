using IMCAPI.Core.DTO;
using IMCAPI.Core.Entities;
using IMCAPI.Core.Interfaces.Repositories;
using IMCAPI.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Application.Services
{
    public class ActividadService : IActividadService
    {
        private readonly IActividadRepository _actividadRepository;
        private readonly ILugarRepository _lugarRepository;

        public ActividadService(IActividadRepository actividadRepository, ILugarRepository lugarRepository)
        {
            _actividadRepository = actividadRepository;
            _lugarRepository = lugarRepository;
        }

        public async Task<IEnumerable<ActividadDto>> GetActividadesAsync()
        {
            var actividades = await _actividadRepository.GetActividadesAsync();
            return actividades.Select(a =>
            new ActividadDto(a.Id, a.Nombre, a.FechaInicio, a.FechaFinal, a.lugar.Nombre, a.proyectos));
        }

        public async Task AddActividadAsync(ActividadDto actividaddto)
        {
            var lugar = await _lugarRepository.GetLugarNombreAsync(actividaddto.Lugar);
            if (lugar != null)
            {
                var actividad = new Actividad
                {
                    Id = actividaddto.Id,
                    Nombre = actividaddto.Nombre,
                    FechaInicio = actividaddto.FechaInicio,
                    FechaFinal = actividaddto.FechaFinal,
                    Lugares_id = lugar.Id,
                    lugar = lugar,
                    proyectos = actividaddto.proyectos
                };

                await _actividadRepository.AddActividadAsync(actividad);
            }
        }
        public async Task UpdateActividadAsync(ActividadDto actividaddto)
        {
            var lugar = await _lugarRepository.GetLugarNombreAsync(actividaddto.Lugar);
            if (lugar != null)
            {
                var actividad = new Actividad
                {
                    Id = actividaddto.Id,
                    Nombre = actividaddto.Nombre,
                    FechaInicio = actividaddto.FechaInicio,
                    FechaFinal = actividaddto.FechaFinal,
                    Lugares_id = lugar.Id,
                    lugar = lugar,
                    proyectos = actividaddto.proyectos
                };

                await _actividadRepository.UpdateActividadAsync(actividad);
            }
        }

        public async Task DeleteActividadAsync(int id)
        {
            await _actividadRepository.DeleteActividadAsync(id);
        }

    }
}
