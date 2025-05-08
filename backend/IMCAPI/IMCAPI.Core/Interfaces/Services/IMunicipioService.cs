using IMCAPI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Services
{
    public interface IMunicipioService
    {
        Task<IEnumerable<MunicipioDto>> GetMunicipiosAsync();
        Task<MunicipioDto?> GetMunicipioIdAsync(int id);
    }
}
