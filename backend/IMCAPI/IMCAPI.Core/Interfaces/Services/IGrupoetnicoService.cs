using IMCAPI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Interfaces.Services
{
    public interface IGrupoetnicoService
    {
        Task<IEnumerable<GrupoetnicoDto>> GetGrupoetnicosAsync();
        Task<GrupoetnicoDto?> GetGrupoetnicoByIdAsync(int id);
        Task AddGrupoetnicoAsync(GrupoetnicoDto grupoetnicodto);
        Task UpdateGrupoetnicoAsync(GrupoetnicoDto grupoetnicodto);
        Task DeleteGrupoetnicoAsync(int id);
    }
}
