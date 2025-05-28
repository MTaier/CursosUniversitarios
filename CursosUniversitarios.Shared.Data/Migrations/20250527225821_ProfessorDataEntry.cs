using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CursosUniversitarios.Shared.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProfessorDataEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Professor", new[] { "Name", "Email", "PhoneNumber" },
                new object[,]
                {
                    { "Dr. Ana Silva", "ana.silva@email.com", "11999990001" },
                    { "Prof. Carlos Souza", "carlos.souza@email.com", "11999990002" },
                    { "Dra. Beatriz Lima", "beatriz.lima@email.com", "11999990003" },
                    { "Prof. João Pereira", "joao.pereira@email.com", "11999990004" },
                    { "Dra. Maria Oliveira", "maria.oliveira@email.com", "11999990005" }
                });

            migrationBuilder.InsertData("CourseProfessor", new[] { "CoursesId", "ProfessorsId" },
                new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 3 },
                    { 3, 4 },
                    { 3, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM CourseProfessor");
            migrationBuilder.Sql("DELETE FROM Professor");
        }
    }
}
