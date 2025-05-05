using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMCAPI.Core.Entities;

namespace IMCAPI.Core.Interfaces.Repositories
{
    public interface IActividadRepository
    {
        Task<IEnumerable<Actividad>> GetActividadesAsync();
        Task<Actividad?> GetActividadByIdAsync(int id);
        Task AddActividadAsync(Actividad actividad);
        Task UpdateActividadAsync(Actividad actividad);
        Task DeleteActividadAsync(int id);
    }
}
