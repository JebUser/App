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
                .ToListAsync();
        }

        public async Task<Beneficiario?> GetBeneficiarioIdAsync(int id)
        {
            return await _context.Beneficiarios
                .Include(b => b.tipoiden)
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
