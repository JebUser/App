using IMCAPI.Core.Entities;
using IMCAPI.Core.DTO;
using IMCAPI.Core.Interfaces.Repositories;
using IMCAPI.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace IMCAPI.Application.Services
{
    public class TipoactividadService : ITipoactividadService
    {
        private readonly ITipoactividadRepository _tipoactividadRepository;

        public TipoactividadService(ITipoactividadRepository tipoactividadRepository)
        {
            _tipoactividadRepository = tipoactividadRepository;
        }

        public async Task<IEnumerable<TipoactividadDto>> GetTipoactividadesAsync()
        {
            var tipoactividades = await _tipoactividadRepository.GetTipoactividadesAsync();
            return tipoactividades.Select(ta => new TipoactividadDto(ta.Id, ta.Nombre));
        }

        public async Task<TipoactividadDto?> GetTipoactividadByIdAsync(int id)
        {
            var tipoactividad = await _tipoactividadRepository.GetTipoactividadByIdAsync(id);
            return new TipoactividadDto(tipoactividad.Id, tipoactividad.Nombre);
        }

        public async Task AddTipoactividadAsync(TipoactividadDto tipoactividaddto)
        {
            var tipoactividad = new Tipoactividad
            {
                Id = tipoactividaddto.Id,
                Nombre = tipoactividaddto.Nombre
            };
            await _tipoactividadRepository.AddTipoactividadAsync(tipoactividad);
        }

        public async Task UpdateTipoactividadAsync(TipoactividadDto tipoactividaddto)
        {
            var tipoactividad = await _tipoactividadRepository.GetTipoactividadByIdAsync(tipoactividaddto.Id);
            if (tipoactividad != null)
            {
                tipoactividad.Nombre = tipoactividaddto.Nombre;
                await _tipoactividadRepository.UpdateTipoactividadAsync(tipoactividad);
            }
        }

        public async Task DeleteTipoactividadAsync(int id)
        {
            await _tipoactividadRepository.DeleteTipoactividadAsync(id);
        }
    }
}
