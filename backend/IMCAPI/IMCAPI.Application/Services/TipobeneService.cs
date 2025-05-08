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
    public class TipobeneService : ITipobeneService
    {
        private readonly ITipobeneRepository _tipobeneRepository;

        public TipobeneService(ITipobeneRepository tipobeneRepository)
        {
            _tipobeneRepository = tipobeneRepository;
        }

        public async Task<IEnumerable<TipobeneDto>> GetTipobenesAsync()
        {
            var tipobenes = await _tipobeneRepository.GetTipobenesAsync();
            return tipobenes.Select(tb => new TipobeneDto(tb.Id, tb.Nombre));
        }

        public async Task<TipobeneDto?> GetTipobeneByIdAsync(int id)
        {
            var tipobene = await _tipobeneRepository.GetTipobeneByIdAsync(id);
            return new TipobeneDto(tipobene.Id, tipobene.Nombre);
        }

        public async Task AddTipobeneAsync(TipobeneDto tipobenedto)
        {
            var tipobene = new Tipobene
            {
                Id = tipobenedto.Id,
                Nombre = tipobenedto.Nombre
            };
            await _tipobeneRepository.AddTipobeneAsync(tipobene);
        }

        public async Task UpdateTipobeneAsync(TipobeneDto tipobenedto)
        {
            var tipobene = await _tipobeneRepository.GetTipobeneByIdAsync(tipobenedto.Id);
            if (tipobene != null)
            {
                tipobene.Nombre = tipobenedto.Nombre;
                await _tipobeneRepository.UpdateTipobeneAsync(tipobene);
            }
        }

        public async Task DeleteTipobeneAsync(int id)
        {
            await _tipobeneRepository.DeleteTipobeneAsync(id);
        }
    }
}
