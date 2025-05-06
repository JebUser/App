using IMCAPI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Services
{
    public interface IBeneficiarioService
    {
        Task<IEnumerable<BeneficiarioDto>> GetBeneficiariosAsync();
        Task<BeneficiarioDto?> GetBeneficiarioIdAsync(int id);
        Task AddBeneficiarioAsync(BeneficiarioDto beneficiariodto);
        Task UpdateBeneficiarioAsync(BeneficiarioDto beneficiariodto);
        Task DeleteBeneficiarioAsync(int id);
    }
}
