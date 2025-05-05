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
    public class EdadService : IEdadService
    {
        private readonly IEdadRepository _edadRepository;

        public EdadService(IEdadRepository edadRepository)
        {
            _edadRepository = edadRepository;
        }

        public async Task<IEnumerable<Edad>> GetEdadesAsync()
        {
            return await _edadRepository.GetEdadesAsync();
        }

        public async Task<Edad?> GetEdadByIdAsync(int id)
        {
            return await _edadRepository.GetEdadByIdAsync(id);
        }

        public async Task AddEdadAsync(Edad edad)
        {
            await _edadRepository.AddEdadAsync(edad);
        }

        public async Task UpdateEdadAsync(Edad edad)
        {
            await _edadRepository.UpdateEdadAsync(edad);
        }

        public async Task DeleteEdadAsync(int id)
        {
            await _edadRepository.DeleteEdadAsync(id);
        }
    }
}
