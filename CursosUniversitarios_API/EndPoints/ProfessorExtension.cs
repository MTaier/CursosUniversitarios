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
            app.MapGet("/Professor", ([FromServices] DAL<Professor> dal) =>
            {
                var ProfessorList = dal.Read();

                if (ProfessorList is null)
                {
                    return Results.NotFound();
                }

                var ProfessorResponseList = EntityListToResponseList(ProfessorList);
                return Results.Ok(ProfessorResponseList);
            });

            app.MapPost("/Professor", ([FromServices] DAL<Professor> dal, [FromServices] DAL<Course> courseDal, [FromBody] ProfessorRequest professor) =>
            {
                var newProfessor = new Professor(professor.name, professor.email, professor.phoneNumber);
                var courses = courseDal.Read().Where(c => professor.courseIds.Contains(c.Id)).ToList();

                newProfessor.Courses = courses;

                dal.Create(newProfessor);

                return Results.NoContent();
            });

            app.MapDelete("/Professor/{id}", ([FromServices] DAL<Professor> dal, int id) =>
            {
                var professor = dal.ReadBy(c => c.Id == id);

                if (professor is null)
                {
                    return Results.NotFound();
                }

                dal.Delete(professor);
                return Results.NoContent();
            });

            app.MapPut("/Professor", ([FromServices] DAL<Professor> dal, [FromServices] DAL < Course > dalCourse, [FromBody] ProfessorEditRequest professor) =>
            {
                var professorToEdit = dal.ReadBy(c => c.Id == professor.id);

                if (professorToEdit is null)
                {
                    return Results.NotFound();
                }

                professorToEdit.Name = professor.name;
                professorToEdit.Email = professor.email;
                professorToEdit.PhoneNumber = professor.phoneNumber;

                var courses = dalCourse.Read().Where(c => professor.courseIds.Contains(c.Id)).ToList();
                professorToEdit.Courses = courses;

                dal.Update(professorToEdit);
                return Results.Created();
            });
        }

        private static ICollection<ProfessorResponse> EntityListToResponseList(IEnumerable<Professor> entities)
        {
            return entities.Select(e => EntityToResponse(e)).ToList();
        }

        private static ProfessorResponse EntityToResponse(Professor entity)
        {
            var courses = entity.Courses.Select(c => new CourseResponse(c.Id, c.Name, c.TotalHours)).ToList();

            return new ProfessorResponse(
                entity.Id,
                entity.Name,
                entity.Email,
                entity.PhoneNumber,
                courses);
        }
    }
}
