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
    public class TipoidenService : ITipoidenService
    {
        private readonly ITipoidenRepository _tipoidenRepository;

        public TipoidenService(ITipoidenRepository tipoidenRepository)
        {
            _tipoidenRepository = tipoidenRepository;
        }

        public async Task<IEnumerable<Tipoiden>> GetTipoidensAsync()
        {
            return await _tipoidenRepository.GetTipoidensAsync();
        }

        public async Task<Tipoiden?> GetTipoidenByIdAsync(int id)
        {
            return await _tipoidenRepository.GetTipoidenByIdAsync(id);
        }

        public async Task AddTipoidenAsync(Tipoiden tipoiden)
        {
            await _tipoidenRepository.AddTipoidenAsync(tipoiden);
        }

        public async Task UpdateTipoidenAsync(Tipoiden tipoiden)
        {
            await _tipoidenRepository.UpdateTipoidenAsync(tipoiden);
        }

        public async Task DeleteTipoidenAsync(int id)
        {
            await _tipoidenRepository.DeleteTipoidenAsync(id);
        }
    }
}
