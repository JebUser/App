using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.DTO
{
    public class CreateProyectoDto
    {
        public required string Nombre { get; set; }
        public required DateTime Fechainicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public int Municipios_id { get; set; }
        public required int Tipoid { get; set; }
    }
}
