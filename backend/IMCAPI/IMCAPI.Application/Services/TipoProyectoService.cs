using IMCAPI.Core.Entities;
using IMCAPI.Core.DTO;
using IMCAPI.Core.Interfaces.Repositories;
using IMCAPI.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace IMCAPI.Application.Services
{
    public class TipoProyectoService : ITipoProyectoService
    {
        private readonly ITipoProyectoRepository _tipoProyectoRepository;

        public TipoProyectoService(ITipoProyectoRepository tipoProyectoRepository)
        {
            _tipoProyectoRepository = tipoProyectoRepository;
        }

        public async Task<IEnumerable<TipoproyectoDto>> GetTipoProyectosAsync()
        {
            var tipoproyectos = await _tipoProyectoRepository.GetTipoProyectosAsync();
            return tipoproyectos.Select(tp => new TipoproyectoDto(tp.Id, tp.Nombre));
        }

        public async Task<TipoproyectoDto?> GetTipoProyectoIdAsync(int id)
        {
            var tipoproyecto = await _tipoProyectoRepository.GetTipoProyectoIdAsync(id);
            if (tipoproyecto == null) return null;
            return new TipoproyectoDto(tipoproyecto.Id, tipoproyecto.Nombre);
        }
        public async Task AddTipoProyectoAsync(TipoproyectoDto tipoproyectodto)
        {
            var tipoproyecto = new Tipoproyecto
            {
                Id = tipoproyectodto.Id,
                Nombre = tipoproyectodto.Nombre
            };
            await _tipoProyectoRepository.AddTipoProyectoAsync(tipoproyecto); // Agrega el tipo de proyecto.
        }
        public async Task UpdateTipoProyectoAsync(TipoproyectoDto tipoproyectodto)
        {
            var tipoproyecto = await _tipoProyectoRepository.GetTipoProyectoIdAsync(tipoproyectodto.Id);
            if (tipoproyecto != null)
            {
                tipoproyecto.Nombre = tipoproyectodto.Nombre;
                await _tipoProyectoRepository.UpdateTipoProyectoAsync(tipoproyecto);
            }
        }
        public async Task DeleteTipoProyectoAsync(int id)
        {
            await _tipoProyectoRepository.DeleteTipoProyectoAsync(id);
        }
    }
}