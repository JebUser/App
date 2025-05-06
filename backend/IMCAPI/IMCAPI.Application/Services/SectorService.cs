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
    public class SectorService : ISectorService
    {
        private readonly ISectorRepository _sectorRepository;

        public SectorService(ISectorRepository sectorRepository)
        {
            _sectorRepository = sectorRepository;
        }

        public async Task<IEnumerable<Sector>> GetSectoresAsync()
        {
            return await _sectorRepository.GetSectoresAsync();
        }

        public async Task<Sector?> GetSectorByIdAsync(int id)
        {
            return await _sectorRepository.GetSectorByIdAsync(id);
        }

        public async Task AddSectorAsync(Sector sector)
        {
            await _sectorRepository.AddSectorAsync(sector);
        }

        public async Task UpdateSectorAsync(Sector sector)
        {
            await _sectorRepository.UpdateSectorAsync(sector);
        }

        public async Task DeleteSectorAsync(int id)
        {
            await _sectorRepository.DeleteSectorAsync(id);
        }
    }
}
