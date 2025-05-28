using CursosUniversitarios.Shared.Data.DB;
using CursosUniversitarios_API.Requests;
using CursosUniversitarios_API.Responses;
using CursosUniversitarios_Console;
using Microsoft.AspNetCore.Mvc;

namespace CursosUniversitarios_API.EndPoints
{
    public static class ProfessorExtension
    {
        public static void AddEndpointsProfessor(this WebApplication app)
        {
            var groupBuilder = app.MapGroup("Professor")
                .WithTags("Professores")
                .RequireAuthorization();

            groupBuilder.MapGet("", ([FromServices] DAL<Professor> dal) =>
            {
                var professorList = dal.Read();

                if (professorList is null)
                {
                    return Results.NotFound();
                }

                var professorResponseList = EntityListToResponseList(professorList);
                return Results.Ok(professorResponseList);
            });

            groupBuilder.MapGet("/{id}", (int id, [FromServices] DAL<Professor> dal) =>
            {
                var crs = dal.ReadBy(c => c.Id == id);

                if (crs is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(EntityToResponse(crs));
            });

            groupBuilder.MapPost("", ([FromServices] DAL<Professor> dal, [FromBody] ProfessorRequest professor) =>
            {
                dal.Create(RequestoToEntity(professor));
                return Results.NoContent();
            });

            groupBuilder.MapDelete("/{id}", ([FromServices] DAL<Professor> dal, int id) =>
            {
                var professor = dal.ReadBy(c => c.Id == id);

                if (professor is null)
                {
                    return Results.NotFound();
                }

                dal.Delete(professor);
                return Results.NoContent();
            });

            groupBuilder.MapPut("", ([FromServices] DAL<Professor> dal, [FromBody] ProfessorEditRequest professor) =>
            {
                var professorToEdit = dal.ReadBy(c => c.Id == professor.id);

                if (professorToEdit is null)
                {
                    return Results.NotFound();
                }

                professorToEdit.Name = professor.name;
                professorToEdit.Email = professor.email;
                professorToEdit.PhoneNumber = professor.phoneNumber;

                dal.Update(professorToEdit);
                return Results.Created();
            });
        }

        private static Professor RequestoToEntity(ProfessorRequest professor)
        {
            return new Professor(professor.name, professor.email, professor.phoneNumber);
        }

        private static ICollection<ProfessorResponse> EntityListToResponseList(IEnumerable<Professor> entities)
        {
            return entities.Select(e => EntityToResponse(e)).ToList();
        }

        private static ProfessorResponse EntityToResponse(Professor entity)
        {
            return new ProfessorResponse(entity.Id, entity.Name, entity.Email, entity.PhoneNumber);
        }
    }
}
