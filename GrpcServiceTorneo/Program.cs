using Datos;
using GrpcServiceTorneo.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using static System.Net.WebRequestMethods;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

builder.Services.AddCors(opciones => {
    opciones.AddPolicy("cors", policy =>
    {
        policy.AllowAnyMethod()
              .AllowAnyHeader()
              .AllowAnyOrigin()
              .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
    }
    );
});

builder.Services.AddDbContext<TorneoBDContext>(opciones =>
{
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection"));
});

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

app.UseCors("cors");
app.UseGrpcWeb();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
app.MapGrpcService<JugadorService>().EnableGrpcWeb();
app.MapGrpcService<EquipoService>().EnableGrpcWeb();
app.MapGrpcService<ImagenService>().EnableGrpcWeb();

app.MapGrpcReflectionService();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
