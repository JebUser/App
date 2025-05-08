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
    public class LineaprodService : ILineaprodService
    {
        private readonly ILineaprodRepository _lineaprodRepository;

        public LineaprodService(ILineaprodRepository lineaprodRepository)
        {
            _lineaprodRepository = lineaprodRepository;
        }

        public async Task<IEnumerable<LineaprodDto>> GetLineaprodsAsync()
        {
            var lineaprods = await _lineaprodRepository.GetLineaprodsAsync();
            return lineaprods.Select(lp => new LineaprodDto(lp.Id, lp.Nombre));
        }

        public async Task<LineaprodDto?> GetLineaprodByIdAsync(int id)
        {
            var lineaprod = await _lineaprodRepository.GetLineaprodByIdAsync(id);
            return new LineaprodDto(lineaprod.Id, lineaprod.Nombre);
        }

        public async Task AddLineaprodAsync(LineaprodDto lineaproddto)
        {
            var lineaprod = new Lineaprod
            {
                Id = lineaproddto.Id,
                Nombre = lineaproddto.Nombre
            };
            await _lineaprodRepository.AddLineaprodAsync(lineaprod);
        }

        public async Task UpdateLineaprodAsync(LineaprodDto lineaproddto)
        {
            var lineaprod = await _lineaprodRepository.GetLineaprodByIdAsync(lineaproddto.Id);
            if (lineaprod != null)
            {
                lineaprod.Nombre = lineaproddto.Nombre;
                await _lineaprodRepository.UpdateLineaprodAsync(lineaprod);
            }
        }

        public async Task DeleteLineaprodAsync(int id)
        {
            await _lineaprodRepository.DeleteLineaprodAsync(id);
        }
    }
}
