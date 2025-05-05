namespace IMCAPI.Core.Entities
{
    public class ActividadProyecto
    {
        public required int Proyectos_id {  get; set; }
        public required int Actividades_id { get; set; }

        // Datos a obtener de las claves foráneas.
        public required Proyecto proyecto { get; set; }
        public required Actividad actividad { get; set; }
    }
}
