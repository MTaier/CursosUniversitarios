using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursosUniversitarios.Shared.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDataEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                "Course",
                new[] { "Name", "TotalHours" },
                new object[,]
                {
                    { "Engenharia de Software", 3600 },
                    { "Sistemas de Informação", 3200 },
                    { "Análise e Desenvolvimento", 3000 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Course");
        }
    }
}
