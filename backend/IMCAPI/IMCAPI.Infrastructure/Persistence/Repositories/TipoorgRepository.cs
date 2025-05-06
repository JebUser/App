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
    public class TipoorgRepository : ITipoorgRepository
    {
        private readonly DbContextIMC _context;

        public TipoorgRepository(DbContextIMC context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tipoorg>> GetTipoorgsAsync()
        {
            return await _context.tipoorg.ToListAsync();
        }

        public async Task<Tipoorg?> GetTipoorgByIdAsync(int id)
        {
            return await _context.tipoorg.FindAsync(id);
        }

        public async Task AddTipoorgAsync(Tipoorg tipoorg)
        {
            _context.tipoorg.Add(tipoorg);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTipoorgAsync(Tipoorg tipoorg)
        {
            _context.tipoorg.Update(tipoorg);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteTipoorgAsync(int id)
        {
            var tipoorg = await _context.tipoorg.FindAsync(id);
            if (tipoorg != null)
            {
                _context.tipoorg.Remove(tipoorg);
                await _context.SaveChangesAsync();
            }
        }
    }
}
