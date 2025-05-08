using IMCAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Repositories
{
    public interface ITipoProyectoRepository
    {
        Task<IEnumerable<Tipoproyecto>> GetTipoProyectosAsync();
        Task<Tipoproyecto?> GetTipoProyectoIdAsync(int id);
        Task AddTipoProyectoAsync(Tipoproyecto tipoproyecto);
        Task UpdateTipoProyectoAsync(Tipoproyecto tipoproyecto);
        Task DeleteTipoProyectoAsync(int id);
    }
}
