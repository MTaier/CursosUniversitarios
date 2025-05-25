using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursosUniversitarios.Shared.Data.Migrations
{
    /// <inheritdoc />
    public partial class SubjectDataEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Subject", new[] { "Name", "Credits", "Semester", "CourseId" },
                new object[,]
                {
                    { "Programação Orientada a Objetos", 4, 1, 1 },
                    { "Banco de Dados", 4, 2, 1 },
                    { "Engenharia de Software", 4, 3, 1 },
                    { "Sistemas Distribuídos", 4, 4, 1 },
                    { "Redes de Computadores", 4, 5, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Subject");
        }
    }
}
