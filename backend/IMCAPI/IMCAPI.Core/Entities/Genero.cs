namespace IMCAPI.Core.Entities
{
    public class Genero
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }

        // Relación con Beneficiarios.
        public List<Beneficiario> beneficiarios { get; set; }
    }
}
