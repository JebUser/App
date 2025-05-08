using IMCAPI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Services
{
    public interface ILugarService
    {
        Task<IEnumerable<LugarDto>> GetLugaresAsync();
        Task<LugarDto?> GetLugarNombreAsync(string nombre);
        Task AddLugarAsync(LugarDto lugardto);
        Task UpdateLugarAsync(LugarDto lugardto);
        Task DeleteLugarAsync(int id);
    }
}
