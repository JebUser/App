using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMCAPI.Core.Entities;

namespace IMCAPI.Core.DTO
{
    public class ActividadDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; } = null;
        public LugarDto lugar {  get; set; }
        
        public ActividadDto(int id, string nombre, DateTime fechainicio, DateTime? fechafinal, LugarDto lugar)
        {
            Id = id;
            Nombre = nombre;
            FechaInicio = fechainicio;
            FechaFinal = fechafinal;
            this.lugar = lugar;
        }
    }
}
