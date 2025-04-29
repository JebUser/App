using IMCAPI.Core.Entities;
using IMCAPI.Core.Interfaces.Repositories;
using IMCAPI.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Application.Services
{
    public class TipoProyectoService : ITipoProyectoService
    {
        private readonly ITipoProyectoRepository _tipoProyectoRepository;

        public TipoProyectoService(ITipoProyectoRepository tipoProyectoRepository)
        {
            _tipoProyectoRepository = tipoProyectoRepository;
        }

        public async Task<IEnumerable<Tipoproyecto>> GetTipoProyectosAsync()
        {
            var tipoproyectos = await _tipoProyectoRepository.GetTipoProyectosAsync();
            return tipoproyectos;
        }

        public async Task<Tipoproyecto?> GetTipoProyectoIdAsync(int id)
        {
            var tipoproyecto = await _tipoProyectoRepository.GetTipoProyectoIdAsync(id);
            if (tipoproyecto == null) return null;
            return tipoproyecto;
        }
        public async Task AddTipoProyectoAsync(Tipoproyecto tipoproyecto)
        {
            await _tipoProyectoRepository.AddTipoProyectoAsync(tipoproyecto); // Agrega el tipo de proyecto.
        }
    }
}