namespace IMCAPI.Core.Entities
{
    public class Proyecto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required int Tipoid { get; set; }

        // Relación muchos a muchos.
        public required List<BeneficiarioProyecto> beneficiarioProyectos { get; set; }
    }
}
