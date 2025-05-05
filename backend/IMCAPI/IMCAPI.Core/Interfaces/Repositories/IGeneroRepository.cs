using IMCAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Repositories
{
    public interface IGeneroRepository
    {
        Task<IEnumerable<Genero>> GetGenerosAsync();
        Task<Genero?> GetGeneroByIdAsync(int id);
        Task AddGeneroAsync(Genero genero);
        Task UpdateGeneroAsync(Genero genero);
        Task DeleteGeneroAsync(int id);
    }
}
