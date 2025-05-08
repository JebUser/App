using IMCAPI.Core.Entities;
using IMCAPI.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IMCAPI.Infrastructure.Persistence.Repositories
{
    public class OrganizacionRepository : IOrganizacionRepository
    {
        private readonly DbContextIMC _context;

        public OrganizacionRepository(DbContextIMC context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Organizacion>> GetOrganizacionesAsync()
        {
            return await _context.Organizaciones
                .Include(o => o.municipio)
                    .ThenInclude(m => m.departamento)
                .Include(o => o.tipoorg)
                .Include(o => o.tipoactividad)
                .Include(o => o.lineaprod)
                .Include(o => o.tipoapoyo)
                .ToListAsync();
        }

        public async Task<Organizacion?> GetOrganizacionIdAsync(int id)
        {
            return await _context.Organizaciones
                .Include(o => o.municipio)
                    .ThenInclude(m => m.departamento)
                .Include(o => o.tipoorg)
                .Include(o => o.tipoactividad)
                .Include(o => o.lineaprod)
                .Include(o => o.tipoapoyo)
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task AddOrganizacionAsync(Organizacion organizacion)
        {
            _context.Organizaciones.Add(organizacion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrganizacionAsync(Organizacion organizacion)
        {
            _context.Organizaciones.Update(organizacion);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrganizacionAsync(int id)
        {
            var organizacion = await _context.Organizaciones.FindAsync(id);

            if (organizacion != null) {
                _context.Organizaciones.Remove(organizacion);
                await _context.SaveChangesAsync();
            }
        }
    }
}
