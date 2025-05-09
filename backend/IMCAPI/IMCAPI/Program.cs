using IMCAPI.Infrastructure.Persistence.Repositories;
using IMCAPI.Core.Interfaces.Repositories;
using IMCAPI.Core.Interfaces.Services;
using IMCAPI.Application.Services;
using IMCAPI.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Agregar repositorios al contenedor.
builder.Services.AddScoped<IActividadRepository, ActividadRepository>();
builder.Services.AddScoped<IBeneficiarioRepository, BeneficiarioRepository>();
builder.Services.AddScoped<IEdadRepository, EdadRepository>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<IGrupoetnicoRepository, GrupoetnicoRepository>();
builder.Services.AddScoped<ILineaprodRepository, LineaprodRepository>();
builder.Services.AddScoped<IMunicipioRepository, MunicipioRepository>();
builder.Services.AddScoped<ILugarRepository, LugarRepository>();
builder.Services.AddScoped<IMunicipioRepository, MunicipioRepository>();
builder.Services.AddScoped<IOrganizacionRepository, OrganizacionRepository>();
builder.Services.AddScoped<IProyectoRepository, ProyectoRepository>();
builder.Services.AddScoped<ISectorRepository, SectorRepository>();
builder.Services.AddScoped<ITipoactividadRepository, TipoactividadRepository>();
builder.Services.AddScoped<ITipoapoyoRepository, TipoapoyoRepository>();
builder.Services.AddScoped<ITipobeneRepository, TipobeneRepository>();
builder.Services.AddScoped<ITipoidenRepository, TipoidenRepository>();
builder.Services.AddScoped<ITipoorgRepository, TipoorgRepository>();
builder.Services.AddScoped<ITipoProyectoRepository, TipoProyectoRepository>();

// Agregar servicios al contenedor.
builder.Services.AddScoped<IActividadService, ActividadService>();
builder.Services.AddScoped<IBeneficiarioService, BeneficiarioService>();
builder.Services.AddScoped<IEdadService, EdadService>();
builder.Services.AddScoped<IGeneroService, GeneroService>();
builder.Services.AddScoped<IGrupoetnicoService, GrupoetnicoService>();
builder.Services.AddScoped<ILineaprodService, LineaprodService>();
builder.Services.AddScoped<ILugarService, LugarService>();
builder.Services.AddScoped<IMunicipioService, MunicipioService>();
builder.Services.AddScoped<IOrganizacionService, OrganizacionService>();
builder.Services.AddScoped<IProyectoService, ProyectoService>();
builder.Services.AddScoped<ISectorService, SectorService>();
builder.Services.AddScoped<ITipoactividadService, TipoactividadService>();
builder.Services.AddScoped<ITipoapoyoService, TipoapoyoService>();
builder.Services.AddScoped<ITipobeneService, TipobeneService>();
builder.Services.AddScoped<ITipoidenService, TipoidenService>();
builder.Services.AddScoped<ITipoorgService, TipoorgService>();
builder.Services.AddScoped<ITipoProyectoService, TipoProyectoService>();
builder.Services.AddScoped<ITipoProyectoService, TipoProyectoService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddDbContext<DbContextIMC>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("IMCConnection"),
        new MySqlServerVersion(new Version(11, 5, 2)) // Versión de MariaDB (sujeto a cambios).
    )
);

// Documentación de Swagger.
builder.Services.AddSwaggerGen(c =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    // Se requiere un token para hacer los llamados.
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Ingrese el token de autorizacion"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
