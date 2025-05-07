using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.DTO
{
    public class MunicipioDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DepartamentoDto departamento { get; set; }

        public MunicipioDto(int id, string nombre, DepartamentoDto departamento)
        {
            Id = id;
            Nombre = nombre;
            this.departamento = departamento;
        }
    }
}
