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
    public class TipobeneService : ITipobeneService
    {
        private readonly ITipobeneRepository _tipobeneRepository;

        public TipobeneService(ITipobeneRepository tipobeneRepository)
        {
            _tipobeneRepository = tipobeneRepository;
        }

        public async Task<IEnumerable<Tipobene>> GetTipobenesAsync()
        {
            return await _tipobeneRepository.GetTipobenesAsync();
        }

        public async Task<Tipobene?> GetTipobeneByIdAsync(int id)
        {
            return await _tipobeneRepository.GetTipobeneByIdAsync(id);
        }

        public async Task AddTipobeneAsync(Tipobene tipobene)
        {
            await _tipobeneRepository.AddTipobeneAsync(tipobene);
        }

        public async Task UpdateTipobeneAsync(Tipobene tipobene)
        {
            await _tipobeneRepository.UpdateTipobeneAsync(tipobene);
        }

        public async Task DeleteTipobeneAsync(int id)
        {
            await _tipobeneRepository.DeleteTipobeneAsync(id);
        }
    }
}
