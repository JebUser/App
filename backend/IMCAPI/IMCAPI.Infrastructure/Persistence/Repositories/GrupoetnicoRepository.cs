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
    public class GrupoetnicoRepository : IGrupoetnicoRepository
    {
        private readonly DbContextIMC _context;

        public GrupoetnicoRepository(DbContextIMC context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Grupoetnico>> GetGrupoetnicosAsync()
        {
            return await _context.grupoEtnico.ToListAsync();
        }

        public async Task<Grupoetnico?> GetGrupoetnicoByIdAsync(int id)
        {
            return await _context.grupoEtnico.FindAsync(id);
        }

        public async Task AddGrupoetnicoAsync(Grupoetnico grupoetnico)
        {
            _context.grupoEtnico.Add(grupoetnico);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGrupoetnicoAsync(Grupoetnico grupoetnico)
        {
            _context.grupoEtnico.Update(grupoetnico);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteGrupoetnicoAsync(int id)
        {
            var grupoetnico = await _context.grupoEtnico.FindAsync(id);
            if (grupoetnico != null)
            {
                _context.grupoEtnico.Remove(grupoetnico);
                await _context.SaveChangesAsync();
            }
        }
    }
}
