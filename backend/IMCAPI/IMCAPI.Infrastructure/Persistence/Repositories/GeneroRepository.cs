using IMCAPI.Core.Entities;
using IMCAPI.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Infrastructure.Persistence.Repositories
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly DbContextIMC _context;

        public GeneroRepository(DbContextIMC context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genero>> GetGenerosAsync()
        {
            return await _context.Generos.ToListAsync();
        }

        public async Task<Genero?> GetGeneroByIdAsync(int id)
        {
            return await _context.Generos.FindAsync(id);
        }

        public async Task AddGeneroAsync(Genero genero)
        {
            _context.Generos.Add(genero);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGeneroAsync(Genero genero)
        {
            _context.Generos.Update(genero);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteGeneroAsync(int id)
        {
            var genero = await _context.Generos.FindAsync(id);
            if (genero != null)
            {
                _context.Generos.Remove(genero);
                await _context.SaveChangesAsync();
            }
        }
    }
}
