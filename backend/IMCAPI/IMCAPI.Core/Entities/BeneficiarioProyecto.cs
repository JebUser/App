namespace IMCAPI.Core.Entities
{
    public class BeneficiarioProyecto
    {
        public required int Proyectoid {  get; set; }
        public required int Beneficiarios_id { get; set; }
        public required DateTime Fechaparticipacion { get; set; }

        // Datos a obtener de las claves foráneas.
        public required Proyecto proyecto { get; set; }
        public required Beneficiario beneficiario { get; set; }
    }
}
