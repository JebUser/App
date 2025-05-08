using IMCAPI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Services
{
    public interface ITipoapoyoService
    {
        Task<IEnumerable<TipoapoyoDto>> GetTipoapoyosAsync();
        Task<TipoapoyoDto?> GetTipoapoyoByIdAsync(int id);
        Task AddTipoapoyoAsync(TipoapoyoDto tipoapoyodto);
        Task UpdateTipoapoyoAsync(TipoapoyoDto tipoapoyodto);
        Task DeleteTipoapoyoAsync(int id);
    }
}
