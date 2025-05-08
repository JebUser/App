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
    public class OrganizacionService : IOrganizacionService
    {
        private readonly IOrganizacionRepository _organizacionRepository;
        public OrganizacionService(IOrganizacionRepository organizacionRepository)
        {
            _organizacionRepository = organizacionRepository;
        }
        public async Task<IEnumerable<OrganizacionDto>> GetOrganizacionesAsync()
        {
            var organizacions = await _organizacionRepository.GetOrganizacionesAsync();
            return organizacions.Select(o => new OrganizacionDto(
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
            ));
        }

        public async Task<OrganizacionDto?> GetOrganizacionIdAsync(int id)
        {
            var organizacion = await _organizacionRepository.GetOrganizacionIdAsync(id);
            if (organizacion != null)
            {
                return new OrganizacionDto(
                organizacion.Id,
                organizacion.Nombre,
                new MunicipioDto(organizacion.municipio.Id, organizacion.municipio.Nombre, new DepartamentoDto(organizacion.municipio.departamento.Id, organizacion.municipio.departamento.Nombre)),
                organizacion.Nit,
                organizacion.Integrantes,
                organizacion.Nummujeres,
                organizacion.Orgmujeres,
                organizacion.tipoorg != null ? new TipoorgDto(organizacion.tipoorg.Id, organizacion.tipoorg.Nombre) : null,
                organizacion.tipoactividad != null ? new TipoactividadDto(organizacion.tipoactividad.Id, organizacion.tipoactividad.Nombre) : null,
                organizacion.lineaprod != null ? new LineaprodDto(organizacion.lineaprod.Id, organizacion.lineaprod.Nombre) : null,
                organizacion.tipoapoyo != null ? new TipoapoyoDto(organizacion.tipoapoyo.Id, organizacion.tipoapoyo.Nombre) : null
            );
            } else
            {
                return null;
            }
        }

        public async Task AddOrganizacionAsync(OrganizacionDto organizaciondto)
        {
            var organizacion = new Organizacion
            {
                Id = organizaciondto.Id,
                Nombre = organizaciondto.Nombre,
                Municipios_id = organizaciondto.municipio.Id,
                Nit = organizaciondto.Nit,
                Integrantes = organizaciondto.Integrantes,
                Nummujeres = organizaciondto.Nummujeres,
                Orgmujeres = organizaciondto.Orgmujeres,
                Tipoorg_id = organizaciondto.tipoorg != null ? organizaciondto.tipoorg.Id : null,
                Tipoactividad_id = organizaciondto.tipoactividad != null ? organizaciondto.tipoactividad.Id : null,
                Lineaprod_id = organizaciondto.lineaprod != null ? organizaciondto.lineaprod.Id : null,
                Tipoapoyo_id = organizaciondto.tipoapoyo != null ? organizaciondto.tipoapoyo.Id : null
            };

            await _organizacionRepository.AddOrganizacionAsync(organizacion);
        }

        public async Task UpdateOrganizacionAsync(OrganizacionDto organizaciondto)
        {
            var organizacion = await _organizacionRepository.GetOrganizacionIdAsync(organizaciondto.Id);
            if (organizacion != null)
            {
                organizacion.Id = organizaciondto.Id;
                organizacion.Nombre = organizaciondto.Nombre;
                organizacion.Municipios_id = organizaciondto.municipio.Id;
                organizacion.Nit = organizaciondto.Nit;
                organizacion.Integrantes = organizaciondto.Integrantes;
                organizacion.Nummujeres = organizaciondto.Nummujeres;
                organizacion.Orgmujeres = organizaciondto.Orgmujeres;
                organizacion.Tipoorg_id = organizaciondto.tipoorg != null ? organizaciondto.tipoorg.Id : null;
                organizacion.Tipoactividad_id = organizaciondto.tipoactividad != null ? organizaciondto.tipoactividad.Id : null;
                organizacion.Lineaprod_id = organizaciondto.lineaprod != null ? organizaciondto.lineaprod.Id : null;
                organizacion.Tipoapoyo_id = organizaciondto.tipoapoyo != null ? organizaciondto.tipoapoyo.Id : null;
                await _organizacionRepository.UpdateOrganizacionAsync(organizacion);
            }
        }

        public async Task DeleteOrganizacionAsync(int id)
        {
            await _organizacionRepository.DeleteOrganizacionAsync(id);
        }
    }
}
