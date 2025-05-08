using Microsoft.AspNetCore.Mvc;
using IMCAPI.Core.DTO;
using IMCAPI.Core.Interfaces.Services;
using IMCAPI.Core.Entities;
[ApiController]
[Route("api/[controller]")]
public class BeneficiariosController : ControllerBase
{
    private readonly IBeneficiarioService _beneficiarioService;

    public BeneficiariosController(IBeneficiarioService beneficiarioService)
    {
        _beneficiarioService = beneficiarioService;
    }

    /// <summary>
    /// Obtiene la lista de beneficiarios almacenados.
    /// </summary>
    /// <returns>Lista de beneficiarios.</returns>
    [HttpGet]
    public async Task<IActionResult> GetBeneficiarios()
    {
        var beneficiarioes = await _beneficiarioService.GetBeneficiariosAsync(); // Obtiene la lista de beneficiarios disponibles.
        if (beneficiarioes == null) return NotFound();
        return Ok(beneficiarioes);
    }
    /// <summary>
    /// Buscar un beneficiario por su id único.
    /// </summary>
    /// <param name="id">El id del beneficiario.</param>
    /// <returns>El beneficiario deseado.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBeneficiarioId(int id)
    {
        var beneficiario = await _beneficiarioService.GetBeneficiarioIdAsync(id); // Busca el beneficiario.
        if (beneficiario == null) return NotFound();
        return Ok(beneficiario);
    }

    /// <summary>
    /// Agrega un nuevo beneficiario al sistema.
    /// </summary>
    /// <param name="beneficiariodto">El beneficiario que se quiere agregar</param>
    [HttpPost]
    public async Task<IActionResult> CreateBeneficiario([FromBody] BeneficiarioDto beneficiariodto)
    {
        await _beneficiarioService.AddBeneficiarioAsync(beneficiariodto); // Instrucción de agregar el nuevo beneficiario.
        return Created($"/api/Beneficiarios/{beneficiariodto.Id}", null);
    }
    /// <summary>
    /// Modifica un beneficiario.
    /// </summary>
    /// <param name="id">El id del beneficiario.</param>
    /// <param name="nuevoBeneficiarioDto">El beneficiario modificado</param>
    /// <returns>Nada.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> ModifyBeneficiario(int id, [FromBody] BeneficiarioDto nuevoBeneficiarioDto)
    {
        if (id != nuevoBeneficiarioDto.Id) return BadRequest(); // No se puede modificar el beneficiario si los ids no concuerdan o si se usan ids de relaciones no existentes.
        await _beneficiarioService.UpdateBeneficiarioAsync(nuevoBeneficiarioDto);
        return NoContent();
    }
    /// <summary>
    /// Elimina un beneficiario del sistema.
    /// </summary>
    /// <param name="id">El id del beneficiario a eliminar.</param>
    /// <returns>Nada.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBeneficiario(int id)
    {
        await _beneficiarioService.DeleteBeneficiarioAsync(id); // Borra el beneficiario de la tabla.
        return NoContent();
    }
}
