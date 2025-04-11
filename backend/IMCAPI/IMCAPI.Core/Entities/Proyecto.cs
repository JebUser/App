namespace IMCAPI.Core.Entities
{
    public class Proyecto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public DateTime Fechainicio { get; set; } = default;
        public DateTime FechaFinal { get; set; } = default;
        public int Municipios_id { get; set; } = 0;
        public required int Tipoid { get; set; }

        // Relaciones.
        public required Tipoproyecto tipoproyecto { get; set; }
        public required Municipio municipio { get; set; }
        // Relación muchos a muchos.
        public List<BeneficiarioProyecto> beneficiarioProyectos { get; set; } = new();
    }
}
