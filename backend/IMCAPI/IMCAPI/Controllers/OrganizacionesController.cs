using Microsoft.AspNetCore.Mvc;
using IMCAPI.Core.DTO;
using IMCAPI.Core.Interfaces.Services;
using IMCAPI.Core.Entities;
[ApiController]
[Route("api/[controller]")]
public class OrganizacionesController : ControllerBase
{
    private readonly IOrganizacionService _organizacionService;

    public OrganizacionesController(IOrganizacionService organizacionService)
    {
        _organizacionService = organizacionService;
    }

    /// <summary>
    /// Obtiene la lista de organizaciones almacenados.
    /// </summary>
    /// <returns>Lista de organizaciones.</returns>
    [HttpGet]
    public async Task<IActionResult> GetOrganizaciones()
    {
        var organizaciones = await _organizacionService.GetOrganizacionesAsync(); // Obtiene la lista de organizaciones disponibles.
        if (organizaciones == null) return NotFound();
        return Ok(organizaciones);
    }
    /// <summary>
    /// Buscar una organizacion por su id único.
    /// </summary>
    /// <param name="id">El id de la organizacion.</param>
    /// <returns>La organizacion deseada.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrganizacionId(int id)
    {
        var organizacion = await _organizacionService.GetOrganizacionIdAsync(id); // Busca el organizacion.
        if (organizacion == null) return NotFound();
        return Ok(organizacion);
    }

    /// <summary>
    /// Agrega una nueva organizacion al sistema.
    /// </summary>
    /// <param name="organizaciondto">La organizacion que se quiere agregar</param>
    [HttpPost]
    public async Task<IActionResult> CreateOrganizacion([FromBody] OrganizacionDto organizaciondto)
    {
        await _organizacionService.AddOrganizacionAsync(organizaciondto); // Instrucción de agregar el nuevo organizacion.
        return Created($"/api/Organizaciones/{organizaciondto.Id}", null);
    }
    /// <summary>
    /// Modifica una organizacion.
    /// </summary>
    /// <param name="id">El id de la organizacion.</param>
    /// <param name="nuevoOrganizacionDto">La organizacion modificada</param>
    /// <returns>Nada.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> ModifyOrganizacion(int id, [FromBody] OrganizacionDto nuevoOrganizacionDto)
    {
        if (id != nuevoOrganizacionDto.Id) return BadRequest(); // No se puede modificar el organizacion si los ids no concuerdan o si se usan ids de relaciones no existentes.
        await _organizacionService.UpdateOrganizacionAsync(nuevoOrganizacionDto);
        return NoContent();
    }
    /// <summary>
    /// Elimina una organizacion del sistema.
    /// </summary>
    /// <param name="id">El id de la organizacion a eliminar.</param>
    /// <returns>Nada.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrganizacion(int id)
    {
        await _organizacionService.DeleteOrganizacionAsync(id); // Borra el organizacion de la tabla.
        return NoContent();
    }
}
