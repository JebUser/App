namespace IMCAPI.Core.Entities
{
    public class Rol
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }

        // Relación con Usuarios.
        public required List<Usuario> usuarios { get; set; }
    }
}
