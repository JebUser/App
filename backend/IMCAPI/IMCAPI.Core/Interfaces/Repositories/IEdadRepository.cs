using IMCAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Repositories
{
    public interface IEdadRepository
    {
        Task<IEnumerable<Edad>> GetEdadesAsync();
        Task<Edad?> GetEdadByIdAsync(int id);
        Task AddEdadAsync(Edad edad);
        Task UpdateEdadAsync(Edad edad);
        Task DeleteEdadAsync(int id);
    }
}
