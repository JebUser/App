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
    public class TipoapoyoRepository : ITipoapoyoRepository
    {
        private readonly DbContextIMC _context;

        public TipoapoyoRepository(DbContextIMC context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tipoapoyo>> GetTipoapoyosAsync()
        {
            return await _context.tipoapoyo.ToListAsync();
        }

        public async Task<Tipoapoyo?> GetTipoapoyoByIdAsync(int id)
        {
            return await _context.tipoapoyo.FindAsync(id);
        }

        public async Task AddTipoapoyoAsync(Tipoapoyo tipoapoyo)
        {
            _context.tipoapoyo.Add(tipoapoyo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTipoapoyoAsync(Tipoapoyo tipoapoyo)
        {
            _context.tipoapoyo.Update(tipoapoyo);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteTipoapoyoAsync(int id)
        {
            var tipoapoyo = await _context.tipoapoyo.FindAsync(id);
            if (tipoapoyo != null)
            {
                _context.tipoapoyo.Remove(tipoapoyo);
                await _context.SaveChangesAsync();
            }
        }
    }
}
