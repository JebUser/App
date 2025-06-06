﻿using IMCAPI.Core.Entities;
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
                .Include(a => a.beneficiarios)
                    .ThenInclude(b => b.tipoiden)
                .Include(a => a.beneficiarios)
                    .ThenInclude(b => b.genero)
                .Include(a => a.beneficiarios)
                    .ThenInclude(b => b.Rangoedad)
                .Include(a => a.beneficiarios)
                    .ThenInclude(b => b.grupoetnico)
                .Include(a => a.beneficiarios)
                    .ThenInclude(b => b.tipobene)
                .Include(a => a.beneficiarios)
                    .ThenInclude(b => b.municipio)
                        .ThenInclude(m => m.departamento)
                .Include(a => a.beneficiarios)
                    .ThenInclude(b => b.sector)
                .Include(a => a.beneficiarios)
                    .ThenInclude(b => b.Organizaciones)
                        .ThenInclude(o => o.municipio)
                            .ThenInclude(m => m.departamento)
                .Include(a => a.beneficiarios)
                    .ThenInclude(b => b.Organizaciones)
                        .ThenInclude(o => o.tipoorg)
                .Include(a => a.beneficiarios)
                    .ThenInclude(b => b.Organizaciones)
                        .ThenInclude(o => o.tipoactividad)
                .Include(a => a.beneficiarios)
                    .ThenInclude(b => b.Organizaciones)
                        .ThenInclude(o => o.lineaprod)
                .Include(a => a.beneficiarios)
                    .ThenInclude(b => b.Organizaciones)
                        .ThenInclude(o => o.tipoapoyo)
                .ToListAsync();
        }

        public async Task<Actividad?> GetActividadByIdAsync(int id)
        {
            return await _context.Actividades
                .Include(a => a.lugar)
                .Include(a => a.beneficiarios)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task AddActividadAsync(Actividad actividad)
        {
            foreach (var beneficiario in actividad.beneficiarios)
            {
                // Solo registrarlo como ya existente.
                if (_context.Beneficiarios.Any(b => b.Id == beneficiario.Id))
                {
                    _context.Attach(beneficiario);
                }

                foreach(var organizacion in beneficiario.Organizaciones)
                {
                    // Solo registrarlo como ya existente.
                    if (_context.Organizaciones.Any(o => o.Id == organizacion.Id))
                    {
                        _context.Attach(organizacion);
                    }
                }
            }
            _context.Actividades.Add(actividad);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateActividadAsync(Actividad actividad, List<int> beneficiariosIds)
        {
            var beneficiarios = await _context.Beneficiarios
                .Where(b => beneficiariosIds.Contains(b.Id))
                .ToListAsync();
            actividad.beneficiarios.Clear();
            foreach (var beneficiario in beneficiarios)
            {
                actividad.beneficiarios.Add(beneficiario);
            }
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
