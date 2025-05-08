using Microsoft.AspNetCore.Mvc;
using IMCAPI.Core.DTO;
using IMCAPI.Core.Interfaces.Services;
using IMCAPI.Core.Entities;
[ApiController]
[Route("api/[controller]")]
public class TipoidensController : ControllerBase
{
    private readonly ITipoidenService _tipoidenService;

    public TipoidensController(ITipoidenService tipoidenService)
    {
        _tipoidenService = tipoidenService;
    }

    /// <summary>
    /// Obtiene la lista de tipoidens almacenados.
    /// </summary>
    /// <returns>Lista de tipoidens.</returns>
    [HttpGet]
    public async Task<IActionResult> GetTipoidens()
    {
        var tipoidens = await _tipoidenService.GetTipoidensAsync(); // Obtiene la lista de tipoidens disponibles.
        if (tipoidens == null) return NotFound();
        return Ok(tipoidens);
    }
    /// <summary>
    /// Buscar un tipoiden por su id único.
    /// </summary>
    /// <param name="id">El id del tipoiden.</param>
    /// <returns>El tipoiden deseado.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTipoidenId(int id)
    {
        var tipoiden = await _tipoidenService.GetTipoidenByIdAsync(id); // Busca el tipoiden.
        if (tipoiden == null) return NotFound();
        return Ok(tipoiden);
    }

    /// <summary>
    /// Agrega un nuevo tipoiden al sistema.
    /// </summary>
    /// <param name="beneficiariodto">El tipoiden que se quiere agregar</param>
    [HttpPost]
    public async Task<IActionResult> CreateTipoiden([FromBody] TipoidenDto beneficiariodto)
    {
        await _tipoidenService.AddTipoidenAsync(beneficiariodto); // Instrucción de agregar el nuevo tipoiden.
        return Created($"/api/Tipoidens/{beneficiariodto.Id}", null);
    }
    /// <summary>
    /// Modifica un tipoiden.
    /// </summary>
    /// <param name="id">El id del tipoiden.</param>
    /// <param name="nuevoTipoidenDto">El tipoiden modificado</param>
    /// <returns>Nada.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> ModifyTipoiden(int id, [FromBody] TipoidenDto nuevoTipoidenDto)
    {
        if (id != nuevoTipoidenDto.Id) return BadRequest(); // No se puede modificar el tipoiden si los ids no concuerdan o si se usan ids de relaciones no existentes.
        await _tipoidenService.UpdateTipoidenAsync(nuevoTipoidenDto);
        return NoContent();
    }
    /// <summary>
    /// Elimina un tipoiden del sistema.
    /// </summary>
    /// <param name="id">El id del tipoiden a eliminar.</param>
    /// <returns>Nada.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTipoiden(int id)
    {
        await _tipoidenService.DeleteTipoidenAsync(id); // Borra el tipoiden de la tabla.
        return NoContent();
    }
}
