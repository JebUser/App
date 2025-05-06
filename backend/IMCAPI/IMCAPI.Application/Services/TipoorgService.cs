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
    public class TipoorgService : ITipoorgService
    {
        private readonly ITipoorgRepository _tipoorgRepository;

        public TipoorgService(ITipoorgRepository tipoorgRepository)
        {
            _tipoorgRepository = tipoorgRepository;
        }

        public async Task<IEnumerable<Tipoorg>> GetTipoorgsAsync()
        {
            return await _tipoorgRepository.GetTipoorgsAsync();
        }

        public async Task<Tipoorg?> GetTipoorgByIdAsync(int id)
        {
            return await _tipoorgRepository.GetTipoorgByIdAsync(id);
        }

        public async Task AddTipoorgAsync(Tipoorg tipoorg)
        {
            await _tipoorgRepository.AddTipoorgAsync(tipoorg);
        }

        public async Task UpdateTipoorgAsync(Tipoorg tipoorg)
        {
            await _tipoorgRepository.UpdateTipoorgAsync(tipoorg);
        }

        public async Task DeleteTipoorgAsync(int id)
        {
            await _tipoorgRepository.DeleteTipoorgAsync(id);
        }
    }
}
