using IMCAPI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Services
{
    public interface ITipoProyectoService
    {
        Task<IEnumerable<TipoproyectoDto>> GetTipoProyectosAsync();
        Task<TipoproyectoDto?> GetTipoProyectoIdAsync(int id);
        Task AddTipoProyectoAsync(TipoproyectoDto tipoproyectodto);
        Task UpdateTipoProyectoAsync(TipoproyectoDto tipoproyectodto);
        Task DeleteTipoProyectoAsync(int id);
    }
}
