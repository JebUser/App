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
    public class SectorRepository : ISectorRepository
    {
        private readonly DbContextIMC _context;

        public SectorRepository(DbContextIMC context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sector>> GetSectoresAsync()
        {
            return await _context.Sectores.ToListAsync();
        }

        public async Task<Sector?> GetSectorByIdAsync(int id)
        {
            return await _context.Sectores.FindAsync(id);
        }

        public async Task AddSectorAsync(Sector sector)
        {
            _context.Sectores.Add(sector);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSectorAsync(Sector sector)
        {
            _context.Sectores.Update(sector);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteSectorAsync(int id)
        {
            var sector = await _context.Sectores.FindAsync(id);
            if (sector != null)
            {
                _context.Sectores.Remove(sector);
                await _context.SaveChangesAsync();
            }
        }
    }
}
