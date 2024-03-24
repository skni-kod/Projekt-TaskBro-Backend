using System.Security.Authentication;
using Api.Extensions;
using Application;
using Infrastructure;
using Microsoft.OpenApi.Models;
using Presentation;


var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "Wprowadź token JWT w polu 'Bearer {token}'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
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
                new string[] {}
            }
        });
    });

}

if (builder.Environment.IsDevelopment())
{
    builder.WebHost.ConfigureKestrel(serverOptions =>
    {
        serverOptions.ConfigureHttpsDefaults(listenOptions =>
        {
            listenOptions.SslProtocols = SslProtocols.Tls12;
        });
    });
}
// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure()
    .AddDatabaseConntection(builder.Configuration);

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.AddGlobalErrorHandler();
    
    app.Run();
}