using IMCAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Services
{
    public interface IGrupoetnicoService
    {
        Task<IEnumerable<Grupoetnico>> GetGrupoetnicosAsync();
        Task<Grupoetnico?> GetGrupoetnicoByIdAsync(int id);
        Task AddGrupoetnicoAsync(Grupoetnico grupoetnico);
        Task UpdateGrupoetnicoAsync(Grupoetnico grupoetnico);
        Task DeleteGrupoetnicoAsync(int id);
    }
}
