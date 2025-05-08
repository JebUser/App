using Microsoft.AspNetCore.Mvc;
using IMCAPI.Core.DTO;
using IMCAPI.Core.Interfaces.Services;
using IMCAPI.Core.Entities;
[ApiController]
[Route("api/[controller]")]
public class TipobenesController : ControllerBase
{
    private readonly ITipobeneService _tipobeneService;

    public TipobenesController(ITipobeneService tipobeneService)
    {
        _tipobeneService = tipobeneService;
    }

    /// <summary>
    /// Obtiene la lista de tipobenes almacenados.
    /// </summary>
    /// <returns>Lista de tipobenes.</returns>
    [HttpGet]
    public async Task<IActionResult> GetTipobenes()
    {
        var beneficiarioes = await _tipobeneService.GetTipobenesAsync(); // Obtiene la lista de tipobenes disponibles.
        if (beneficiarioes == null) return NotFound();
        return Ok(beneficiarioes);
    }
    /// <summary>
    /// Buscar un tipobene por su id único.
    /// </summary>
    /// <param name="id">El id del tipobene.</param>
    /// <returns>El tipobene deseado.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTipobeneId(int id)
    {
        var tipobene = await _tipobeneService.GetTipobeneByIdAsync(id); // Busca el tipobene.
        if (tipobene == null) return NotFound();
        return Ok(tipobene);
    }

    /// <summary>
    /// Agrega un nuevo tipobene al sistema.
    /// </summary>
    /// <param name="beneficiariodto">El tipobene que se quiere agregar</param>
    [HttpPost]
    public async Task<IActionResult> CreateTipobene([FromBody] TipobeneDto beneficiariodto)
    {
        await _tipobeneService.AddTipobeneAsync(beneficiariodto); // Instrucción de agregar el nuevo tipobene.
        return Created($"/api/Tipobenes/{beneficiariodto.Id}", null);
    }
    /// <summary>
    /// Modifica un tipobene.
    /// </summary>
    /// <param name="id">El id del tipobene.</param>
    /// <param name="nuevoTipobeneDto">El tipobene modificado</param>
    /// <returns>Nada.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> ModifyTipobene(int id, [FromBody] TipobeneDto nuevoTipobeneDto)
    {
        if (id != nuevoTipobeneDto.Id) return BadRequest(); // No se puede modificar el tipobene si los ids no concuerdan o si se usan ids de relaciones no existentes.
        await _tipobeneService.UpdateTipobeneAsync(nuevoTipobeneDto);
        return NoContent();
    }
    /// <summary>
    /// Elimina un tipobene del sistema.
    /// </summary>
    /// <param name="id">El id del tipobene a eliminar.</param>
    /// <returns>Nada.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTipobene(int id)
    {
        await _tipobeneService.DeleteTipobeneAsync(id); // Borra el tipobene de la tabla.
        return NoContent();
    }
}
