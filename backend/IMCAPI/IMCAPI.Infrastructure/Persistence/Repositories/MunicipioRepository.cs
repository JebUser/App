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
    public class MunicipioRepository : IMunicipioRepository
    {
        private readonly DbContextIMC _context;

        public MunicipioRepository(DbContextIMC context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Municipio>> GetMunicipiosAsync()
        {
            return await _context.Municipios
                .Include(m => m.departamento)
                .ToListAsync(); // Obtiene todos los tipos de proyectos.
        }
        public async Task<Municipio?> GetMunicipioIdAsync(int id)
        {
            return await _context.Municipios
                .Include(m => m.departamento)
                .Where(m => m.Id == id) // Busca un tipo de proyecto por su id.
                .FirstOrDefaultAsync();
        }
    }
}
