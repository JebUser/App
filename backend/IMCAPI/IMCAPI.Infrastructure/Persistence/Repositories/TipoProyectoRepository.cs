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
    public class TipoProyectoRepository : ITipoProyectoRepository
    {
        private readonly DbContextIMC _context;

        public TipoProyectoRepository(DbContextIMC context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tipoproyecto>> GetTipoProyectosAsync()
        {
            return await _context.tipoProyecto.ToListAsync(); // Obtiene todos los tipos de proyectos.
        }
        public async Task<Tipoproyecto?> GetTipoProyectoIdAsync(int id)
        {
            return await _context.tipoProyecto.FindAsync(id); // Busca un tipo de proyecto por su id.
        }
        public async Task AddTipoProyectoAsync(Tipoproyecto tipoproyecto)
        {
            _context.tipoProyecto.Add(tipoproyecto); // Agrega el nuevo tipo de proyecto.
            await _context.SaveChangesAsync(); // Guarda los cambios.
        }
    }
}
