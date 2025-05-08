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
    public class GeneroService : IGeneroService
    {
        private readonly IGeneroRepository _generoRepository;

        public GeneroService(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }

        public async Task<IEnumerable<GeneroDto>> GetGenerosAsync()
        {
            var generos = await _generoRepository.GetGenerosAsync();
            return generos.Select(g => new GeneroDto(g.Id, g.Nombre));
        }

        public async Task<GeneroDto?> GetGeneroByIdAsync(int id)
        {
            var genero = await _generoRepository.GetGeneroByIdAsync(id);
            return new GeneroDto(genero.Id, genero.Nombre);
        }

        public async Task AddGeneroAsync(GeneroDto generodto)
        {
            var genero = new Genero
            {
                Id = generodto.Id,
                Nombre = generodto.Nombre
            };
            await _generoRepository.AddGeneroAsync(genero);
        }

        public async Task UpdateGeneroAsync(GeneroDto generodto)
        {
            var genero = await _generoRepository.GetGeneroByIdAsync(generodto.Id);
            if (genero != null)
            {
                genero.Nombre = generodto.Nombre;
                await _generoRepository.UpdateGeneroAsync(genero);
            }
        }

        public async Task DeleteGeneroAsync(int id)
        {
            await _generoRepository.DeleteGeneroAsync(id);
        }
    }
}
