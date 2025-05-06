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
    public class LineaprodService : ILineaprodService
    {
        private readonly ILineaprodRepository _lineaprodRepository;

        public LineaprodService(ILineaprodRepository lineaprodRepository)
        {
            _lineaprodRepository = lineaprodRepository;
        }

        public async Task<IEnumerable<Lineaprod>> GetLineaprodsAsync()
        {
            return await _lineaprodRepository.GetLineaprodsAsync();
        }

        public async Task<Lineaprod?> GetLineaprodByIdAsync(int id)
        {
            return await _lineaprodRepository.GetLineaprodByIdAsync(id);
        }

        public async Task AddLineaprodAsync(Lineaprod lineaprod)
        {
            await _lineaprodRepository.AddLineaprodAsync(lineaprod);
        }

        public async Task UpdateLineaprodAsync(Lineaprod lineaprod)
        {
            await _lineaprodRepository.UpdateLineaprodAsync(lineaprod);
        }

        public async Task DeleteLineaprodAsync(int id)
        {
            await _lineaprodRepository.DeleteLineaprodAsync(id);
        }
    }
}
