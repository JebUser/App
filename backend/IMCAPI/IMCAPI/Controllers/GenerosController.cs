using Microsoft.AspNetCore.Mvc;
using IMCAPI.Core.DTO;
using IMCAPI.Core.Interfaces.Services;
using IMCAPI.Core.Entities;
[ApiController]
[Route("api/[controller]")]
public class GenerosController : ControllerBase
{
    private readonly IGeneroService _generoService;

    public GenerosController(IGeneroService generoService)
    {
        _generoService = generoService;
    }

    /// <summary>
    /// Obtiene la lista de generos almacenados.
    /// </summary>
    /// <returns>Lista de generos.</returns>
    [HttpGet]
    public async Task<IActionResult> GetGeneros()
    {
        var generoes = await _generoService.GetGenerosAsync(); // Obtiene la lista de generos disponibles.
        if (generoes == null) return NotFound();
        return Ok(generoes);
    }
    /// <summary>
    /// Buscar un genero por su id único.
    /// </summary>
    /// <param name="id">El id del genero.</param>
    /// <returns>El genero deseado.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetGeneroId(int id)
    {
        var genero = await _generoService.GetGeneroByIdAsync(id); // Busca el genero.
        if (genero == null) return NotFound();
        return Ok(genero);
    }

    /// <summary>
    /// Agrega un nuevo genero al sistema.
    /// </summary>
    /// <param name="generodto">El genero que se quiere agregar</param>
    [HttpPost]
    public async Task<IActionResult> CreateGenero([FromBody] GeneroDto generodto)
    {
        await _generoService.AddGeneroAsync(generodto); // Instrucción de agregar el nuevo genero.
        return Created($"/api/Generos/{generodto.Id}", null);
    }
    /// <summary>
    /// Modifica un genero.
    /// </summary>
    /// <param name="id">El id del genero.</param>
    /// <param name="nuevoGeneroDto">El genero modificado</param>
    /// <returns>Nada.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> ModifyGenero(int id, [FromBody] GeneroDto nuevoGeneroDto)
    {
        if (id != nuevoGeneroDto.Id) return BadRequest(); // No se puede modificar el genero si los ids no concuerdan o si se usan ids de relaciones no existentes.
        await _generoService.UpdateGeneroAsync(nuevoGeneroDto);
        return NoContent();
    }
    /// <summary>
    /// Elimina un genero del sistema.
    /// </summary>
    /// <param name="id">El id del genero a eliminar.</param>
    /// <returns>Nada.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGenero(int id)
    {
        await _generoService.DeleteGeneroAsync(id); // Borra el genero de la tabla.
        return NoContent();
    }
}
