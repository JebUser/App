using IMCAPI.Core.Entities;
using IMCAPI.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IMCAPI.Infrastructure.Persistence.Repositories
{
    public class ProyectoRepository : IProyectoRepository
    {
        private readonly DbContextIMC _context;

        public ProyectoRepository(DbContextIMC context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Proyecto>> GetProyectosAsync()
        {
            return await _context.Proyectos
                .Include(p => p.tipoproyecto) // Agrega su tipo de proyecto.
                .Include(p => p.actividades)
                    .ThenInclude(a => a.lugar)
                .ToListAsync();
        }
        public async Task<Proyecto?> GetProyectoIdAsync(int id)
        {
            return await _context.Proyectos
                .Include(p => p.tipoproyecto) // Agrega su tipo de proyecto.
                .Include (p => p.actividades)
                    .ThenInclude (a => a.lugar)
                .Where(p => p.Id == id) // Busca un proyecto por su id.
                .FirstOrDefaultAsync(); 
        }
        public async Task AddProyectoAsync(Proyecto proyecto)
        {
            _context.AttachRange(proyecto.actividades);
            _context.Proyectos.Add(proyecto); // Agrega un nuevo proyecto.
            await _context.SaveChangesAsync(); // Guarda los cambios.
        }
        public async Task UpdateProyectoAsync(Proyecto proyecto, List<int> actividadesIds)
        {
            var actividades = await _context.Actividades
                .Where(a => actividadesIds.Contains(a.Id))
                .ToListAsync();
            proyecto.actividades.Clear();
            foreach (var actividad in actividades)
            {
                proyecto.actividades.Add(actividad);
            }
            _context.Proyectos.Update(proyecto); // Actualiza el proyecto.
            await _context.SaveChangesAsync(); // Guarda los cambios.
        }
        public async Task DeleteProyectoAsync(int id)
        {
            var proyecto = await _context.Proyectos.FindAsync(id); // Busca si el proyecto existe.
            if (proyecto != null)
            {
                _context.Proyectos.Remove(proyecto); // Elimina el proyecto.
                await _context.SaveChangesAsync(); // Guarda los cambios.
            }
        }
    }
}
