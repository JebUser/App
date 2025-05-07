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
                    Lugares_id = lugar.Id
                };

                await _actividadRepository.AddActividadAsync(actividad);
            }
        }
        public async Task UpdateActividadAsync(ActividadDto actividaddto)
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
                    Lugares_id = lugar.Id
                };

                await _actividadRepository.UpdateActividadAsync(actividad);
            }
        }

        public async Task DeleteActividadAsync(int id)
        {
            await _actividadRepository.DeleteActividadAsync(id);
        }

    }
}
