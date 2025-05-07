using Microsoft.EntityFrameworkCore;
using IMCAPI.Core.Entities;

namespace IMCAPI.Infrastructure.Persistence
{
    public class DbContextIMC : DbContext
    {
        public DbContextIMC(DbContextOptions<DbContextIMC> options) : base(options) { }

        public DbSet<Actividad> Actividades { get; set; } // Obtiene la tabla de actividades.
        public DbSet<Beneficiario> Beneficiarios { get; set; } // Obtiene la tabla de beneficiarios.
        public DbSet<BeneficiarioActividad> beneficiarioActividad { get; set; } // Obtiene las relaciones entre beneficiarios y proyectos.
        public DbSet<Departamento> Departamentos { get; set; } // Obtiene la tabla de departamentos.
        public DbSet<Edad> Edades { get; set; } // Obtiene la tabla de rango de edades.
        public DbSet<Genero> Generos { get; set; } // Obtiene la tabla de géneros.
        public DbSet<Grupoetnico> grupoEtnico { get; set; } // Obtiene la tabla de grupos étnicos.
        public DbSet<Lineaprod> lineaProd { get; set; } // Obtiene la tabla de líneas de producción.
        public DbSet<Lugar> Lugares { get; set; } // Obtiene la tabla de lugares.
        public DbSet<Municipio> Municipios { get; set; } // Obtiene la tabla de municipios.
        public DbSet<Organizacion> Organizaciones { get; set; } // Obtiene la tabla de organizaciones.
        public DbSet<Proyecto> Proyectos { get; set; } // Obtiene la tabla de proyectos.
        public DbSet <Sector> Sectores { get; set; } // Obtiene la tabla de sectores.
        public DbSet <Tipoactividad> tipoActividad { get; set; } // Obtiene la tabla de tipos de actividad.
        public DbSet <Tipoapoyo> tipoapoyo { get; set; } // Obtiene la tabla de los tipos de apoyo.
        public DbSet <Tipobene> tipoBene { get; set; } // Obtiene la tabla de los tipos de beneficiarios.
        public DbSet<Tipoiden> tipoiden { get; set; } // Obtiene la tabla de los tipos de identificación.
        public DbSet<Tipoorg> tipoorg { get; set; } // Obtiene la tabla de los tipos de organización.
        public DbSet<Tipoproyecto> tipoProyecto { get; set; } // Obtiene la tabla de los tipos de proyectos.

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BeneficiarioActividad>()
                .HasKey(bp => new { bp.Actividades_id, bp.Beneficiarios_id }); // PK compuesta

            // Relaciones de una actividad.
            modelBuilder.Entity<Actividad>()
                .HasOne(a => a.lugar)
                .WithMany(l => l.actividades)
                .HasForeignKey(a => a.Lugares_id);
            modelBuilder.Entity<Actividad>()
                .HasMany(a => a.beneficiarioactividad)
                .WithOne(ba => ba.actividad);

            // Relaciones de un beneficiario.
            modelBuilder.Entity<Beneficiario>()
                .HasOne(b => b.tipoiden)
                .WithMany(t => t.beneficiarios)
                .HasForeignKey(b => b.Tipoiden_id);
            modelBuilder.Entity<Beneficiario>()
                .HasOne(b => b.genero)
                .WithMany(g => g.beneficiarios)
                .HasForeignKey(b => b.Generos_id);
            modelBuilder.Entity<Beneficiario>()
                .HasOne(b => b.Rangoedad)
                .WithMany(e => e.beneficiarios)
                .HasForeignKey(b => b.Edades_id);
            modelBuilder.Entity<Beneficiario>()
                .HasOne(b => b.grupoetnico)
                .WithMany(g => g.beneficiarios)
                .HasForeignKey(b => b.Grupoetnico_id);
            modelBuilder.Entity<Beneficiario>()
                .HasOne(b => b.tipobene)
                .WithMany(t => t.beneficiarios)
                .HasForeignKey(b => b.Tipobene_id);
            modelBuilder.Entity<Beneficiario>()
                .HasOne(b => b.municipio)
                .WithMany(m => m.beneficiarios)
                .HasForeignKey(b => b.Municipios_id);
            modelBuilder.Entity<Beneficiario>()
                .HasOne(b => b.sector)
                .WithMany(s => s.beneficiarios)
                .HasForeignKey(b => b.Sectores_id);
            modelBuilder.Entity<Beneficiario>()
                .HasMany(b => b.beneficiarioactividad)
                .WithOne(bp => bp.beneficiario);
            modelBuilder.Entity<Beneficiario>()
                .HasMany(b => b.Organizaciones)
                .WithMany(o => o.Beneficiarios);

