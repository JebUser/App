using IMCAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Services
{
    public interface ILineaprodService
    {
        Task<IEnumerable<Lineaprod>> GetLineaprodsAsync();
        Task<Lineaprod?> GetLineaprodByIdAsync(int id);
        Task AddLineaprodAsync(Lineaprod lineaprod);
        Task UpdateLineaprodAsync(Lineaprod lineaprod);
        Task DeleteLineaprodAsync(int id);
    }
}
