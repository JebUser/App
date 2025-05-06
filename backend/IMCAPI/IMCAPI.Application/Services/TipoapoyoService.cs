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
    public class TipoapoyoService : ITipoapoyoService
    {
        private readonly ITipoapoyoRepository _tipoapoyoRepository;

        public TipoapoyoService(ITipoapoyoRepository tipoapoyoRepository)
        {
            _tipoapoyoRepository = tipoapoyoRepository;
        }

        public async Task<IEnumerable<Tipoapoyo>> GetTipoapoyosAsync()
        {
            return await _tipoapoyoRepository.GetTipoapoyosAsync();
        }

        public async Task<Tipoapoyo?> GetTipoapoyoByIdAsync(int id)
        {
            return await _tipoapoyoRepository.GetTipoapoyoByIdAsync(id);
        }

        public async Task AddTipoapoyoAsync(Tipoapoyo tipoapoyo)
        {
            await _tipoapoyoRepository.AddTipoapoyoAsync(tipoapoyo);
        }

        public async Task UpdateTipoapoyoAsync(Tipoapoyo tipoapoyo)
        {
            await _tipoapoyoRepository.UpdateTipoapoyoAsync(tipoapoyo);
        }

        public async Task DeleteTipoapoyoAsync(int id)
        {
            await _tipoapoyoRepository.DeleteTipoapoyoAsync(id);
        }
    }
}
