using IMCAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Services
{
    public interface ITipobeneService
    {
        Task<IEnumerable<Tipobene>> GetTipobenesAsync();
        Task<Tipobene?> GetTipobeneByIdAsync(int id);
        Task AddTipobeneAsync(Tipobene tipobene);
        Task UpdateTipobeneAsync(Tipobene tipobene);
        Task DeleteTipobeneAsync(int id);
    }
}
