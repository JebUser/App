namespace IMCAPI.Core.Entities
{
    public class Municipio
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required int Departamentos_id { get; set; }

        // Relación con su departamento.
        public required Departamento departamento { get; set; }
        // Relación con Beneficiarios.
        public required List<Beneficiario> beneficiarios { get; set; }
        // Relación con Organizaciones.
        public required List<Organizacion> organizaciones { get; set; }
    }
}
