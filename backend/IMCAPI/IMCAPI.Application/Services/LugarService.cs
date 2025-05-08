using IMCAPI.Core.Entities;
using IMCAPI.Core.DTO;
using IMCAPI.Core.Interfaces.Repositories;
using IMCAPI.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMCAPI.Application.Services
{
    public class LugarService : ILugarService
    {
        private readonly ILugarRepository _lugarRepository;

        public LugarService(ILugarRepository lugarRepository)
        {
            _lugarRepository = lugarRepository;
        }

        public async Task<IEnumerable<LugarDto>> GetLugaresAsync()
        {
            var lugars = await _lugarRepository.GetLugaresAsync();
            return lugars.Select(lp => new LugarDto(lp.Id, lp.Nombre));
        }

        public async Task<LugarDto?> GetLugarNombreAsync(string nombre)
        {
            var lugar = await _lugarRepository.GetLugarNombreAsync(nombre);
            return new LugarDto(lugar.Id, lugar.Nombre);
        }

        public async Task AddLugarAsync(LugarDto lugardto)
        {
            var lugar = new Lugar
            {
                Id = lugardto.Id,
                Nombre = lugardto.Nombre
            };
            await _lugarRepository.AddLugarAsync(lugar);
        }

        public async Task UpdateLugarAsync(LugarDto lugardto)
        {
            var lugar = await _lugarRepository.GetLugarNombreAsync(lugardto.Nombre);
            if (lugar != null)
            {
                lugar.Nombre = lugardto.Nombre;
                await _lugarRepository.UpdateLugarAsync(lugar);
            }
        }

        public async Task DeleteLugarAsync(int id)
        {
            await _lugarRepository.DeleteLugarAsync(id);
        }
    }
}
