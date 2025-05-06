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
    public class TipoidenRepository : ITipoidenRepository
    {
        private readonly DbContextIMC _context;

        public TipoidenRepository(DbContextIMC context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tipoiden>> GetTipoidensAsync()
        {
            return await _context.tipoiden.ToListAsync();
        }

        public async Task<Tipoiden?> GetTipoidenByIdAsync(int id)
        {
            return await _context.tipoiden.FindAsync(id);
        }

        public async Task AddTipoidenAsync(Tipoiden tipoiden)
        {
            _context.tipoiden.Add(tipoiden);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTipoidenAsync(Tipoiden tipoiden)
        {
            _context.tipoiden.Update(tipoiden);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteTipoidenAsync(int id)
        {
            var tipoiden = await _context.tipoiden.FindAsync(id);
            if (tipoiden != null)
            {
                _context.tipoiden.Remove(tipoiden);
                await _context.SaveChangesAsync();
            }
        }
    }
}
