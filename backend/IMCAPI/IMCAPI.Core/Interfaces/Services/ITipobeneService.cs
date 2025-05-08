using IMCAPI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Services
{
    public interface ITipobeneService
    {
        Task<IEnumerable<TipobeneDto>> GetTipobenesAsync();
        Task<TipobeneDto?> GetTipobeneByIdAsync(int id);
        Task AddTipobeneAsync(TipobeneDto tipobenedto);
        Task UpdateTipobeneAsync(TipobeneDto tipobenedto);
        Task DeleteTipobeneAsync(int id);
    }
}
