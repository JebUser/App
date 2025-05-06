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
    public class GeneroService : IGeneroService
    {
        private readonly IGeneroRepository _generoRepository;

        public GeneroService(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }

        public async Task<IEnumerable<Genero>> GetGenerosAsync()
        {
            return await _generoRepository.GetGenerosAsync();
        }

        public async Task<Genero?> GetGeneroByIdAsync(int id)
        {
            return await _generoRepository.GetGeneroByIdAsync(id);
        }

        public async Task AddGeneroAsync(Genero genero)
        {
            await _generoRepository.AddGeneroAsync(genero);
        }

        public async Task UpdateGeneroAsync(Genero genero)
        {
            await _generoRepository.UpdateGeneroAsync(genero);
        }

        public async Task DeleteGeneroAsync(int id)
        {
            await _generoRepository.DeleteGeneroAsync(id);
        }
    }
}
