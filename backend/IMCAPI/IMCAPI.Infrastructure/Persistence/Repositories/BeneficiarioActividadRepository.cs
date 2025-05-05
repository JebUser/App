using IMCAPI.Core.Entities;
using IMCAPI.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Infrastructure.Persistence.Repositories
{
    public class BeneficiarioActividadRepository : IBeneficiarioActividadRepository
    {
        private readonly DbContextIMC _context;

        public BeneficiarioActividadRepository(DbContextIMC context)
        {
            _context = context;
        }

        public async Task AddBeneficiarioActividadAsync(BeneficiarioActividad beneficiarioActividad)
        {
            _context.beneficiarioActividad.Add(beneficiarioActividad);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBeneficiarioActividadAsync(int beneficiarioid, int actividadid)
        {
            var beneficiarioactividad = await _context.beneficiarioActividad.FindAsync(actividadid, beneficiarioid);
            if (beneficiarioactividad != null)
            {
                _context.beneficiarioActividad.Remove(beneficiarioactividad);
                await _context.SaveChangesAsync();
            }
        }
    }
}
