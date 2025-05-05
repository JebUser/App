using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.Entities
{
    public class Lugar
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }

        // Datos a obtener de las claves foráneas.
        public List<Actividad> actividades { get; set; } = new();
    }
}
