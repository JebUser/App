namespace IMCAPI.Core.Entities
{
    public class Tipoapoyo
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }

        // Relación con Organizaciones.
        public List<Organizacion> organizaciones { get; set; }
    }
}
