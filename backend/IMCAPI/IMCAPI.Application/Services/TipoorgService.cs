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
    public class TipoorgService : ITipoorgService
    {
        private readonly ITipoorgRepository _tipoorgRepository;

        public TipoorgService(ITipoorgRepository tipoorgRepository)
        {
            _tipoorgRepository = tipoorgRepository;
        }

        public async Task<IEnumerable<TipoorgDto>> GetTipoorgsAsync()
        {
            var tipoorgs = await _tipoorgRepository.GetTipoorgsAsync();
            return tipoorgs.Select(to => new TipoorgDto(to.Id, to.Nombre));
        }

        public async Task<TipoorgDto?> GetTipoorgByIdAsync(int id)
        {
            var tipoorg = await _tipoorgRepository.GetTipoorgByIdAsync(id);
            return new TipoorgDto(tipoorg.Id, tipoorg.Nombre);
        }

        public async Task AddTipoorgAsync(TipoorgDto tipoorgdto)
        {
            var tipoorg = new Tipoorg
            {
                Id = tipoorgdto.Id,
                Nombre = tipoorgdto.Nombre
            };
            await _tipoorgRepository.AddTipoorgAsync(tipoorg);
        }

        public async Task UpdateTipoorgAsync(TipoorgDto tipoorgdto)
        {
            var tipoorg = await _tipoorgRepository.GetTipoorgByIdAsync(tipoorgdto.Id);
            if (tipoorg != null)
            {
                tipoorg.Nombre = tipoorgdto.Nombre;
                await _tipoorgRepository.UpdateTipoorgAsync(tipoorg);
            }
        }

        public async Task DeleteTipoorgAsync(int id)
        {
            await _tipoorgRepository.DeleteTipoorgAsync(id);
        }
    }
}
