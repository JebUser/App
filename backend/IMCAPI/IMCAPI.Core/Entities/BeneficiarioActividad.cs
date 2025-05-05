namespace IMCAPI.Core.Entities
{
    public class BeneficiarioActividad
    {
        public required int Actividades_id {  get; set; }
        public required int Beneficiarios_id { get; set; }

        // Datos a obtener de las claves foráneas.
        public required Actividad actividad { get; set; }
        public required Beneficiario beneficiario { get; set; }
    }
}
