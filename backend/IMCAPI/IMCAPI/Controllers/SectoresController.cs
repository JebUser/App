using Microsoft.AspNetCore.Mvc;
using IMCAPI.Core.DTO;
using IMCAPI.Core.Interfaces.Services;
using IMCAPI.Core.Entities;
[ApiController]
[Route("api/[controller]")]
public class SectoresController : ControllerBase
{
    private readonly ISectorService _sectorService;

    public SectoresController(ISectorService sectorService)
    {
        _sectorService = sectorService;
    }

    /// <summary>
    /// Obtiene la lista de sectores almacenados.
    /// </summary>
    /// <returns>Lista de sectores.</returns>
    [HttpGet]
    public async Task<IActionResult> GetSectores()
    {
        var sectores = await _sectorService.GetSectoresAsync(); // Obtiene la lista de sectores disponibles.
        if (sectores == null) return NotFound();
        return Ok(sectores);
    }
    /// <summary>
    /// Buscar un sector por su id único.
    /// </summary>
    /// <param name="id">El id del sector.</param>
    /// <returns>El sector deseado.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSectorId(int id)
    {
        var sector = await _sectorService.GetSectorByIdAsync(id); // Busca el sector.
        if (sector == null) return NotFound();
        return Ok(sector);
    }

    /// <summary>
    /// Agrega un nuevo sector al sistema.
    /// </summary>
    /// <param name="sectordto">El sector que se quiere agregar</param>
    [HttpPost]
    public async Task<IActionResult> CreateSector([FromBody] SectorDto sectordto)
    {
        await _sectorService.AddSectorAsync(sectordto); // Instrucción de agregar el nuevo sector.
        return Created($"/api/Sectores/{sectordto.Id}", null);
    }
    /// <summary>
    /// Modifica un sector.
    /// </summary>
    /// <param name="id">El id del sector.</param>
    /// <param name="nuevoSectorDto">El sector modificado</param>
    /// <returns>Nada.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> ModifySector(int id, [FromBody] SectorDto nuevoSectorDto)
    {
        if (id != nuevoSectorDto.Id) return BadRequest(); // No se puede modificar el sector si los ids no concuerdan o si se usan ids de relaciones no existentes.
        await _sectorService.UpdateSectorAsync(nuevoSectorDto);
        return NoContent();
    }
    /// <summary>
    /// Elimina un sector del sistema.
    /// </summary>
    /// <param name="id">El id del sector a eliminar.</param>
    /// <returns>Nada.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSector(int id)
    {
        await _sectorService.DeleteSectorAsync(id); // Borra el sector de la tabla.
        return NoContent();
    }
}
