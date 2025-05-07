using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.DTO
{
    public class DepartamentoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public DepartamentoDto(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
    }
}
