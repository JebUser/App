using IMCAPI.Core.Entities;

namespace IMCAPI.Core.DTO
{
    public class ProyectoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Fechainicio { get; set; }
        public DateTime Fechafinal {  get; set; }
        public TipoproyectoDto? tipoproyecto { get; set; }
        public List<ActividadDto> actividades { get; set; }

        public ProyectoDto(int id, string nombre, DateTime fechainicio, DateTime fechafinal, TipoproyectoDto tipoproyecto, List<ActividadDto> actividades)
        {
            Id = id;
            Nombre = nombre;
            Fechainicio = fechainicio;
            Fechafinal = fechafinal;
            this.tipoproyecto = tipoproyecto;
            this.actividades = actividades;
        }
    }
}
