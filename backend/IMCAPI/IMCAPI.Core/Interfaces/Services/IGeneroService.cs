using IMCAPI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Services
{
    public interface IGeneroService
    {
        Task<IEnumerable<GeneroDto>> GetGenerosAsync();
        Task<GeneroDto?> GetGeneroByIdAsync(int id);
        Task AddGeneroAsync(GeneroDto generodto);
        Task UpdateGeneroAsync(GeneroDto genero);
        Task DeleteGeneroAsync(int id);
    }
}
