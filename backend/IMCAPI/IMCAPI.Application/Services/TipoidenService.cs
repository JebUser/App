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
    public class TipoidenService : ITipoidenService
    {
        private readonly ITipoidenRepository _tipoidenRepository;

        public TipoidenService(ITipoidenRepository tipoidenRepository)
        {
            _tipoidenRepository = tipoidenRepository;
        }

        public async Task<IEnumerable<TipoidenDto>> GetTipoidensAsync()
        {
            var tipoidens = await _tipoidenRepository.GetTipoidensAsync();
            return tipoidens.Select(ti => new TipoidenDto(ti.Id, ti.Nombre));
        }

        public async Task<TipoidenDto?> GetTipoidenByIdAsync(int id)
        {
            var tipoiden = await _tipoidenRepository.GetTipoidenByIdAsync(id);
            return new TipoidenDto(tipoiden.Id, tipoiden.Nombre);
        }

        public async Task AddTipoidenAsync(TipoidenDto tipoidendto)
        {
            var tipoiden = new Tipoiden
            {
                Id = tipoidendto.Id,
                Nombre = tipoidendto.Nombre
            };
            await _tipoidenRepository.AddTipoidenAsync(tipoiden);
        }

        public async Task UpdateTipoidenAsync(TipoidenDto tipoidendto)
        {
            var tipoiden = await _tipoidenRepository.GetTipoidenByIdAsync(tipoidendto.Id);
            if (tipoiden != null)
            {
                tipoiden.Nombre = tipoidendto.Nombre;
                await _tipoidenRepository.UpdateTipoidenAsync(tipoiden);
            }
        }

        public async Task DeleteTipoidenAsync(int id)
        {
            await _tipoidenRepository.DeleteTipoidenAsync(id);
        }
    }
}
