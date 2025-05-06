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
        public string Identificacion { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Celular { get; set; }
        public byte[] Firma { get; set; }
        public Tipoiden tipoiden { get; set; }
        public Genero genero { get; set; }
        public Edad Rangoedad { get; set; }
        public Grupoetnico grupoetnico { get; set; }
        public Tipobene tipobene { get; set; }
        public Municipio municipio { get; set; }
        public Sector sector { get; set; }
        public List<BeneficiarioActividad> beneficiarioactividad { get; set; }
        public List<Organizacion> Organizaciones { get; set; }

        public BeneficiarioDto(int id, string identificacion, string nombre1, string nombre2, string apellido1, string apellido2, string celular, byte[] firma, Tipoiden tipoiden, Genero genero, Edad rangoedad, Grupoetnico grupoetnico, Tipobene tipobene, Municipio municipio, Sector sector, List<BeneficiarioActividad> beneficiarioactividad, List<Organizacion> organizaciones)
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
            this.beneficiarioactividad = beneficiarioactividad;
            Organizaciones = organizaciones;
        }
    }
}
