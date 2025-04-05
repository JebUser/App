namespace IMCAPI.Core.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public required string usuario {  get; set; }
        public required string Contrasena { get; set; }
        public required int Rolid { get; set; }

        // Relación uno a muchos.
        public required Rol rol { get; set; }
    }
}
