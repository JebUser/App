using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMCAPI.Core.DTO;
using IMCAPI.Core.Entities;

namespace IMCAPI.Core.Interfaces.Services
{
    public interface IActividadService
    {
        Task<IEnumerable<ActividadDto>> GetActividadesAsync();
        Task AddActividadAsync(ActividadDto actividaddto);
        Task UpdateActividadAsync(ActividadDto actividaddto);
        Task DeleteActividadAsync(int id);
    }
}
