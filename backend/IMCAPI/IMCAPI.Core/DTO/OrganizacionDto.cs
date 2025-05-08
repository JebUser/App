using IMCAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.DTO
{
    public class OrganizacionDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public MunicipioDto municipio { get; set; }
        public string? Nit {  get; set; }
        public int? Integrantes { get; set; }
        public int? Nummujeres { get; set; }
        public float? Orgmujeres { get; set; }
        public TipoorgDto? tipoorg { get; set; }
        public TipoactividadDto? tipoactividad { get; set; }
        public LineaprodDto? lineaprod { get; set; }
        public TipoapoyoDto? tipoapoyo { get; set; }

        public OrganizacionDto(int id, string nombre, MunicipioDto municipio, string? nit, int? integrantes, int? nummujeres, float? orgmujeres, TipoorgDto? tipoorg, TipoactividadDto? tipoactividad, LineaprodDto? lineaprod, TipoapoyoDto? tipoapoyo)
        {
            Id = id;
            Nombre = nombre;
            this.municipio = municipio;
            Nit = nit;
            Integrantes = integrantes;
            Nummujeres = nummujeres;
            Orgmujeres = orgmujeres;
            this.tipoorg = tipoorg;
            this.tipoactividad = tipoactividad;
            this.lineaprod = lineaprod;
            this.tipoapoyo = tipoapoyo;
        }
    }
}
