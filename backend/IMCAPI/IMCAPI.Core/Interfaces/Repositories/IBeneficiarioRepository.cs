﻿using IMCAPI.Core.Entities;

namespace IMCAPI.Core.Interfaces.Repositories
{
    public interface IBeneficiarioRepository
    {
        Task<IEnumerable<Beneficiario>> GetBeneficiariosAsync();
        Task<Beneficiario?> GetBeneficiarioIdAsync(int id);
        Task AddBeneficiarioAsync(Beneficiario beneficiario);
        Task UpdateBeneficiarioAsync(Beneficiario beneficiario, List<int> organizacionesIds);
        Task DeleteBeneficiarioAsync(int id);
    }
}
