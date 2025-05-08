using IMCAPI.Core.Interfaces.Services;
using IMCAPI.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMCAPI.Core.DTO;
using IMCAPI.Core.Entities;
using System.Runtime.Intrinsics.X86;

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
                b.tipoiden != null ? new TipoidenDto(b.tipoiden.Id, b.tipoiden.Nombre) : null,
                new GeneroDto(b.genero.Id, b.genero.Nombre),
                b.Rangoedad != null ? new EdadDto(b.Rangoedad.Id, b.Rangoedad.Rango) : null,
                b.grupoetnico != null ? new GrupoetnicoDto(b.grupoetnico.Id, b.grupoetnico.Nombre) : null,
                b.tipobene != null ? new TipobeneDto(b.tipobene.Id, b.tipobene.Nombre) : null,
                b.municipio != null ? new MunicipioDto(b.municipio.Id, b.municipio.Nombre, new DepartamentoDto(b.municipio.departamento.Id, b.municipio.departamento.Nombre)) : null,
                b.sector != null ? new SectorDto(b.sector.Id, b.sector.Nombre) : null,
                b.Organizaciones.Select(o => new OrganizacionDto(
                    o.Id,
                    o.Nombre,
                    new MunicipioDto(o.municipio.Id, o.municipio.Nombre, new DepartamentoDto(o.municipio.departamento.Id, o.municipio.departamento.Nombre)),
                    o.Nit,
                    o.Integrantes,
                    o.Nummujeres,
                    o.Orgmujeres,
                    o.tipoorg != null ? new TipoorgDto(o.tipoorg.Id, o.tipoorg.Nombre) : null,
                    o.tipoactividad != null ? new TipoactividadDto(o.tipoactividad.Id, o.tipoactividad.Nombre) : null,
                    o.lineaprod != null ? new LineaprodDto(o.lineaprod.Id, o.lineaprod.Nombre) : null,
                    o.tipoapoyo != null ? new TipoapoyoDto(o.tipoapoyo.Id, o.tipoapoyo.Nombre) : null

                )).ToList()
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
                beneficiario.tipoiden != null ? new TipoidenDto(beneficiario.tipoiden.Id, beneficiario.tipoiden.Nombre) : null,
                new GeneroDto(beneficiario.genero.Id, beneficiario.genero.Nombre),
                beneficiario.Rangoedad != null ? new EdadDto(beneficiario.Rangoedad.Id, beneficiario.Rangoedad.Rango) : null,
                beneficiario.grupoetnico != null ? new GrupoetnicoDto(beneficiario.grupoetnico.Id, beneficiario.grupoetnico.Nombre) : null,
                beneficiario.tipobene != null ? new TipobeneDto(beneficiario.tipobene.Id, beneficiario.tipobene.Nombre) : null,
                beneficiario.municipio != null ? new MunicipioDto(beneficiario.municipio.Id, beneficiario.municipio.Nombre, new DepartamentoDto(beneficiario.municipio.departamento.Id, beneficiario.municipio.departamento.Nombre)) : null,
                beneficiario.sector != null ? new SectorDto(beneficiario.sector.Id, beneficiario.sector.Nombre) : null,
                beneficiario.Organizaciones.Select(o => new OrganizacionDto(
                    o.Id,
                    o.Nombre,
                    new MunicipioDto(o.municipio.Id, o.municipio.Nombre, new DepartamentoDto(o.municipio.departamento.Id, o.municipio.departamento.Nombre)),
                    o.Nit,
                    o.Integrantes,
                    o.Nummujeres,
                    o.Orgmujeres,
                    o.tipoorg != null ? new TipoorgDto(o.tipoorg.Id, o.tipoorg.Nombre) : null,
                    o.tipoactividad != null ? new TipoactividadDto(o.tipoactividad.Id, o.tipoactividad.Nombre) : null,
                    o.lineaprod != null ? new LineaprodDto(o.lineaprod.Id, o.lineaprod.Nombre) : null,
                    o.tipoapoyo != null ? new TipoapoyoDto(o.tipoapoyo.Id, o.tipoapoyo.Nombre) : null

                )).ToList()
            );
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
                Tipoiden_id = beneficiariodto.tipoiden != null ? beneficiariodto.tipoiden.Id : null,
                Generos_id = beneficiariodto.genero.Id,
                Edades_id = beneficiariodto.Rangoedad != null ? beneficiariodto.Rangoedad.Id : null,
                Firma = beneficiariodto.Firma,
                Grupoetnico_id = beneficiariodto.grupoetnico != null ? beneficiariodto.grupoetnico.Id : null,
                Tipobene_id = beneficiariodto.tipobene != null ? beneficiariodto.tipobene.Id : null,
                Municipios_id = beneficiariodto.municipio != null ? beneficiariodto.municipio.Id : null,
                Sectores_id = beneficiariodto.sector != null ? beneficiariodto.sector.Id : null,
                Organizaciones = beneficiariodto.Organizaciones.Select(o => new Organizacion 
                { 
                    Id = o.Id,
                    Nombre = o.Nombre,
                    Municipios_id = o.municipio.Id,
                    Nit = o.Nit,
                    Integrantes = o.Integrantes,
                    Nummujeres = o.Nummujeres,
                    Orgmujeres = o.Orgmujeres,
                    Tipoorg_id = o.tipoorg != null ? o.tipoorg.Id : null,
                    Tipoactividad_id = o.tipoactividad != null ? o.tipoactividad.Id : null,
                    Lineaprod_id = o.lineaprod != null ? o.lineaprod.Id : null,
                    Tipoapoyo_id = o.tipoapoyo != null ? o.tipoapoyo.Id : null
                }).ToList()
            };

            await _beneficiarioRepository.AddBeneficiarioAsync(beneficiario);
        }

        public async Task UpdateBeneficiarioAsync(BeneficiarioDto beneficiariodto)
        {
            var beneficiario = await _beneficiarioRepository.GetBeneficiarioIdAsync(beneficiariodto.Id);
            if (beneficiario != null)
            {
                beneficiario.Id = beneficiariodto.Id;
                beneficiario.Identificacion = beneficiariodto.Identificacion;
                beneficiario.Nombre1 = beneficiariodto.Nombre1;
                beneficiario.Nombre2 = beneficiariodto.Nombre2;
                beneficiario.Apellido1 = beneficiariodto.Apellido1;
                beneficiario.Apellido2 = beneficiariodto.Apellido2;
                beneficiario.Celular = beneficiariodto.Celular;
                beneficiario.Tipoiden_id = beneficiariodto.tipoiden != null ? beneficiariodto.tipoiden.Id : null;
                beneficiario.Generos_id = beneficiariodto.genero.Id;
                beneficiario.Edades_id = beneficiariodto.Rangoedad != null ? beneficiariodto.Rangoedad.Id : null;
                beneficiario.Firma = beneficiariodto.Firma;
                beneficiario.Grupoetnico_id = beneficiariodto.grupoetnico != null ? beneficiariodto.grupoetnico.Id : null;
                beneficiario.Tipobene_id = beneficiariodto.tipobene != null ? beneficiariodto.tipobene.Id : null;
                beneficiario.Municipios_id = beneficiariodto.municipio != null ? beneficiariodto.municipio.Id : null;
                beneficiario.Sectores_id = beneficiariodto.sector != null ? beneficiariodto.sector.Id : null;

                if (beneficiario.Organizaciones.Count() != beneficiariodto.Organizaciones.Count())
                {
                    beneficiario.Organizaciones.Clear();
                    beneficiario.Organizaciones = beneficiariodto.Organizaciones.Select(o => new Organizacion
                    {
                        Id = o.Id,
                        Nombre = o.Nombre,
                        Municipios_id = o.municipio.Id,
                        Nit = o.Nit,
                        Integrantes = o.Integrantes,
                        Nummujeres = o.Nummujeres,
                        Orgmujeres = o.Orgmujeres,
                        Tipoorg_id = o.tipoorg != null ? o.tipoorg.Id : null,
                        Tipoactividad_id = o.tipoactividad != null ? o.tipoactividad.Id : null,
                        Lineaprod_id = o.lineaprod != null ? o.lineaprod.Id : null,
                        Tipoapoyo_id = o.tipoapoyo != null ? o.tipoapoyo.Id : null
                    }).ToList();
                }
                await _beneficiarioRepository.UpdateBeneficiarioAsync(beneficiario);
            }
        }

        public async Task DeleteBeneficiarioAsync(int id)
        {
            await _beneficiarioRepository.DeleteBeneficiarioAsync(id);
        }
    }
}
