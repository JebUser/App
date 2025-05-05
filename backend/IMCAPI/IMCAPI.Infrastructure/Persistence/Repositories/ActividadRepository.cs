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
    public class ActividadRepository : IActividadRepository
    {
        private readonly DbContextIMC _context;

        public ActividadRepository(DbContextIMC context) { _context = context; }

        public async Task<IEnumerable<Actividad>> GetActividadesAsync()
        {
            return await _context.Actividades
                .Include(a => a.lugar)
                .Include(a => a.proyectos)
                .ToListAsync();
        }

        public async Task<Actividad?> GetActividadByIdAsync(int id)
        {
            return await _context.Actividades
                .Include(a => a.lugar)
                .Include(a => a.proyectos)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task AddActividadAsync(Actividad actividad)
        {
            _context.Actividades.Add(actividad);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateActividadAsync(Actividad actividad)
        {
            _context.Actividades.Update(actividad);
            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteActividadAsync(int id)
        {
            var actividad = await _context.Actividades.FindAsync(id);
            if (actividad != null)
            {
                _context.Actividades.Remove(actividad);
                await _context.SaveChangesAsync();
            }
        }
    }
}
