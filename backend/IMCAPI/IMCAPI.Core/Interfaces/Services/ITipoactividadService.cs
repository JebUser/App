using IMCAPI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Services
{
    public interface ITipoactividadService
    {
        Task<IEnumerable<TipoactividadDto>> GetTipoactividadesAsync();
        Task<TipoactividadDto?> GetTipoactividadByIdAsync(int id);
        Task AddTipoactividadAsync(TipoactividadDto tipoactividaddto);
        Task UpdateTipoactividadAsync(TipoactividadDto tipoactividaddto);
        Task DeleteTipoactividadAsync(int id);
    }
}