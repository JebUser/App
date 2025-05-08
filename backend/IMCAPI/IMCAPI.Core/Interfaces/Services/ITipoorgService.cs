using IMCAPI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Services
{
    public interface ITipoorgService
    {
        Task<IEnumerable<TipoorgDto>> GetTipoorgsAsync();
        Task<TipoorgDto?> GetTipoorgByIdAsync(int id);
        Task AddTipoorgAsync(TipoorgDto tipoorgdto);
        Task UpdateTipoorgAsync(TipoorgDto tipoorgdto);
        Task DeleteTipoorgAsync(int id);
    }
}
