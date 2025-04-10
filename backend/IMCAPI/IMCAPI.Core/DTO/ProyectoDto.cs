namespace IMCAPI.Core.DTO
{
    public class ProyectoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string municipio { get; set; }
        public DateTime Fechainicio { get; set; }
        public DateTime Fechafinal {  get; set; }

        public ProyectoDto(int id, string nombre, string Municipio, DateTime fechainicio, DateTime fechafinal)
        {
            Id = id;
            Nombre = nombre;
            municipio = Municipio;
            Fechainicio = fechainicio;
            Fechafinal = fechafinal;
        }
    }
}
