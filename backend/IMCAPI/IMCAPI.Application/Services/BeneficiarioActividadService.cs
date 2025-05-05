using IMCAPI.Core.DTO;
using IMCAPI.Core.Interfaces.Repositories;
using IMCAPI.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMCAPI.Core.Entities;

namespace IMCAPI.Application.Services
{
    public class BeneficiarioActividadService : IBeneficiarioActividadService
    {
        private readonly IBeneficiarioActividadRepository _beneficiarioActividadRepository;
        private readonly IActividadRepository _actividadRepository;
        private readonly IBeneficiarioRepository _beneficiarioRepository;

        public BeneficiarioActividadService(IBeneficiarioActividadRepository beneficiarioActividadRepository)
        {
            _beneficiarioActividadRepository = beneficiarioActividadRepository;
        }

        public async Task AddBeneficiarioActividadAsync(BeneficiarioActividadDto beneficiarioActividadDto)
        {
            var actividad = await _actividadRepository.GetActividadByIdAsync(beneficiarioActividadDto.Actividades_id);
            var beneficiario = await _beneficiarioRepository.GetBeneficiarioIdAsync(beneficiarioActividadDto.Beneficiarios_id);
            if (actividad != null && beneficiario != null)
            {
                var beneficiarioactividad = new BeneficiarioActividad
                {
                    Actividades_id = beneficiarioActividadDto.Actividades_id,
                    Beneficiarios_id = beneficiarioActividadDto.Beneficiarios_id,
                    actividad = actividad,
                    beneficiario = beneficiario
                };
            }
        }

        public async Task DeleteBeneficiarioActividadAsync(int beneficiarioid, int actividadid)
        {
            await _beneficiarioActividadRepository.DeleteBeneficiarioActividadAsync(beneficiarioid, actividadid);
        }
    }
}
