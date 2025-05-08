using Microsoft.AspNetCore.Mvc;
using IMCAPI.Core.DTO;
using IMCAPI.Core.Interfaces.Services;
using IMCAPI.Core.Entities;
[ApiController]
[Route("api/[controller]")]
public class TipoactividadesController : ControllerBase
{
    private readonly ITipoactividadService _tipoactividadService;

    public TipoactividadesController(ITipoactividadService tipoactividadService)
    {
        _tipoactividadService = tipoactividadService;
    }

    /// <summary>
    /// Obtiene la lista de tipoactividades almacenados.
    /// </summary>
    /// <returns>Lista de tipoactividades.</returns>
    [HttpGet]
    public async Task<IActionResult> GetTipotipoactividades()
    {
        var tipoactividades = await _tipoactividadService.GetTipoactividadesAsync(); // Obtiene la lista de tipoactividades disponibles.
        if (tipoactividades == null) return NotFound();
        return Ok(tipoactividades);
    }
    /// <summary>
    /// Buscar un tipoactividad por su id único.
    /// </summary>
    /// <param name="id">El id del tipoactividad.</param>
    /// <returns>El tipoactividad deseado.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTipoactividadId(int id)
    {
        var tipoactividad = await _tipoactividadService.GetTipoactividadByIdAsync(id); // Busca el tipoactividad.
        if (tipoactividad == null) return NotFound();
        return Ok(tipoactividad);
    }

    /// <summary>
    /// Agrega un nuevo tipoactividad al sistema.
    /// </summary>
    /// <param name="tipoactividaddto">El tipoactividad que se quiere agregar</param>
    [HttpPost]
    public async Task<IActionResult> CreateTipoactividad([FromBody] TipoactividadDto tipoactividaddto)
    {
        await _tipoactividadService.AddTipoactividadAsync(tipoactividaddto); // Instrucción de agregar el nuevo actividad.
        return Created($"/api/Tipoactividades/{tipoactividaddto.Id}", null);
    }
    /// <summary>
    /// Modifica un tipoactividad.
    /// </summary>
    /// <param name="id">El id del tipoactividad.</param>
    /// <param name="nuevoTipoactividadDto">El tipoactividad modificado</param>
    /// <returns>Nada.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> ModifyTipoactividad(int id, [FromBody] TipoactividadDto nuevoTipoactividadDto)
    {
        if (id != nuevoTipoactividadDto.Id) return BadRequest(); // No se puede modificar el tipoactividad si los ids no concuerdan o si se usan ids de relaciones no existentes.
        await _tipoactividadService.UpdateTipoactividadAsync(nuevoTipoactividadDto);
        return NoContent();
    }
    /// <summary>
    /// Elimina un tipoactividad del sistema.
    /// </summary>
    /// <param name="id">El id del tipoactividad a eliminar.</param>
    /// <returns>Nada.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTipoactividad(int id)
    {
        await _tipoactividadService.DeleteTipoactividadAsync(id); // Borra el actividad de la tabla.
        return NoContent();
    }
}
