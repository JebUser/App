using Microsoft.AspNetCore.Mvc;
using IMCAPI.Core.DTO;
using IMCAPI.Core.Interfaces.Services;
using IMCAPI.Core.Entities;
[ApiController]
[Route("api/[controller]")]
public class GrupoetnicosController : ControllerBase
{
    private readonly IGrupoetnicoService _grupoetnicoService;

    public GrupoetnicosController(IGrupoetnicoService grupoetnicoService)
    {
        _grupoetnicoService = grupoetnicoService;
    }

    /// <summary>
    /// Obtiene la lista de grupoetnicos almacenados.
    /// </summary>
    /// <returns>Lista de grupoetnicos.</returns>
    [HttpGet]
    public async Task<IActionResult> GetGrupoetnicos()
    {
        var beneficiarioes = await _grupoetnicoService.GetGrupoetnicosAsync(); // Obtiene la lista de grupoetnicos disponibles.
        if (beneficiarioes == null) return NotFound();
        return Ok(beneficiarioes);
    }
    /// <summary>
    /// Buscar un grupoetnico por su id único.
    /// </summary>
    /// <param name="id">El id del grupoetnico.</param>
    /// <returns>El grupoetnico deseado.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetGrupoetnicoId(int id)
    {
        var grupoetnico = await _grupoetnicoService.GetGrupoetnicoByIdAsync(id); // Busca el grupoetnico.
        if (grupoetnico == null) return NotFound();
        return Ok(grupoetnico);
    }

    /// <summary>
    /// Agrega un nuevo grupoetnico al sistema.
    /// </summary>
    /// <param name="beneficiariodto">El grupoetnico que se quiere agregar</param>
    [HttpPost]
    public async Task<IActionResult> CreateGrupoetnico([FromBody] GrupoetnicoDto beneficiariodto)
    {
        await _grupoetnicoService.AddGrupoetnicoAsync(beneficiariodto); // Instrucción de agregar el nuevo grupoetnico.
        return Created($"/api/Grupoetnicos/{beneficiariodto.Id}", null);
    }
    /// <summary>
    /// Modifica un grupoetnico.
    /// </summary>
    /// <param name="id">El id del grupoetnico.</param>
    /// <param name="nuevoGrupoetnicoDto">El grupoetnico modificado</param>
    /// <returns>Nada.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> ModifyGrupoetnico(int id, [FromBody] GrupoetnicoDto nuevoGrupoetnicoDto)
    {
        if (id != nuevoGrupoetnicoDto.Id) return BadRequest(); // No se puede modificar el grupoetnico si los ids no concuerdan o si se usan ids de relaciones no existentes.
        await _grupoetnicoService.UpdateGrupoetnicoAsync(nuevoGrupoetnicoDto);
        return NoContent();
    }
    /// <summary>
    /// Elimina un grupoetnico del sistema.
    /// </summary>
    /// <param name="id">El id del grupoetnico a eliminar.</param>
    /// <returns>Nada.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGrupoetnico(int id)
    {
        await _grupoetnicoService.DeleteGrupoetnicoAsync(id); // Borra el grupoetnico de la tabla.
        return NoContent();
    }
}
