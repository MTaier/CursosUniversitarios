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
                var prof = dal.ReadBy(p => p.Id == id);

                if (prof is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(EntityToResponse(prof));
            });

            groupBuilder.MapGet("{id}/Course", ([FromServices] DAL<Professor> dal, int id) =>
            {
                var prof = dal.ReadBy(p => p.Id == id);

                if (prof is null)
                {
                    return Results.NotFound();
                }

                var response = new
                {
                    professor = prof.Name,
                    cursos = prof.Courses.Select(c => c.Name).ToList()
                };

                return Results.Ok(response);
            });

            groupBuilder.MapPost("", ([FromServices] DAL<Professor> dal, [FromServices] DAL<Course> courseDal, [FromBody] ProfessorRequest professor) =>
            {
                var professorEntity = new Professor(professor.name, professor.email, professor.phoneNumber)
                {
                    Courses = professor.courses is not null
                        ? CourseRequestConvert(professor.courses, courseDal)
                        : new List<Course>()
                };

                dal.Create(professorEntity);
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

        private static List<Course> CourseRequestConvert(ICollection<CourseReferenceRequest> courseRequests, DAL<Course> courseDal)
        {
            var existingCourses = courseDal.Read().ToList();
            var courseList = new List<Course>();

            foreach (var courseReq in courseRequests)
            {
                var existing = existingCourses.FirstOrDefault(c => c.Name.ToUpper() == courseReq.name.ToUpper());

                if (existing is not null)
                {
                    courseList.Add(existing);
                }
                else
                {
                    courseList.Add(new Course(courseReq.name, 0));
                }
            }

            return courseList;
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
