using IMCAPI.Core.Entities;
using IMCAPI.Core.DTO;
using IMCAPI.Core.Interfaces.Repositories;
using IMCAPI.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Application.Services
{
    public class EdadService : IEdadService
    {
        private readonly IEdadRepository _edadRepository;

        public EdadService(IEdadRepository edadRepository)
        {
            _edadRepository = edadRepository;
        }

        public async Task<IEnumerable<EdadDto>> GetEdadesAsync()
        {
            var edades = await _edadRepository.GetEdadesAsync();
            return edades.Select(e => new EdadDto(e.Id, e.Rango));
        }

        public async Task<EdadDto?> GetEdadByIdAsync(int id)
        {
            var edad = await _edadRepository.GetEdadByIdAsync(id);
            return new EdadDto(edad.Id, edad.Rango);
        }

        public async Task AddEdadAsync(EdadDto edaddto)
        {
            var edad = new Edad
            {
                Id = edaddto.Id,
                Rango = edaddto.Rango
            };
            await _edadRepository.AddEdadAsync(edad);
        }

        public async Task UpdateEdadAsync(EdadDto edaddto)
        {
            var edad = await _edadRepository.GetEdadByIdAsync(edaddto.Id);
            if (edad != null)
            {
                edad.Rango = edaddto.Rango;
                await _edadRepository.UpdateEdadAsync(edad);
            }
        }

        public async Task DeleteEdadAsync(int id)
        {
            await _edadRepository.DeleteEdadAsync(id);
        }
    }
}
