using IMCAPI.Core.Entities;
using IMCAPI.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IMCAPI.Infrastructure.Persistence.Repositories
{
    public class BeneficiarioRepository : IBeneficiarioRepository
    {
        private readonly DbContextIMC _context;

        public BeneficiarioRepository(DbContextIMC context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Beneficiario>> GetBeneficiariosAsync()
        {
            return await _context.Beneficiarios
                .Include(b => b.tipoiden)
                .Include(b => b.genero)
                .Include(b => b.Rangoedad)
                .Include(b => b.grupoetnico)
                .Include(b => b.tipobene)
                .Include(b => b.municipio)
                .Include(b => b.sector)
                .Include(b => b.Organizaciones)
                .ToListAsync();
        }

        public async Task<Beneficiario?> GetBeneficiarioIdAsync(int id)
        {
            return await _context.Beneficiarios
                .Include(b => b.tipoiden)
                .Include(b => b.genero)
                .Include(b => b.Rangoedad)
                .Include(b => b.grupoetnico)
                .Include(b => b.tipobene)
                .Include(b => b.municipio)
                .Include(b => b.sector)
                .Include(b => b.Organizaciones)
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task AddBeneficiarioAsync(Beneficiario beneficiario)
        {
            _context.AttachRange(beneficiario.Organizaciones);
            _context.Beneficiarios.Add(beneficiario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBeneficiarioAsync(Beneficiario beneficiario)
        {
            _context.Beneficiarios.Update(beneficiario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBeneficiarioAsync(int id)
        {
            var beneficiario = await _context.Beneficiarios.FindAsync(id);

            if (beneficiario != null) {
                _context.Beneficiarios.Remove(beneficiario);
                await _context.SaveChangesAsync();
            }
        }
    }
}
