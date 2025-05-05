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
    public class MunicipioService : IMunicipioService
    {
        private readonly IMunicipioRepository _municipioRepository;

        public MunicipioService(IMunicipioRepository municipioRepository)
        {
            _municipioRepository = municipioRepository;
        }

        public async Task<IEnumerable<Municipio>> GetMunicipiosAsync()
        {
            var municipios = await _municipioRepository.GetMunicipiosAsync();
            return municipios;
        }

        public async Task<Municipio?> GetMunicipioIdAsync(int id)
        {
            var municipio = await _municipioRepository.GetMunicipioIdAsync(id);
            if (municipio == null) return null;
            return municipio;
        }
    }
}