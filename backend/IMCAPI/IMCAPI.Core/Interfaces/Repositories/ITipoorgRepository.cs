using IMCAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Repositories
{
    public interface ITipoorgRepository
    {
        Task<IEnumerable<Tipoorg>> GetTipoorgsAsync();
        Task<Tipoorg?> GetTipoorgByIdAsync(int id);
        Task AddTipoorgAsync(Tipoorg tipoorg);
        Task UpdateTipoorgAsync(Tipoorg tipoorg);
        Task DeleteTipoorgAsync(int id);
    }
}