            // Relaciones de la relación beneficiario actividad.
            modelBuilder.Entity<BeneficiarioActividad>()
                .HasOne(ba => ba.beneficiario)
                .WithMany(b => b.beneficiarioactividad)
                .HasForeignKey(ba => ba.Beneficiarios_id);
            modelBuilder.Entity<BeneficiarioActividad>()
                .HasOne(ba => ba.actividad)
                .WithMany(a => a.beneficiarioactividad)
                .HasForeignKey(ba => ba.Actividades_id);

            // Relaciones de los departamentos.
            modelBuilder.Entity<Departamento>()
                .HasMany(d => d.municipios)
                .WithOne(m => m.departamento);

            // Relaciones de los rangos de edades.
            modelBuilder.Entity<Edad>()
                .HasMany(e => e.beneficiarios)
                .WithOne(b => b.Rangoedad);

            // Relaciones de los géneros.
            modelBuilder.Entity<Genero>()
                .HasMany(g => g.beneficiarios)
                .WithOne(b => b.genero);

            // Relaciones de los grupos étnicos.
            modelBuilder.Entity<Grupoetnico>()
                .HasMany(g => g.beneficiarios)
                .WithOne(b => b.grupoetnico);

            // Relaciones de las líneas de producción.
            modelBuilder.Entity<Lineaprod>()
                .HasMany(l => l.organizaciones)
                .WithOne(o => o.lineaprod);

            // Relaciones de los lugares.
            modelBuilder.Entity<Lugar>()
                .HasMany(l => l.actividades)
                .WithOne(a => a.lugar);

            // Relaciones de los municipios.
            modelBuilder.Entity<Municipio>()
                .HasOne(m => m.departamento)
                .WithMany(d => d.municipios)
                .HasForeignKey(m => m.Departamentos_id);
            modelBuilder.Entity<Municipio>()
                .HasMany(m => m.beneficiarios)
                .WithOne(b => b.municipio);

            // Relaciones de las organizaciones.
            modelBuilder.Entity<Organizacion>()
                .HasOne(o => o.municipio)
                .WithMany(m => m.organizaciones)
                .HasForeignKey(o => o.Municipios_id);
            modelBuilder.Entity<Organizacion>()
                .HasOne(o => o.tipoorg)
                .WithMany(t => t.organizaciones)
                .HasForeignKey(o => o.Tipoorg_id);
            modelBuilder.Entity<Organizacion>()
                .HasOne(o => o.tipoactividad)
                .WithMany(t => t.organizaciones)
                .HasForeignKey(o => o.Tipoactividad_id);
            modelBuilder.Entity<Organizacion>()
                .HasOne(o => o.lineaprod)
                .WithMany(l => l.organizaciones)
                .HasForeignKey(o => o.Lineaprod_id);
            modelBuilder.Entity<Organizacion>()
                .HasOne(o => o.tipoapoyo)
                .WithMany(t => t.organizaciones)
                .HasForeignKey(o => o.Tipoapoyo_id);

            // Relaciones de los proyectos.
            modelBuilder.Entity<Proyecto>()
                .HasOne(p => p.tipoproyecto)
                .WithMany(t => t.proyectos)
                .HasForeignKey(p => p.Tipoid);
            modelBuilder.Entity<Proyecto>()
                .HasMany(p => p.actividades)
                .WithMany(a => a.proyectos)
                .UsingEntity<Dictionary<string, object>>(
        "ActividadProyecto",
        j => j.HasOne<Actividad>().WithMany().HasForeignKey("Actividades_Id").OnDelete(DeleteBehavior.Cascade),
        j => j.HasOne<Proyecto>().WithMany().HasForeignKey("Proyectos_Id").OnDelete(DeleteBehavior.Cascade)
    );

            // Relaciones de los sectores a los que pertenecen los beneficiarios.
            modelBuilder.Entity<Sector>()
                .HasMany(s => s.beneficiarios)
                .WithOne(b => b.sector);

            // Relaciones de los tipos de actividad.
            modelBuilder.Entity<Tipoactividad>()
                .HasMany(t => t.organizaciones)
                .WithOne(o => o.tipoactividad);

            // Relaciones de los tipos de apoyo.
            modelBuilder.Entity<Tipoapoyo>()
                .HasMany(t => t.organizaciones)
                .WithOne(o => o.tipoapoyo);

            // Relaciones de los tipos de beneficiario.
            modelBuilder.Entity<Tipobene>()
                .HasMany(t => t.beneficiarios)
                .WithOne(b => b.tipobene);

            // Relaciones de los tipos de identificación.
            modelBuilder.Entity<Tipoiden>()
                .HasMany(t => t.beneficiarios)
                .WithOne(b => b.tipoiden);

            // Relaciones de los tipos de organización.
            modelBuilder.Entity<Tipoorg>()
                .HasMany(t => t.organizaciones)
                .WithOne(o => o.tipoorg);

            // Relaciones de los tipos de proyecto.
            modelBuilder.Entity<Tipoproyecto>()
                .HasMany(t => t.proyectos)
                .WithOne(p => p.tipoproyecto);

            base.OnModelCreating(modelBuilder);
        }
    }
}
