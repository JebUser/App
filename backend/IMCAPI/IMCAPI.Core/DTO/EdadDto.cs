using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.DTO
{
    public class EdadDto
    {
        public int Id { get; set; }
        public string Rango { get; set; }

        public EdadDto(int id, string rango)
        {
            Id = id;
            Rango = rango;
        }
    }
}
