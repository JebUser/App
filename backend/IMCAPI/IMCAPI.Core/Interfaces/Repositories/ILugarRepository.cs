using IMCAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Repositories
{
    public interface ILugarRepository
    {
        Task<IEnumerable<Lugar>> GetLugaresAsync();
        Task<Lugar?> GetLugarNombreAsync(string nombre);
        Task AddLugarAsync(Lugar lugar);
        Task UpdateLugarAsync(Lugar lugar);
        Task DeleteLugarAsync(int id);
    }
}
