using IMCAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Services
{
    public interface ITipoidenService
    {
        Task<IEnumerable<Tipoiden>> GetTipoidensAsync();
        Task<Tipoiden?> GetTipoidenByIdAsync(int id);
        Task AddTipoidenAsync(Tipoiden tipoiden);
        Task UpdateTipoidenAsync(Tipoiden tipoiden);
        Task DeleteTipoidenAsync(int id);
    }
}
