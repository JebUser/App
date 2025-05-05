using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Entities
{
    public class Actividad
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required DateTime FechaInicio { get; set; }
        public DateTime? FechaFinal { get; set; } = null;
        public required int Lugares_id { get; set; }

        // Datos a obtener de las claves foráneas.
        public required Lugar lugar { get; set; }
        public List<BeneficiarioActividad> beneficiarioactividad { get; set; } = new();
        public List<Proyecto> proyectos { get; set; } = new();
    }
}
