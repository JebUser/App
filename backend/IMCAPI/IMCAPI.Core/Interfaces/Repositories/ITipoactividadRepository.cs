using IMCAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Repositories
{
    public interface ITipoactividadRepository
    {
        Task<IEnumerable<Tipoactividad>> GetTipoactividadesAsync();
        Task<Tipoactividad?> GetTipoactividadByIdAsync(int id);
        Task AddTipoactividadAsync(Tipoactividad tipoactividad);
        Task UpdateTipoactividadAsync(Tipoactividad tipoactividad);
        Task DeleteTipoactividadAsync(int id);
    }
}