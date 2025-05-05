using IMCAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Repositories
{
    public interface IMunicipioRepository
    {
        Task<IEnumerable<Municipio>> GetMunicipiosAsync();
        Task<Municipio?> GetMunicipioIdAsync(int id);
    }
}
