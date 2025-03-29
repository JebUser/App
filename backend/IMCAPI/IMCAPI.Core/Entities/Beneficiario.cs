namespace IMCAPI.Core.Entities
{
    public class Beneficiario
    {
        public int Id { get; set; }
        public string Identificacion { get; set; } = string.Empty;
        public required string Nombre1 { get; set; }
        public string Nombre2 { get; set; } = string.Empty;
        public required string Apellido1 { get; set; }
        public string Apellido2 { get; set; } = string.Empty;
        public string Celular { get; set; } = string.Empty;
        public int Tipoiden_id { get; set; } = 0;
        public int Generos_id { get; set; } = 0;
        public int Edades_id { get; set; } = 0;
        public byte[] Firma { get; set; } = default;
        public int Grupoetnico_id { get; set; } = 0;
        public int Tipobene_id { get; set; } = 0;
        public int Municipios_id { get; set; } = 0;
        public int Sectores_id { get; set; } = 0;

        // Datos a obtener de las claves foráneas.
        public Tipoiden tipoiden { get; set; } = default;
        public Genero genero { get; set; } = default;
        public Edad Rangoedad { get; set; } = default;
        public Grupoetnico grupoetnico { get; set; } = default;
        public Tipobene tipobene { get; set; } = default;
        public Municipio municipio { get; set; } = default;
        public Sector sector { get; set; } = default;

        // Relaciones muchos a muchos.
        public required List<BeneficiarioProyecto> beneficiarioProyectos { get; set; }
    }
}
