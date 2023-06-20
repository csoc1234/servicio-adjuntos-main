using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using TFHKA.Adjuntos.listado.Application.Interfaz;
using TFHKA.Adjuntos.listado.Application.Principal;
using TFHKA.Adjuntos.listado.Domain.Core;
using TFHKA.Adjuntos.listado.Domain.Interfaz;
using TFHKA.Adjuntos.listado.Infraestructure.Datos;
using TFHKA.Adjuntos.listado.Infraestructure.Repo;
using TFHKA.Adjuntos.listado.Infraestruture.Interfaz;
using TFHKA.Adjuntos.listado.Transversal.Comun;
using TFHKA.Adjuntos.listado.Transversal.Mapeo;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
      c =>
      {
          c.SwaggerDoc("v1", new OpenApiInfo
          {
              Version = $"v{Assembly.GetExecutingAssembly().GetName().Version.ToString()}",
              Title = "API Adjuntos Listado" + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
              Description = "Web API Adjuntos listado.",
          });
          string? xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
          string? xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
          c.IncludeXmlComments(xmlPath);



          c.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme
          {
              Description = "Authorization by API key.",
              In = ParameterLocation.Header,
              Type = SecuritySchemeType.ApiKey,
              Name = "Authorization"
          });

          c.AddSecurityRequirement(new OpenApiSecurityRequirement
{{       new OpenApiSecurityScheme
    {    Reference = new OpenApiReference {
        Type = ReferenceType.SecurityScheme,
        Id = "Authorization" }},
        new List<string>()
      }});
      });

//Disable Validation in Request
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

//Auth Bearer Jwt 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddCookie()
  .AddJwtBearer(jwtBearerOptions =>
  {
      jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
      {
          ValidateActor = false,
          ValidateAudience = false,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          ValidIssuer = builder.Configuration["Autenticacion:Token:Issuer"],
          ValidAudience = builder.Configuration["Autenticacion:Token:Audience"],
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Autenticacion:Token:Key"]))
      };
  });

#region Inyección de dependencias. Arquitectura de aplicaciones empresariales por capas

builder.Services.AddAutoMapper(typeof(PerfilMapeo));
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<IFabricaConexion, FabricaConexionSqlServer>();
builder.Services.AddAutoMapper(typeof(PerfilMapeo));
builder.Services.AddScoped<IAdjuntosListadoApplication, AdjuntosListadoApplication>();
builder.Services.AddScoped<IAdjuntosListadoDomainInterfaz, AdjuntosListadoDomain>();
builder.Services.AddScoped<IAdjuntosListadoInfraInterfaz, AdjuntosListadoRepositorio>();

#endregion Inyección de dependencias. Arquitectura de aplicaciones empresariales por capas

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
    options.DocumentTitle = "API Adjuntos";
});


app.UseAuthorization();

app.MapControllers();

app.Run();
