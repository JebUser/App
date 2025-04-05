﻿using IMCAPI.Core.Entities;
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

        public async Task<Proyecto?> GetBeneficiariosInProyectoAsync(int proyectoid)
        {
            return await _context.Proyectos
                .Include(p => p.beneficiarioProyectos) // Obtiene las relaciones con los beneficiarios.
                .Where(p => p.Id == proyectoid)
                .FirstOrDefaultAsync();
        }
    }
}
