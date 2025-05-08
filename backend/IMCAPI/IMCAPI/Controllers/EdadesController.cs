using Microsoft.AspNetCore.Mvc;
using IMCAPI.Core.DTO;
using IMCAPI.Core.Interfaces.Services;
using IMCAPI.Core.Entities;
[ApiController]
[Route("api/[controller]")]
public class EdadesController : ControllerBase
{
    private readonly IEdadService _edadService;

    public EdadesController(IEdadService edadService)
    {
        _edadService = edadService;
    }

    /// <summary>
    /// Obtiene la lista de edades almacenados.
    /// </summary>
    /// <returns>Lista de edades.</returns>
    [HttpGet]
    public async Task<IActionResult> GetEdades()
    {
        var edades = await _edadService.GetEdadesAsync(); // Obtiene la lista de edades disponibles.
        if (edades == null) return NotFound();
        return Ok(edades);
    }
    /// <summary>
    /// Buscar una edad por su id único.
    /// </summary>
    /// <param name="id">El id de la edad.</param>
    /// <returns>La edad deseada.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEdadId(int id)
    {
        var edad = await _edadService.GetEdadByIdAsync(id); // Busca el edad.
        if (edad == null) return NotFound();
        return Ok(edad);
    }

    /// <summary>
    /// Agrega una nueva edad al sistema.
    /// </summary>
    /// <param name="edaddto">La edad que se quiere agregar</param>
    [HttpPost]
    public async Task<IActionResult> CreateEdad([FromBody] EdadDto edaddto)
    {
        await _edadService.AddEdadAsync(edaddto); // Instrucción de agregar el nuevo edad.
        return Created($"/api/Edades/{edaddto.Id}", null);
    }
    /// <summary>
    /// Modifica una edad.
    /// </summary>
    /// <param name="id">El id de la edad.</param>
    /// <param name="nuevoEdadDto">La edad modificada</param>
    /// <returns>Nada.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> ModifyEdad(int id, [FromBody] EdadDto nuevoEdadDto)
    {
        if (id != nuevoEdadDto.Id) return BadRequest(); // No se puede modificar el edad si los ids no concuerdan o si se usan ids de relaciones no existentes.
        await _edadService.UpdateEdadAsync(nuevoEdadDto);
        return NoContent();
    }
    /// <summary>
    /// Elimina una edad del sistema.
    /// </summary>
    /// <param name="id">El id de la edad a eliminar.</param>
    /// <returns>Nada.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEdad(int id)
    {
        await _edadService.DeleteEdadAsync(id); // Borra el edad de la tabla.
        return NoContent();
    }
}
