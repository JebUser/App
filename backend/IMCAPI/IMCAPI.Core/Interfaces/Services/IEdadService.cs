using IMCAPI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Services
{
    public interface IEdadService
    {
        Task<IEnumerable<EdadDto>> GetEdadesAsync();
        Task<EdadDto?> GetEdadByIdAsync(int id);
        Task AddEdadAsync(EdadDto edaddto);
        Task UpdateEdadAsync(EdadDto edaddto);
        Task DeleteEdadAsync(int id);
    }
}
