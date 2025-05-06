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
    public class TipoactividadService : ITipoactividadService
    {
        private readonly ITipoactividadRepository _tipoactividadRepository;

        public TipoactividadService(ITipoactividadRepository tipoactividadRepository)
        {
            _tipoactividadRepository = tipoactividadRepository;
        }

        public async Task<IEnumerable<Tipoactividad>> GetTipoactividadesAsync()
        {
            return await _tipoactividadRepository.GetTipoactividadesAsync();
        }

        public async Task<Tipoactividad?> GetTipoactividadByIdAsync(int id)
        {
            return await _tipoactividadRepository.GetTipoactividadByIdAsync(id);
        }

        public async Task AddTipoactividadAsync(Tipoactividad tipoactividad)
        {
            await _tipoactividadRepository.AddTipoactividadAsync(tipoactividad);
        }

        public async Task UpdateTipoactividadAsync(Tipoactividad tipoactividad)
        {
            await _tipoactividadRepository.UpdateTipoactividadAsync(tipoactividad);
        }

        public async Task DeleteTipoactividadAsync(int id)
        {
            await _tipoactividadRepository.DeleteTipoactividadAsync(id);
        }
    }
}
