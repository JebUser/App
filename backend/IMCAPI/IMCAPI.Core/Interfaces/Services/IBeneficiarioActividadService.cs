using IMCAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMCAPI.Core.DTO;

namespace IMCAPI.Core.Interfaces.Services
{
    public interface IBeneficiarioActividadService
    {
        Task AddBeneficiarioActividadAsync(BeneficiarioActividadDto beneficiarioActividadDto);
        Task DeleteBeneficiarioActividadAsync(int beneficiarioid, int actividadid);
    }
}
