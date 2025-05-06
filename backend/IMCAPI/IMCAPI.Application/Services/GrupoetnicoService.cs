using IMCAPI.Core.Entities;
using IMCAPI.Core.Interfaces.Repositories;
using IMCAPI.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Application.Services
{
    public class GrupoetnicoService : IGrupoetnicoService
    {
        private readonly IGrupoetnicoRepository _grupoetnicoRepository;

        public GrupoetnicoService(IGrupoetnicoRepository grupoetnicoRepository)
        {
            _grupoetnicoRepository = grupoetnicoRepository;
        }

        public async Task<IEnumerable<Grupoetnico>> GetGrupoetnicosAsync()
        {
            return await _grupoetnicoRepository.GetGrupoetnicosAsync();
        }

        public async Task<Grupoetnico?> GetGrupoetnicoByIdAsync(int id)
        {
            return await _grupoetnicoRepository.GetGrupoetnicoByIdAsync(id);
        }

        public async Task AddGrupoetnicoAsync(Grupoetnico grupoetnico)
        {
            await _grupoetnicoRepository.AddGrupoetnicoAsync(grupoetnico);
        }

        public async Task UpdateGrupoetnicoAsync(Grupoetnico grupoetnico)
        {
            await _grupoetnicoRepository.UpdateGrupoetnicoAsync(grupoetnico);
        }

        public async Task DeleteGrupoetnicoAsync(int id)
        {
            await _grupoetnicoRepository.DeleteGrupoetnicoAsync(id);
        }
    }
}
