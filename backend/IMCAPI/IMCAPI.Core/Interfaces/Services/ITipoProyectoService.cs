using IMCAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Services
{
    public interface ITipoProyectoService
    {
        Task<IEnumerable<Tipoproyecto>> GetTipoProyectosAsync();
        Task<Tipoproyecto?> GetTipoProyectoIdAsync(int id);
        Task AddTipoProyectoAsync(Tipoproyecto tipoproyecto);
    }
}
