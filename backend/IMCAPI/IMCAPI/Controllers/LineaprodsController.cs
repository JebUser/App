using Microsoft.AspNetCore.Mvc;
using IMCAPI.Core.DTO;
using IMCAPI.Core.Interfaces.Services;
using IMCAPI.Core.Entities;
[ApiController]
[Route("api/[controller]")]
public class LineaprodsController : ControllerBase
{
    private readonly ILineaprodService _lineaprodService;

    public LineaprodsController(ILineaprodService lineaprodService)
    {
        _lineaprodService = lineaprodService;
    }

    /// <summary>
    /// Obtiene la lista de lineaprods almacenados.
    /// </summary>
    /// <returns>Lista de lineaprods.</returns>
    [HttpGet]
    public async Task<IActionResult> GetLineaprods()
    {
        var lineaprods = await _lineaprodService.GetLineaprodsAsync(); // Obtiene la lista de lineaprods disponibles.
        if (lineaprods == null) return NotFound();
        return Ok(lineaprods);
    }
    /// <summary>
    /// Buscar un lineaprod por su id único.
    /// </summary>
    /// <param name="id">El id del lineaprod.</param>
    /// <returns>El lineaprod deseado.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetLineaprodId(int id)
    {
        var lineaprod = await _lineaprodService.GetLineaprodByIdAsync(id); // Busca el lineaprod.
        if (lineaprod == null) return NotFound();
        return Ok(lineaprod);
    }

    /// <summary>
    /// Agrega un nuevo lineaprod al sistema.
    /// </summary>
    /// <param name="beneficiariodto">El lineaprod que se quiere agregar</param>
    [HttpPost]
    public async Task<IActionResult> CreateLineaprod([FromBody] LineaprodDto beneficiariodto)
    {
        await _lineaprodService.AddLineaprodAsync(beneficiariodto); // Instrucción de agregar el nuevo lineaprod.
        return Created($"/api/Lineaprods/{beneficiariodto.Id}", null);
    }
    /// <summary>
    /// Modifica un lineaprod.
    /// </summary>
    /// <param name="id">El id del lineaprod.</param>
    /// <param name="nuevoLineaprodDto">El lineaprod modificado</param>
    /// <returns>Nada.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> ModifyLineaprod(int id, [FromBody] LineaprodDto nuevoLineaprodDto)
    {
        if (id != nuevoLineaprodDto.Id) return BadRequest(); // No se puede modificar el lineaprod si los ids no concuerdan o si se usan ids de relaciones no existentes.
        await _lineaprodService.UpdateLineaprodAsync(nuevoLineaprodDto);
        return NoContent();
    }
    /// <summary>
    /// Elimina un lineaprod del sistema.
    /// </summary>
    /// <param name="id">El id del lineaprod a eliminar.</param>
    /// <returns>Nada.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLineaprod(int id)
    {
        await _lineaprodService.DeleteLineaprodAsync(id); // Borra el lineaprod de la tabla.
        return NoContent();
    }
}
