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
                .Include(p => p.municipio) // Agrega el municipio al que pertenece el proyecto.
                .Include(p => p.tipoproyecto) // Agrega su tipo de proyecto.
                .ToListAsync();
        }
        public async Task<Proyecto?> GetProyectoIdAsync(int id)
        {
            return await _context.Proyectos
                .Include(p => p.municipio) // Agrega el municipio al que pertenece el proyecto.
                .Include(p => p.tipoproyecto) // Agrega su tipo de proyecto.
                .Where(p => p.Id == id) // Busca un proyecto por su id.
                .FirstOrDefaultAsync(); 
        }

        public async Task<Proyecto?> GetBeneficiariosInProyectoAsync(int proyectoid)
        {
            return await _context.Proyectos
                .Include(p => p.beneficiarioProyectos) // Obtiene las relaciones con los beneficiarios.
                .Where(p => p.Id == proyectoid)
                .FirstOrDefaultAsync();
        }
        public async Task AddProyectoAsync(Proyecto proyecto)
        {
            _context.Proyectos.Add(proyecto); // Agrega un nuevo proyecto.
            await _context.SaveChangesAsync(); // Guarda los cambios.
        }
        public async Task UpdateProyectoAsync(Proyecto proyecto)
        {
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
