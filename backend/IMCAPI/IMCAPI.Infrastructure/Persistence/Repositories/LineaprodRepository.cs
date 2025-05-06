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
    public class LineaprodRepository : ILineaprodRepository
    {
        private readonly DbContextIMC _context;

        public LineaprodRepository(DbContextIMC context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Lineaprod>> GetLineaprodsAsync()
        {
            return await _context.lineaProd.ToListAsync();
        }

        public async Task<Lineaprod?> GetLineaprodByIdAsync(int id)
        {
            return await _context.lineaProd.FindAsync(id);
        }

        public async Task AddLineaprodAsync(Lineaprod lineaprod)
        {
            _context.lineaProd.Add(lineaprod);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLineaprodAsync(Lineaprod lineaprod)
        {
            _context.lineaProd.Update(lineaprod);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteLineaprodAsync(int id)
        {
            var lineaprod = await _context.lineaProd.FindAsync(id);
            if (lineaprod != null)
            {
                _context.lineaProd.Remove(lineaprod);
                await _context.SaveChangesAsync();
            }
        }
    }
}
