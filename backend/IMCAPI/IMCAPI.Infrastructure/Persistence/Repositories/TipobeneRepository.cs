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
    public class TipobeneRepository : ITipobeneRepository
    {
        private readonly DbContextIMC _context;

        public TipobeneRepository(DbContextIMC context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tipobene>> GetTipobenesAsync()
        {
            return await _context.tipoBene.ToListAsync();
        }

        public async Task<Tipobene?> GetTipobeneByIdAsync(int id)
        {
            return await _context.tipoBene.FindAsync(id);
        }

        public async Task AddTipobeneAsync(Tipobene tipobene)
        {
            _context.tipoBene.Add(tipobene);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTipobeneAsync(Tipobene tipobene)
        {
            _context.tipoBene.Update(tipobene);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteTipobeneAsync(int id)
        {
            var tipobene = await _context.tipoBene.FindAsync(id);
            if (tipobene != null)
            {
                _context.tipoBene.Remove(tipobene);
                await _context.SaveChangesAsync();
            }
        }
    }
}
