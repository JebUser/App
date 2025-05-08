using Microsoft.AspNetCore.Mvc;
using IMCAPI.Core.DTO;
using IMCAPI.Core.Interfaces.Services;
using IMCAPI.Core.Entities;
[ApiController]
[Route("api/[controller]")]
public class TipoProyectosController : ControllerBase
{
    private readonly ITipoProyectoService _tipoproyectoService;

    public TipoProyectosController(ITipoProyectoService tipoproyectoService)
    {
        _tipoproyectoService = tipoproyectoService;
    }

    /// <summary>
    /// Obtiene la lista de tipoproyectos almacenados.
    /// </summary>
    /// <returns>Lista de tipoproyectos.</returns>
    [HttpGet]
    public async Task<IActionResult> GetTipoProyectos()
    {
        var tipoproyectos = await _tipoproyectoService.GetTipoProyectosAsync(); // Obtiene la lista de tipoproyectos disponibles.
        if (tipoproyectos == null) return NotFound();
        return Ok(tipoproyectos);
    }
    /// <summary>
    /// Buscar un tipoproyecto por su id único.
    /// </summary>
    /// <param name="id">El id del tipoproyecto.</param>
    /// <returns>El tipoproyecto deseado.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTipoProyectoId(int id)
    {
        var tipoproyecto = await _tipoproyectoService.GetTipoProyectoIdAsync(id); // Busca el tipoproyecto.
        if (tipoproyecto == null) return NotFound();
        return Ok(tipoproyecto);
    }

    /// <summary>
    /// Agrega un nuevo tipoproyecto al sistema.
    /// </summary>
    /// <param name="beneficiariodto">El tipoproyecto que se quiere agregar</param>
    [HttpPost]
    public async Task<IActionResult> CreateTipoProyecto([FromBody] TipoproyectoDto beneficiariodto)
    {
        await _tipoproyectoService.AddTipoProyectoAsync(beneficiariodto); // Instrucción de agregar el nuevo tipoproyecto.
        return Created($"/api/TipoProyectos/{beneficiariodto.Id}", null);
    }
    /// <summary>
    /// Modifica un tipoproyecto.
    /// </summary>
    /// <param name="id">El id del tipoproyecto.</param>
    /// <param name="nuevoTipoProyectoDto">El tipoproyecto modificado</param>
    /// <returns>Nada.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> ModifyTipoProyecto(int id, [FromBody] TipoproyectoDto nuevoTipoProyectoDto)
    {
        if (id != nuevoTipoProyectoDto.Id) return BadRequest(); // No se puede modificar el tipoproyecto si los ids no concuerdan o si se usan ids de relaciones no existentes.
        await _tipoproyectoService.UpdateTipoProyectoAsync(nuevoTipoProyectoDto);
        return NoContent();
    }
    /// <summary>
    /// Elimina un tipoproyecto del sistema.
    /// </summary>
    /// <param name="id">El id del tipoproyecto a eliminar.</param>
    /// <returns>Nada.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTipoProyecto(int id)
    {
        await _tipoproyectoService.DeleteTipoProyectoAsync(id); // Borra el tipoproyecto de la tabla.
        return NoContent();
    }
}
