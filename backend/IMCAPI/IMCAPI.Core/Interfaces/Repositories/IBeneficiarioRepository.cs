using IMCAPI.Core.Entities;

namespace IMCAPI.Core.Interfaces.Repositories
{
    public interface IBeneficiarioRepository
    {
        Task<IEnumerable<Beneficiario>> GetBeneficiariosAsync();
        Task<Beneficiario?> GetBeneficiarioIdAsync(int id);
        
    }
}
