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
    public class MunicipioService : IMunicipioService
    {
        private readonly IMunicipioRepository _municipioRepository;

        public MunicipioService(IMunicipioRepository municipioRepository)
        {
            _municipioRepository = municipioRepository;
        }

        public async Task<IEnumerable<MunicipioDto>> GetMunicipiosAsync()
        {
            var municipios = await _municipioRepository.GetMunicipiosAsync();

            return municipios.Select(m => new MunicipioDto(
                m.Id,
                m.Nombre,
                new DepartamentoDto(m.departamento.Id, m.departamento.Nombre)
            ));
        }

        public async Task<MunicipioDto?> GetMunicipioIdAsync(int id)
        {
            var municipio = await _municipioRepository.GetMunicipioIdAsync(id);
            if (municipio == null) return null;
            return new MunicipioDto(
                municipio.Id,
                municipio.Nombre,
                new DepartamentoDto(municipio.departamento.Id, municipio.departamento.Nombre)
            );
        }
    }
}