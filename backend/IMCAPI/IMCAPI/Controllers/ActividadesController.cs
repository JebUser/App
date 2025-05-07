using Microsoft.AspNetCore.Mvc;
using IMCAPI.Core.DTO;
using IMCAPI.Core.Interfaces.Services;
using IMCAPI.Core.Entities;
[ApiController]
[Route("api/[controller]")]
public class ActividadesController : ControllerBase
{
    private readonly IActividadService _actividadService;

    public ActividadesController(IActividadService actividadService)
    {
        _actividadService = actividadService;
    }

    /// <summary>
    /// Obtiene la lista de actividades almacenados.
    /// </summary>
    /// <returns>Lista de actividades.</returns>
    [HttpGet]
    public async Task<IActionResult> GetActividades()
    {
        var actividades = await _actividadService.GetActividadesAsync(); // Obtiene la lista de actividades disponibles.
        if (actividades == null) return NotFound();
        return Ok(actividades);
    }
    /// <summary>
    /// Buscar una actividad por su id único.
    /// </summary>
    /// <param name="id">El id de la actividad.</param>
    /// <returns>La actividad deseada.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetActividadId(int id)
    {
        var actividad = await _actividadService.GetActividadByIdAsync(id); // Busca el actividad.
        if (actividad == null) return NotFound();
        return Ok(actividad);
    }

    /// <summary>
    /// Agrega una nueva actividad al sistema.
    /// </summary>
    /// <param name="actividaddto">La actividad que se quiere agregar</param>
    [HttpPost]
    public async Task<IActionResult> CreateActividad([FromBody] ActividadDto actividaddto)
    {
        await _actividadService.AddActividadAsync(actividaddto); // Instrucción de agregar el nuevo actividad.
        return Created($"/api/Actividades/{actividaddto.Id}", null);
    }
    /// <summary>
    /// Modifica una actividad.
    /// </summary>
    /// <param name="id">El id de la actividad.</param>
    /// <param name="nuevoActividadDto">La actividad modificada</param>
    /// <returns>Nada.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> ModifyActividad(int id, [FromBody] ActividadDto nuevoActividadDto)
    {
        if (id != nuevoActividadDto.Id) return BadRequest(); // No se puede modificar el actividad si los ids no concuerdan o si se usan ids de relaciones no existentes.
        await _actividadService.UpdateActividadAsync(nuevoActividadDto);
        return NoContent();
    }
    /// <summary>
    /// Elimina una actividad del sistema.
    /// </summary>
    /// <param name="id">El id de la actividad a eliminar.</param>
    /// <returns>Nada.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteActividad(int id)
    {
        await _actividadService.DeleteActividadAsync(id); // Borra el actividad de la tabla.
        return NoContent();
    }
}
