namespace IMCAPI.Core.Entities
{
    public class Proyecto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public DateTime Fechainicio { get; set; } = default;
        public DateTime FechaFinal { get; set; } = default;
        public int? Tipoid { get; set; }
        // Relaciones.
        public Tipoproyecto tipoproyecto { get; set; }
        // Relación muchos a muchos.
        public List<Actividad> actividades { get; set; } = new();
    }
}
