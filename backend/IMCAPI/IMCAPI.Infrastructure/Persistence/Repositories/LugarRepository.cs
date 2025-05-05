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
    public class LugarRepository : ILugarRepository
    {
        private readonly DbContextIMC _context;

        public LugarRepository(DbContextIMC context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Lugar>> GetLugaresAsync()
        {
            return await _context.Lugares.ToListAsync();
        }

        public async Task<Lugar?> GetLugarNombreAsync(string nombre)
        {
            return await _context.Lugares.Where(l => l.Nombre.ToLower().Contains(nombre)).FirstOrDefaultAsync();
        }

        public async Task AddLugarAsync(Lugar lugar)
        {
            _context.Lugares.Add(lugar);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLugarAsync(Lugar lugar)
        {
            _context.Lugares.Update(lugar);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLugarAsync(int id)
        {
            var lugar = await _context.Lugares.FindAsync(id);
            if (lugar != null)
            {
                _context.Lugares.Remove(lugar);
                await _context.SaveChangesAsync();
            }
        }
    }
}
