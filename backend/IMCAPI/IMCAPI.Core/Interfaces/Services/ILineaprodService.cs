using IMCAPI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Services
{
    public interface ILineaprodService
    {
        Task<IEnumerable<LineaprodDto>> GetLineaprodsAsync();
        Task<LineaprodDto?> GetLineaprodByIdAsync(int id);
        Task AddLineaprodAsync(LineaprodDto lineaproddto);
        Task UpdateLineaprodAsync(LineaprodDto lineaproddto);
        Task DeleteLineaprodAsync(int id);
    }
}
