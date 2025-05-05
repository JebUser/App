using IMCAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Services
{
    public interface IMunicipioService
    {
        Task<IEnumerable<Municipio>> GetMunicipiosAsync();
        Task<Municipio?> GetMunicipioIdAsync(int id);
    }
}
