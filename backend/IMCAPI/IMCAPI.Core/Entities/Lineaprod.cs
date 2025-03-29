namespace IMCAPI.Core.Entities
{
    public class Lineaprod
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }

        // Relación con Organizaciones.
        public required List<Organizacion> organizaciones { get; set; }
    }
}
