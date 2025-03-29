namespace IMCAPI.Core.Entities
{
    public class Departamento
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }

        // Relación con sus municipios.
        public required List<Municipio> municipios { get; set; }
    }
}
