using IMCAPI.Core.Entities;
using IMCAPI.Core.DTO;
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

        public async Task<IEnumerable<TipoapoyoDto>> GetTipoapoyosAsync()
        {
            var tipoapoyos = await _tipoapoyoRepository.GetTipoapoyosAsync();
            return tipoapoyos.Select(ta => new TipoapoyoDto(ta.Id, ta.Nombre));
        }

        public async Task<TipoapoyoDto?> GetTipoapoyoByIdAsync(int id)
        {
            var tipoapoyo = await _tipoapoyoRepository.GetTipoapoyoByIdAsync(id);
            return new TipoapoyoDto(tipoapoyo.Id, tipoapoyo.Nombre);
        }

        public async Task AddTipoapoyoAsync(TipoapoyoDto tipoapoyodto)
        {
            var tipoapoyo = new Tipoapoyo
            {
                Id = tipoapoyodto.Id,
                Nombre = tipoapoyodto.Nombre
            };
            await _tipoapoyoRepository.AddTipoapoyoAsync(tipoapoyo);
        }

        public async Task UpdateTipoapoyoAsync(TipoapoyoDto tipoapoyodto)
        {
            var tipoapoyo = await _tipoapoyoRepository.GetTipoapoyoByIdAsync(tipoapoyodto.Id);
            if (tipoapoyo != null)
            {
                tipoapoyo.Nombre = tipoapoyodto.Nombre;
                await _tipoapoyoRepository.UpdateTipoapoyoAsync(tipoapoyo);
            }
        }

        public async Task DeleteTipoapoyoAsync(int id)
        {
            await _tipoapoyoRepository.DeleteTipoapoyoAsync(id);
        }
    }
}
