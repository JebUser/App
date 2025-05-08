using IMCAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.DTO
{
    public class BeneficiarioDto
    {
        public int Id { get; set; }
        public string? Identificacion { get; set; }
        public string Nombre1 { get; set; }
        public string? Nombre2 { get; set; }
        public string Apellido1 { get; set; }
        public string? Apellido2 { get; set; }
        public string? Celular { get; set; }
        public byte[]? Firma { get; set; }
        public TipoidenDto? tipoiden { get; set; }
        public GeneroDto genero { get; set; }
        public EdadDto? Rangoedad { get; set; }
        public GrupoetnicoDto? grupoetnico { get; set; }
        public TipobeneDto? tipobene { get; set; }
        public MunicipioDto? municipio { get; set; }
        public SectorDto? sector { get; set; }
        public List<OrganizacionDto> Organizaciones { get; set; }

        public BeneficiarioDto(int id, string identificacion, string nombre1, string nombre2, string apellido1, string apellido2, string celular, byte[]? firma, TipoidenDto? tipoiden, GeneroDto genero, EdadDto? rangoedad, GrupoetnicoDto? grupoetnico, TipobeneDto? tipobene, MunicipioDto? municipio, SectorDto? sector,List<OrganizacionDto> organizaciones)
        {
            Id = id;
            Identificacion = identificacion;
            Nombre1 = nombre1;
            Nombre2 = nombre2;
            Apellido1 = apellido1;
            Apellido2 = apellido2;
            Celular = celular;
            Firma = firma;
            this.tipoiden = tipoiden;
            this.genero = genero;
            Rangoedad = rangoedad;
            this.grupoetnico = grupoetnico;
            this.tipobene = tipobene;
            this.municipio = municipio;
            this.sector = sector;
            Organizaciones = organizaciones;
        }
    }
}
