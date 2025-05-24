using Microsoft.AspNetCore.Mvc;
using IMCAPI.Core.DTO;
using IMCAPI.Core.Interfaces.Services;
using IMCAPI.Core.Entities;
[ApiController]
[Route("api/[controller]")]
public class LugaresController : ControllerBase
{
    private readonly ILugarService _lugarService;

    public LugaresController(ILugarService lugarService)
    {
        _lugarService = lugarService;
    }

    /// <summary>
    /// Obtiene la lista de lugares almacenados.
    /// </summary>
    /// <returns>Lista de lugares.</returns>
    [HttpGet]
    public async Task<IActionResult> GetLugares()
    {
        var lugares = await _lugarService.GetLugaresAsync(); // Obtiene la lista de lugares disponibles.
        if (lugares == null) return NotFound();
        return Ok(lugares);
    }
    /// <summary>
    /// Buscar un lugar por su id único.
    /// </summary>
    /// <param name="nombre">El nombre del lugar.</param>
    /// <returns>El lugar deseado.</returns>
    [HttpGet("{nombre}")]
    public async Task<IActionResult> GetLugarNombre(string nombre)
    {
        var lugar = await _lugarService.GetLugarNombreAsync(nombre); // Busca el lugar.
        if (lugar == null) return NotFound();
        return Ok(lugar);
    }

    /// <summary>
    /// Agrega un nuevo lugar al sistema.
    /// </summary>
    /// <param name="beneficiariodto">El lugar que se quiere agregar</param>
    [HttpPost]
    public async Task<IActionResult> CreateLugar([FromBody] LugarDto beneficiariodto)
    {
        await _lugarService.AddLugarAsync(beneficiariodto); // Instrucción de agregar el nuevo lugar.
        return Created($"/api/Lugares/{beneficiariodto.Id}", null);
    }
    /// <summary>
    /// Modifica un lugar.
    /// </summary>
    /// <param name="id">El id del lugar.</param>
    /// <param name="nuevoLugarDto">El lugar modificado</param>
    /// <returns>Nada.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> ModifyLugar(int id, [FromBody] LugarDto nuevoLugarDto)
    {
        if (id != nuevoLugarDto.Id) return BadRequest(); // No se puede modificar el lugar si los ids no concuerdan o si se usan ids de relaciones no existentes.
        await _lugarService.UpdateLugarAsync(nuevoLugarDto);
        return NoContent();
    }
    /// <summary>
    /// Elimina un lugar del sistema.
    /// </summary>
    /// <param name="id">El id del lugar a eliminar.</param>
    /// <returns>Nada.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLugar(int id)
    {
        await _lugarService.DeleteLugarAsync(id); // Borra el lugar de la tabla.
        return NoContent();
    }
}
