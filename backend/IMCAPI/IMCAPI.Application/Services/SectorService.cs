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
    public class SectorService : ISectorService
    {
        private readonly ISectorRepository _sectorRepository;

        public SectorService(ISectorRepository sectorRepository)
        {
            _sectorRepository = sectorRepository;
        }

        public async Task<IEnumerable<SectorDto>> GetSectoresAsync()
        {
            var sectores = await _sectorRepository.GetSectoresAsync();
            return sectores.Select(s => new SectorDto(s.Id, s.Nombre));
        }

        public async Task<SectorDto?> GetSectorByIdAsync(int id)
        {
            var sector = await _sectorRepository.GetSectorByIdAsync(id);
            return new SectorDto(sector.Id, sector.Nombre);
        }

        public async Task AddSectorAsync(SectorDto sectordto)
        {
            var sector = new Sector
            {
                Id = sectordto.Id,
                Nombre = sectordto.Nombre
            };
            await _sectorRepository.AddSectorAsync(sector);
        }

        public async Task UpdateSectorAsync(SectorDto sectordto)
        {
            var sector = await _sectorRepository.GetSectorByIdAsync(sectordto.Id);
            if (sector != null)
            {
                sector.Nombre = sectordto.Nombre;
                await _sectorRepository.UpdateSectorAsync(sector);
            }
        }

        public async Task DeleteSectorAsync(int id)
        {
            await _sectorRepository.DeleteSectorAsync(id);
        }
    }
}
