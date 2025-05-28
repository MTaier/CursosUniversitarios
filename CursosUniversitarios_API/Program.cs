using CursosUniversitarios.Shared.Data.DB;
using CursosUniversitarios.Shared.Data.Models;
using CursosUniversitarios_API.EndPoints;
using CursosUniversitarios_Console;
using Microsoft.AspNetCore.Identity;
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
        builder.Services.AddIdentityApiEndpoints<AccessUser>().AddEntityFrameworkStores<CursosUniversitariosContext>();
        builder.Services.AddAuthorization();

        var app = builder.Build();

        app.UseAuthorization();

        app.MapGroup("auth").MapIdentityApi<AccessUser>().WithTags("Authorization");

        app.MapPost("auth/logout", async ([FromServices] SignInManager<AccessUser> manager) =>
        {
            await manager.SignOutAsync();
            return Results.Ok();
        }).RequireAuthorization().WithTags("Authorization");

        app.AddEndpointsCourse();
        app.AddEndpointsSubject();
        app.AddEndpointsProfessor();

        app.UseSwagger();
        app.UseSwaggerUI();
        
        app.Run();
    }
}