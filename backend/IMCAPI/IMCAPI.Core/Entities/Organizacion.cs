namespace IMCAPI.Core.Entities
{
    public class Organizacion
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public required int Municipios_id { get; set; }
        public string Nit {  get; set; } = string.Empty;
        public int Integrantes { get; set; } = -1;
        public int Nummujeres { get; set; } = -1;
        public float Orgmujeres { get; set; } = -1f;
        public int? Tipoorg_id { get; set; } = 0;
        public int? Tipoactividad_id { get; set; } = 0;
        public int? Lineaprod_id { get; set; } = 0;
        public int? Tipoapoyo_id { get; set; } = 0;

        // Datos a obtener de las claves foráneas.
        public Municipio municipio { get; set; }
        public Tipoorg? tipoorg { get; set; } = default;
        public Tipoactividad? tipoactividad { get; set; } = default;
        public Lineaprod? lineaprod { get; set; } = default;
        public Tipoapoyo? tipoapoyo { get; set; } = default;
        // Relación muchos a muchos.
        public List<Beneficiario> Beneficiarios { get; set; }
    }
}
