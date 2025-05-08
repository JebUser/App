using Microsoft.AspNetCore.Mvc;
using IMCAPI.Core.DTO;
using IMCAPI.Core.Interfaces.Services;
using IMCAPI.Core.Entities;
[ApiController]
[Route("api/[controller]")]
public class MunicipiosController : ControllerBase
{
    private readonly IMunicipioService _municipioService;

    public MunicipiosController(IMunicipioService municipioService)
    {
        _municipioService = municipioService;
    }

    /// <summary>
    /// Obtiene la lista de municipios almacenados.
    /// </summary>
    /// <returns>Lista de municipios.</returns>
    [HttpGet]
    public async Task<IActionResult> GetMunicipios()
    {
        var municipios = await _municipioService.GetMunicipiosAsync(); // Obtiene la lista de municipios disponibles.
        if (municipios == null) return NotFound();
        return Ok(municipios);
    }
    /// <summary>
    /// Buscar un municipio por su id único.
    /// </summary>
    /// <param name="id">El id del municipio.</param>
    /// <returns>El municipio deseado.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMunicipioId(int id)
    {
        var municipio = await _municipioService.GetMunicipioIdAsync(id); // Busca el municipio.
        if (municipio == null) return NotFound();
        return Ok(municipio);
    }
}
