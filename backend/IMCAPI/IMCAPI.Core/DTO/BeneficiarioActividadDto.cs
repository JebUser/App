using IMCAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Core.DTO
{
    public class BeneficiarioActividadDto
    {
        public int Actividades_id { get; set; }
        public int Beneficiarios_id { get; set; }

        public BeneficiarioActividadDto(BeneficiarioActividad beneficiarioActividad)
        {
            Actividades_id = beneficiarioActividad.Actividades_id;
            Beneficiarios_id = beneficiarioActividad.Beneficiarios_id;
        }
    }
}
