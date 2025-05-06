using IMCAPI.Core.Interfaces.Services;
using IMCAPI.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMCAPI.Core.DTO;
using IMCAPI.Core.Entities;

namespace IMCAPI.Application.Services
{
    public class BeneficiarioService : IBeneficiarioService
    {
        private readonly IBeneficiarioRepository _beneficiarioRepository;
        public BeneficiarioService(IBeneficiarioRepository beneficiarioRepository)
        {
            _beneficiarioRepository = beneficiarioRepository;
        }
        public async Task<IEnumerable<BeneficiarioDto>> GetBeneficiariosAsync()
        {
            var beneficiarios = await _beneficiarioRepository.GetBeneficiariosAsync();
            return beneficiarios.Select(b => new BeneficiarioDto(
                b.Id,
                b.Identificacion,
                b.Nombre1,
                b.Nombre2,
                b.Apellido1,
                b.Apellido2,
                b.Celular,
                b.Firma,
                b.tipoiden,
                b.genero,
                b.Rangoedad,
                b.grupoetnico,
                b.tipobene,
                b.municipio,
                b.sector,
                b.beneficiarioactividad,
                b.Organizaciones
            ));
        }

        public async Task<BeneficiarioDto?> GetBeneficiarioIdAsync(int id)
        {
            var beneficiario = await _beneficiarioRepository.GetBeneficiarioIdAsync(id);
            if (beneficiario != null)
            {
                return new BeneficiarioDto(
                    beneficiario.Id,
                    beneficiario.Identificacion,
                    beneficiario.Nombre1,
                    beneficiario.Nombre2,
                    beneficiario.Apellido1,
                    beneficiario.Apellido2,
                    beneficiario.Celular,
                    beneficiario.Firma,
                    beneficiario.tipoiden,
                    beneficiario.genero,
                    beneficiario.Rangoedad,
                    beneficiario.grupoetnico,
                    beneficiario.tipobene,
                    beneficiario.municipio,
                    beneficiario.sector,
                    beneficiario.beneficiarioactividad,
                    beneficiario.Organizaciones);
            } else
            {
                return null;
            }
        }

        public async Task AddBeneficiarioAsync(BeneficiarioDto beneficiariodto)
        {
            var beneficiario = new Beneficiario
            {
                Id = beneficiariodto.Id,
                Identificacion = beneficiariodto.Identificacion,
                Nombre1 = beneficiariodto.Nombre1,
                Nombre2 = beneficiariodto.Nombre2,
                Apellido1 = beneficiariodto.Apellido1,
                Apellido2 = beneficiariodto.Apellido2,
                Celular = beneficiariodto.Celular,
                Tipoiden_id = beneficiariodto.tipoiden.Id,
                Generos_id = beneficiariodto.genero.Id,
                Edades_id = beneficiariodto.Rangoedad.Id,
                Firma = beneficiariodto.Firma,
                Grupoetnico_id = beneficiariodto.grupoetnico.Id,
                Tipobene_id = beneficiariodto.tipobene.Id,
                Municipios_id = beneficiariodto.municipio.Id,
                Sectores_id = beneficiariodto.sector.Id,
                tipoiden = beneficiariodto.tipoiden,
                genero = beneficiariodto.genero,
                Rangoedad = beneficiariodto.Rangoedad,
                grupoetnico = beneficiariodto.grupoetnico,
                tipobene = beneficiariodto.tipobene,
                municipio = beneficiariodto.municipio,
                sector = beneficiariodto.sector,
                beneficiarioactividad = beneficiariodto.beneficiarioactividad,
                Organizaciones = beneficiariodto.Organizaciones
            };

            await _beneficiarioRepository.AddBeneficiarioAsync(beneficiario);
        }

        public async Task UpdateBeneficiarioAsync(BeneficiarioDto beneficiariodto)
        {
            var beneficiario = new Beneficiario
            {
                Id = beneficiariodto.Id,
                Identificacion = beneficiariodto.Identificacion,
                Nombre1 = beneficiariodto.Nombre1,
                Nombre2 = beneficiariodto.Nombre2,
                Apellido1 = beneficiariodto.Apellido1,
                Apellido2 = beneficiariodto.Apellido2,
                Celular = beneficiariodto.Celular,
                Tipoiden_id = beneficiariodto.tipoiden.Id,
                Generos_id = beneficiariodto.genero.Id,
                Edades_id = beneficiariodto.Rangoedad.Id,
                Firma = beneficiariodto.Firma,
                Grupoetnico_id = beneficiariodto.grupoetnico.Id,
                Tipobene_id = beneficiariodto.tipobene.Id,
                Municipios_id = beneficiariodto.municipio.Id,
                Sectores_id = beneficiariodto.sector.Id,
                tipoiden = beneficiariodto.tipoiden,
                genero = beneficiariodto.genero,
                Rangoedad = beneficiariodto.Rangoedad,
                grupoetnico = beneficiariodto.grupoetnico,
                tipobene = beneficiariodto.tipobene,
                municipio = beneficiariodto.municipio,
                sector = beneficiariodto.sector,
                beneficiarioactividad = beneficiariodto.beneficiarioactividad,
                Organizaciones = beneficiariodto.Organizaciones
            };

            await _beneficiarioRepository.UpdateBeneficiarioAsync(beneficiario);
        }

        public async Task DeleteBeneficiarioAsync(int id)
        {
            await _beneficiarioRepository.DeleteBeneficiarioAsync(id);
        }
    }
}
