namespace IMCAPI.Core.Entities
{
    public class Tipoactividad
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }

        // Relación con Organizaciones.
        public List<Organizacion> organizaciones { get; set; }
    }
}
