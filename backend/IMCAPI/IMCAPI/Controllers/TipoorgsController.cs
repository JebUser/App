using Microsoft.AspNetCore.Mvc;
using IMCAPI.Core.DTO;
using IMCAPI.Core.Interfaces.Services;
using IMCAPI.Core.Entities;
[ApiController]
[Route("api/[controller]")]
public class TipoorgsController : ControllerBase
{
    private readonly ITipoorgService _tipoorgService;

    public TipoorgsController(ITipoorgService tipoorgService)
    {
        _tipoorgService = tipoorgService;
    }

    /// <summary>
    /// Obtiene la lista de tipoorgs almacenados.
    /// </summary>
    /// <returns>Lista de tipoorgs.</returns>
    [HttpGet]
    public async Task<IActionResult> GetTipoorgs()
    {
        var beneficiarioes = await _tipoorgService.GetTipoorgsAsync(); // Obtiene la lista de tipoorgs disponibles.
        if (beneficiarioes == null) return NotFound();
        return Ok(beneficiarioes);
    }
    /// <summary>
    /// Buscar un tipoorg por su id único.
    /// </summary>
    /// <param name="id">El id del tipoorg.</param>
    /// <returns>El tipoorg deseado.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTipoorgId(int id)
    {
        var tipoorg = await _tipoorgService.GetTipoorgByIdAsync(id); // Busca el tipoorg.
        if (tipoorg == null) return NotFound();
        return Ok(tipoorg);
    }

    /// <summary>
    /// Agrega un nuevo tipoorg al sistema.
    /// </summary>
    /// <param name="beneficiariodto">El tipoorg que se quiere agregar</param>
    [HttpPost]
    public async Task<IActionResult> CreateTipoorg([FromBody] TipoorgDto beneficiariodto)
    {
        await _tipoorgService.AddTipoorgAsync(beneficiariodto); // Instrucción de agregar el nuevo tipoorg.
        return Created($"/api/Tipoorgs/{beneficiariodto.Id}", null);
    }
    /// <summary>
    /// Modifica un tipoorg.
    /// </summary>
    /// <param name="id">El id del tipoorg.</param>
    /// <param name="nuevoTipoorgDto">El tipoorg modificado</param>
    /// <returns>Nada.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> ModifyTipoorg(int id, [FromBody] TipoorgDto nuevoTipoorgDto)
    {
        if (id != nuevoTipoorgDto.Id) return BadRequest(); // No se puede modificar el tipoorg si los ids no concuerdan o si se usan ids de relaciones no existentes.
        await _tipoorgService.UpdateTipoorgAsync(nuevoTipoorgDto);
        return NoContent();
    }
    /// <summary>
    /// Elimina un tipoorg del sistema.
    /// </summary>
    /// <param name="id">El id del tipoorg a eliminar.</param>
    /// <returns>Nada.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTipoorg(int id)
    {
        await _tipoorgService.DeleteTipoorgAsync(id); // Borra el tipoorg de la tabla.
        return NoContent();
    }
}
