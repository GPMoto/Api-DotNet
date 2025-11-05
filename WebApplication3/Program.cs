using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System;
using System.Reflection;
using System.Text;
using WebApplication3.Data;
using WebApplication3.Repositories;
using WebApplication3.Service;

var builder = WebApplication.CreateBuilder(args);


var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

// Add services to the container.


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<CidadeRepository>();
builder.Services.AddScoped<EstadoRepository>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<EnderecoRepository>();
builder.Services.AddScoped<ContatoRepository>();
builder.Services.AddScoped<FilialRepository>();
builder.Services.AddScoped<MotoRespository>();
builder.Services.AddScoped<PaisRepository>();
builder.Services.AddScoped<PerfilRepository>();
builder.Services.AddScoped<SecoesFilialRepository>();
builder.Services.AddScoped<TelefoneRepository>();
builder.Services.AddScoped<TipoMotoRepository>();
builder.Services.AddScoped<TipoSecaoFilialRepository>();
builder.Services.AddScoped<UwbRepository>();


builder.Services.AddScoped<CidadeService>();
builder.Services.AddScoped<EstadoService>();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<EnderecoService>();
builder.Services.AddScoped<ContatoService>();
builder.Services.AddScoped<FilialService>();
builder.Services.AddScoped<MotoService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<PaisService>();
builder.Services.AddScoped<PerfilService>();
builder.Services.AddScoped<SecoesFilialService>();
builder.Services.AddScoped<TelefoneService>();
builder.Services.AddScoped<TipoMotoService>();
builder.Services.AddScoped<TIpoSecaoFilialService>();
builder.Services.AddScoped<UwbService>();
builder.Services.AddScoped<MotoMlService>();


builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter());
});

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource
        .AddService("GpsMottuAPI", "1.0.0")
        .AddAttributes(new Dictionary<string, object>
        {
            ["deployment.environment"] = builder.Environment.EnvironmentName
        }))
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation(options =>
        {
            options.RecordException = true;
            options.Filter = (httpContext) =>
            {
                return !httpContext.Request.Path.StartsWithSegments("/swagger");
            };
        })
        .AddHttpClientInstrumentation()
        .AddConsoleExporter())
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .AddConsoleExporter());


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            //Valida se a chave est� 100% ok
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),

            //valida quem emitiu o token de issuer
            ValidateIssuer = true,
            ValidIssuer = jwtIssuer,

            //Valida quem o token foi destinado.
            ValidateAudience = true,
            ValidAudience = jwtAudience,

            //Valida��o de tempo de expira��o do token
            ValidateLifetime = true,
            //tolerancia (Controlada pelo servi�o)
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(c =>
{

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Insira o token JWT desta forma: Bearer {seu Token}"
    });

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Gps Mottu API",
        Version = "v3",
        Description = "Api desenvolvida para o challenge da mottu, ela tem como intuito ajudar na organização dos patios da mottu",
        
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

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
            new string[] { }
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
