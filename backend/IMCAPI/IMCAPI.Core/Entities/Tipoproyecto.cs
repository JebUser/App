﻿namespace IMCAPI.Core.Entities
{
    public class Tipoproyecto
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }

        // Relación con Proyectos.
        public required List<Proyecto> proyectos { get; set; }
    }
}
