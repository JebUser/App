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
    public class EdadRepository : IEdadRepository
    {
        private readonly DbContextIMC _context;

        public EdadRepository(DbContextIMC context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Edad>> GetEdadesAsync()
        {
            return await _context.Edades.ToListAsync();
        }

        public async Task<Edad?> GetEdadByIdAsync(int id)
        {
            return await _context.Edades.FindAsync(id);
        }

        public async Task AddEdadAsync(Edad edad)
        {
            _context.Edades.Add(edad);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEdadAsync(Edad edad)
        {
            _context.Edades.Update(edad);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteEdadAsync(int id)
        {
            var edad = await _context.Edades.FindAsync(id);
            if (edad != null)
            {
                _context.Edades.Remove(edad);
                await _context.SaveChangesAsync();
            }
        }
    }
}
