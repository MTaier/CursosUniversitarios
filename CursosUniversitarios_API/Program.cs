using CursosUniversitarios.Shared.Data.DB;
using CursosUniversitarios_Console;
using System.Text.Json.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        var app = builder.Build();

        app.MapGet("/", () =>
        {
            var dal = new DAL<Course>();
            return dal.Read();
        });

        app.Run();
    }
}