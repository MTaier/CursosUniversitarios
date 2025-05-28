using CursosUniversitarios.Shared.Data.DB;
using CursosUniversitarios_API.EndPoints;
using CursosUniversitarios_Console;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        builder.Services.AddDbContext<CursosUniversitariosContext>();
        builder.Services.AddTransient<DAL<Course>>();
        builder.Services.AddTransient<DAL<Subject>>();
        builder.Services.AddTransient<DAL<Professor>>();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();
        app.AddEndpointsCourse();
        app.AddEndpointsSubject();
        app.AddEndpointsProfessor();
        app.UseSwagger();
        app.UseSwaggerUI();
        
        app.Run();
    }
}