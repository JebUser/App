using Microsoft.AspNetCore.Mvc;
using IMCAPI.Core.DTO;
using IMCAPI.Core.Interfaces.Services;
using IMCAPI.Core.Entities;
[ApiController]
[Route("api/[controller]")]
public class ProyectosController : ControllerBase
{
    private readonly IProyectoService _proyectoService;

    public ProyectosController(IProyectoService proyectoService)
    {
        _proyectoService = proyectoService;
    }

    /// <summary>
    /// Obtiene la lista de proyectos almacenados.
    /// </summary>
    /// <returns>Lista de proyectos.</returns>
    [HttpGet]
    public async Task<IActionResult> GetProyectos()
    {
        var proyectos = await _proyectoService.GetProyectosAsync(); // Obtiene la lista de proyectos disponibles.
        if (proyectos == null) return NotFound();
        return Ok(proyectos);
    }
    /// <summary>
    /// Buscar un proyecto por su id único.
    /// </summary>
    /// <param name="id">El id del proyecto.</param>
    /// <returns>El proyecto deseado.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProyectoId(int id)
    {
        var proyecto = await _proyectoService.GetProyectoIdAsync(id); // Busca el proyecto.
        if (proyecto == null) return NotFound();
        return Ok(proyecto);
    }
    /// <summary>
    /// Obtiene los beneficiarios vinculados a un proyecto.
    /// </summary>
    /// <param name="id">El id del proyecto.</param>
    /// <returns>La lista de beneficiarios en el proyecto.</returns>
    [HttpGet("proyectoid/{id}")]
    public async Task<IActionResult> GetBeneficiariosOfProyecto(int id)
    {
        var beneficiarios = await _proyectoService.GetBeneficiariosInProyectoAsync(id);
        if (beneficiarios == null) return NotFound();
        return Ok(beneficiarios);
    }
    /// <summary>
    /// Agrega un nuevo proyecto al sistema.
    /// </summary>
    /// <param name="proyectodto">El proyecto que se quiere agregar</param>
    [HttpPost]
    public async Task<IActionResult> CreateProyecto([FromBody] CreateProyectoDto proyectodto)
    {
        var proyecto = new Proyecto
        {
            Nombre = proyectodto.Nombre,
            Fechainicio = proyectodto.Fechainicio,
            FechaFinal = proyectodto.FechaFinal,
            Municipios_id = proyectodto.Municipios_id,
            Tipoid = proyectodto.Tipoid,
            // Inicializaciones vacías que resolverá automáticamente EF Core.
            municipio = null!,
            tipoproyecto = null!,
            beneficiarioProyectos = new List<BeneficiarioProyecto>()
        };
        if (!_proyectoService.TheIdsAreCorrect(proyecto)) return BadRequest(); // No se puede agregar si las relaciones no existen.
        await _proyectoService.AddProyectoAsync(proyecto); // Instrucción de agregar el nuevo proyecto.
        return Created($"/api/Proyectos/{proyecto.Id}", null);
    }
    /// <summary>
    /// Modifica un proyecto.
    /// </summary>
    /// <param name="id">El id del proyecto.</param>
    /// <param name="nuevoProyecto">El proyecto modificado</param>
    /// <returns>Nada.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> ModifyProyecto(int id, [FromBody] Proyecto nuevoProyecto)
    {
        if (id != nuevoProyecto.Id || !_proyectoService.TheIdsAreCorrect(nuevoProyecto)) return BadRequest(); // No se puede modificar el proyecto si los ids no concuerdan o si se usan ids de relaciones no existentes.
        await _proyectoService.UpdateProyectoAsync(nuevoProyecto);
        return NoContent();
    }
    /// <summary>
    /// Elimina un proyecto del sistema.
    /// </summary>
    /// <param name="id">El id del proyecto a eliminar.</param>
    /// <returns>Nada.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProyecto(int id)
    {
        await _proyectoService.DeleteProyectoAsync(id); // Borra el proyecto de la tabla.
        return NoContent();
    }
}
