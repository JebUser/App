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
    public class TipoactividadRepository : ITipoactividadRepository
    {
        private readonly DbContextIMC _context;

        public TipoactividadRepository(DbContextIMC context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tipoactividad>> GetTipoactividadesAsync()
        {
            return await _context.tipoActividad.ToListAsync();
        }

        public async Task<Tipoactividad?> GetTipoactividadByIdAsync(int id)
        {
            return await _context.tipoActividad.FindAsync(id);
        }

        public async Task AddTipoactividadAsync(Tipoactividad tipoactividad)
        {
            _context.tipoActividad.Add(tipoactividad);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTipoactividadAsync(Tipoactividad tipoactividad)
        {
            _context.tipoActividad.Update(tipoactividad);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteTipoactividadAsync(int id)
        {
            var tipoactividad = await _context.tipoActividad.FindAsync(id);
            if (tipoactividad != null)
            {
                _context.tipoActividad.Remove(tipoactividad);
                await _context.SaveChangesAsync();
            }
        }
    }
}
