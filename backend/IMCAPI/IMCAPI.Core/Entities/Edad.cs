namespace IMCAPI.Core.Entities
{
    public class Edad
    {
        public int Id { get; set; }
        public required string Rango { get; set; }

        // Relación con Beneficiarios.
        public required List<Beneficiario> beneficiarios { get; set; }
    }
}
