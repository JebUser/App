using IMCAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Services
{
    public interface ISectorService
    {
        Task<IEnumerable<Sector>> GetSectoresAsync();
        Task<Sector?> GetSectorByIdAsync(int id);
        Task AddSectorAsync(Sector sector);
        Task UpdateSectorAsync(Sector sector);
        Task DeleteSectorAsync(int id);
    }
}
