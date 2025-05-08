using IMCAPI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Services
{
    public interface ITipoidenService
    {
        Task<IEnumerable<TipoidenDto>> GetTipoidensAsync();
        Task<TipoidenDto?> GetTipoidenByIdAsync(int id);
        Task AddTipoidenAsync(TipoidenDto tipoidendto);
        Task UpdateTipoidenAsync(TipoidenDto tipoidendto);
        Task DeleteTipoidenAsync(int id);
    }
}
