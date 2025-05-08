using IMCAPI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Services
{
    public interface ISectorService
    {
        Task<IEnumerable<SectorDto>> GetSectoresAsync();
        Task<SectorDto?> GetSectorByIdAsync(int id);
        Task AddSectorAsync(SectorDto sectordto);
        Task UpdateSectorAsync(SectorDto sectordto);
        Task DeleteSectorAsync(int id);
    }
}
