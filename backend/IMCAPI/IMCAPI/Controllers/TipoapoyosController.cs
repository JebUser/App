using Microsoft.AspNetCore.Mvc;
using IMCAPI.Core.DTO;
using IMCAPI.Core.Interfaces.Services;
using IMCAPI.Core.Entities;
[ApiController]
[Route("api/[controller]")]
public class TipoapoyosController : ControllerBase
{
    private readonly ITipoapoyoService _tipoapoyoService;

    public TipoapoyosController(ITipoapoyoService tipoapoyoService)
    {
        _tipoapoyoService = tipoapoyoService;
    }

    /// <summary>
    /// Obtiene la lista de tipoapoyos almacenados.
    /// </summary>
    /// <returns>Lista de tipoapoyos.</returns>
    [HttpGet]
    public async Task<IActionResult> GetTipoapoyos()
    {
        var beneficiarioes = await _tipoapoyoService.GetTipoapoyosAsync(); // Obtiene la lista de tipoapoyos disponibles.
        if (beneficiarioes == null) return NotFound();
        return Ok(beneficiarioes);
    }
    /// <summary>
    /// Buscar un tipoapoyo por su id único.
    /// </summary>
    /// <param name="id">El id del tipoapoyo.</param>
    /// <returns>El tipoapoyo deseado.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTipoapoyoId(int id)
    {
        var tipoapoyo = await _tipoapoyoService.GetTipoapoyoByIdAsync(id); // Busca el tipoapoyo.
        if (tipoapoyo == null) return NotFound();
        return Ok(tipoapoyo);
    }

    /// <summary>
    /// Agrega un nuevo tipoapoyo al sistema.
    /// </summary>
    /// <param name="beneficiariodto">El tipoapoyo que se quiere agregar</param>
    [HttpPost]
    public async Task<IActionResult> CreateTipoapoyo([FromBody] TipoapoyoDto beneficiariodto)
    {
        await _tipoapoyoService.AddTipoapoyoAsync(beneficiariodto); // Instrucción de agregar el nuevo tipoapoyo.
        return Created($"/api/Tipoapoyos/{beneficiariodto.Id}", null);
    }
    /// <summary>
    /// Modifica un tipoapoyo.
    /// </summary>
    /// <param name="id">El id del tipoapoyo.</param>
    /// <param name="nuevoTipoapoyoDto">El tipoapoyo modificado</param>
    /// <returns>Nada.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> ModifyTipoapoyo(int id, [FromBody] TipoapoyoDto nuevoTipoapoyoDto)
    {
        if (id != nuevoTipoapoyoDto.Id) return BadRequest(); // No se puede modificar el tipoapoyo si los ids no concuerdan o si se usan ids de relaciones no existentes.
        await _tipoapoyoService.UpdateTipoapoyoAsync(nuevoTipoapoyoDto);
        return NoContent();
    }
    /// <summary>
    /// Elimina un tipoapoyo del sistema.
    /// </summary>
    /// <param name="id">El id del tipoapoyo a eliminar.</param>
    /// <returns>Nada.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTipoapoyo(int id)
    {
        await _tipoapoyoService.DeleteTipoapoyoAsync(id); // Borra el tipoapoyo de la tabla.
        return NoContent();
    }
}
