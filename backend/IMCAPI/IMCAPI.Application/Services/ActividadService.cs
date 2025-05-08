using IMCAPI.Core.DTO;
using IMCAPI.Core.Entities;
using IMCAPI.Core.Interfaces.Repositories;
using IMCAPI.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Application.Services
{
    public class ActividadService : IActividadService
    {
        private readonly IActividadRepository _actividadRepository;
        private readonly ILugarRepository _lugarRepository;

        public ActividadService(IActividadRepository actividadRepository, ILugarRepository lugarRepository)
        {
            _actividadRepository = actividadRepository;
            _lugarRepository = lugarRepository;
        }

        public async Task<IEnumerable<ActividadDto>> GetActividadesAsync()
        {
            var actividades = await _actividadRepository.GetActividadesAsync();
            return actividades.Select(a =>
            new ActividadDto(a.Id, a.Nombre, a.FechaInicio, a.FechaFinal, new LugarDto(a.lugar.Id, a.lugar.Nombre),a.beneficiarios.Select(b => new BeneficiarioDto(
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
            )).ToList()
            ));
        }

        public async Task<ActividadDto?> GetActividadByIdAsync(int id)
        {
            var actividad = await _actividadRepository.GetActividadByIdAsync(id);
            return new ActividadDto(actividad.Id, actividad.Nombre, actividad.FechaInicio, actividad.FechaFinal, new LugarDto(actividad.lugar.Id, actividad.lugar.Nombre), actividad.beneficiarios.Select(b => new BeneficiarioDto(
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
            )).ToList()
            );
        }

        public async Task AddActividadAsync(ActividadDto actividaddto)
        {
            var lugar = await _lugarRepository.GetLugarNombreAsync(actividaddto.lugar.Nombre);
            if (lugar != null)
            {
                var actividad = new Actividad
                {
                    Id = actividaddto.Id,
                    Nombre = actividaddto.Nombre,
                    FechaInicio = actividaddto.FechaInicio,
                    FechaFinal = actividaddto.FechaFinal,
                    Lugares_id = lugar.Id,
                    beneficiarios = actividaddto.beneficiarios.Select(a => new Beneficiario
                    {
                        Id = a.Id,
                        Identificacion = a.Identificacion,
                        Nombre1 = a.Nombre1,
                        Nombre2 = a.Nombre2,
                        Apellido1 = a.Apellido1,
                        Apellido2 = a.Apellido2,
                        Celular = a.Celular,
                        Tipoiden_id = a.tipoiden != null ? a.tipoiden.Id : null,
                        Generos_id = a.genero.Id,
                        Edades_id = a.Rangoedad != null ? a.Rangoedad.Id : null,
                        Firma = a.Firma,
                        Grupoetnico_id = a.grupoetnico != null ? a.grupoetnico.Id : null,
                        Tipobene_id = a.tipobene != null ? a.tipobene.Id : null,
                        Municipios_id = a.municipio != null ? a.municipio.Id : null,
                        Sectores_id = a.sector != null ? a.sector.Id : null,
                        Organizaciones = a.Organizaciones.Select(o => new Organizacion
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
                    }).ToList()
                };

                await _actividadRepository.AddActividadAsync(actividad);
            }
        }
        public async Task UpdateActividadAsync(ActividadDto actividaddto)
        {
            var lugar = await _lugarRepository.GetLugarNombreAsync(actividaddto.lugar.Nombre);
            var actividad = await _actividadRepository.GetActividadByIdAsync(actividaddto.Id);
            if (lugar != null && actividad != null)
            {
                actividad.Id = actividaddto.Id;
                actividad.Nombre = actividaddto.Nombre;
                actividad.FechaInicio = actividaddto.FechaInicio;
                actividad.FechaFinal = actividaddto.FechaFinal;
                actividad.Lugares_id = lugar.Id;
                actividad.beneficiarios.Clear();
                actividad.beneficiarios = actividaddto.beneficiarios.Select(a => new Beneficiario
                {
                    Id = a.Id,
                    Identificacion = a.Identificacion,
                    Nombre1 = a.Nombre1,
                    Nombre2 = a.Nombre2,
                    Apellido1 = a.Apellido1,
                    Apellido2 = a.Apellido2,
                    Celular = a.Celular,
                    Tipoiden_id = a.tipoiden != null ? a.tipoiden.Id : null,
                    Generos_id = a.genero.Id,
                    Edades_id = a.Rangoedad != null ? a.Rangoedad.Id : null,
                    Firma = a.Firma,
                    Grupoetnico_id = a.grupoetnico != null ? a.grupoetnico.Id : null,
                    Tipobene_id = a.tipobene != null ? a.tipobene.Id : null,
                    Municipios_id = a.municipio != null ? a.municipio.Id : null,
                    Sectores_id = a.sector != null ? a.sector.Id : null,
                    Organizaciones = a.Organizaciones.Select(o => new Organizacion
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
                }).ToList();

                await _actividadRepository.UpdateActividadAsync(actividad);
            }
        }

        public async Task DeleteActividadAsync(int id)
        {
            await _actividadRepository.DeleteActividadAsync(id);
        }

    }
}
