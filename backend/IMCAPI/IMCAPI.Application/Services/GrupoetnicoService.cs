using IMCAPI.Core.Entities;
using IMCAPI.Core.DTO;
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

        public async Task<IEnumerable<GrupoetnicoDto>> GetGrupoetnicosAsync()
        {
            var grupoetnicos = await _grupoetnicoRepository.GetGrupoetnicosAsync();
            return grupoetnicos.Select(ge => new GrupoetnicoDto(ge.Id, ge.Nombre));
        }

        public async Task<GrupoetnicoDto?> GetGrupoetnicoByIdAsync(int id)
        {
            var grupoetnico = await _grupoetnicoRepository.GetGrupoetnicoByIdAsync(id);
            return new GrupoetnicoDto(grupoetnico.Id, grupoetnico.Nombre);
        }

        public async Task AddGrupoetnicoAsync(GrupoetnicoDto grupoetnicodto)
        {
            var grupoetnico = new Grupoetnico
            {
                Id = grupoetnicodto.Id,
                Nombre = grupoetnicodto.Nombre
            };
            await _grupoetnicoRepository.AddGrupoetnicoAsync(grupoetnico);
        }

        public async Task UpdateGrupoetnicoAsync(GrupoetnicoDto grupoetnicodto)
        {
            var grupoetnico = await _grupoetnicoRepository.GetGrupoetnicoByIdAsync(grupoetnicodto.Id);
            if (grupoetnico != null)
            {
                grupoetnico.Nombre = grupoetnicodto.Nombre;
                await _grupoetnicoRepository.UpdateGrupoetnicoAsync(grupoetnico);
            }
        }

        public async Task DeleteGrupoetnicoAsync(int id)
        {
            await _grupoetnicoRepository.DeleteGrupoetnicoAsync(id);
        }
    }
}
