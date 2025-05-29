using CursosUniversitarios.Shared.Data.DB;
using CursosUniversitarios_API.Requests;
using CursosUniversitarios_API.Responses;
using CursosUniversitarios_Console;
using Microsoft.AspNetCore.Mvc;

namespace CursosUniversitarios_API.EndPoints
{
    public static class CourseExtension
    {
        public static void AddEndpointsCourse(this WebApplication app)
        {
            var groupBuilder = app.MapGroup("Course")
                .WithTags("Cursos")
                .RequireAuthorization();

            groupBuilder.MapGet("", ([FromServices] DAL<Course> dal) =>
            {
                var courseList = dal.Read();

                if (courseList is null)
                {
                    return Results.NotFound();
                }

                var courseResponseList = EntityListToResponseList(courseList);
                return Results.Ok(courseResponseList);
            });

            groupBuilder.MapGet("/{id}", (int id, [FromServices] DAL<Course> dal) =>
            {
                var crs = dal.ReadBy(c => c.Id == id);

                if (crs is null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(EntityToDetailedResponse(crs));
            });

            groupBuilder.MapPost("", ([FromServices] DAL<Course> dal, [FromServices] DAL<Professor> profDal, [FromBody] CourseRequest crs) =>
            {
                dal.Create(new Course(crs.name, crs.totalHours)
                {
                    Professors = crs.professors is not null ?
                    ProfessorRequestConvert(crs.professors, profDal) : new List<Professor>()
                }
                );
                return Results.NoContent();
            });

            groupBuilder.MapDelete("/{id}", ([FromServices] DAL<Course> dal, int id) =>
            {
                var crs = dal.ReadBy(c => c.Id == id);

                if (crs is null)
                {
                    return Results.NotFound();
                }

                dal.Delete(crs);
                return Results.NoContent();
            });

            groupBuilder.MapPut("", ([FromServices] DAL<Course> dal, [FromBody] CourseEditRequest crs) =>
            {
                var crsToEdit = dal.ReadBy(c => c.Id == crs.id);

                if (crsToEdit is null)
                {
                    return Results.NotFound();
                }

                crsToEdit.Name = crs.name;
                crsToEdit.TotalHours = crs.totalHours;

                dal.Update(crsToEdit);
                return Results.Created();
            });
        }

        private static object? EntityToDetailedResponse(Course entity)
        {
            return new CourseResponseById(
                entity.Name,
                entity.TotalHours.ToString(),
                entity.Subjects.Select(s => new SubjectRequest(s.Name, s.Credits, s.Semester, s.CourseId)).ToList(),
                entity.Professors.Select(p => new ProfessorRequest(p.Name, p.Email, p.PhoneNumber)).ToList()
            );
        }

        private static List<Professor> ProfessorRequestConvert(ICollection<ProfessorRequest> professorsList, DAL<Professor> profDal)
        {
            var profList = new List<Professor>();

            foreach (var item in professorsList)
            {
                var profBuscado = profDal.ReadBy(p => p.Email.ToLower() == item.email.ToLower());

                if (profBuscado is not null)
                {
                    profList.Add(profBuscado);
                }
                else
                {
                    var prof = new Professor(item.name, item.email, item.phoneNumber);
                    profList.Add(prof);
                }
            }

            return profList;
        }

        private static Professor RequestToEntity(ProfessorRequest p)
        {
            return new Professor(p.name, p.email, p.phoneNumber);
        }

        private static ICollection<CourseResponse> EntityListToResponseList(IEnumerable<Course> entities)
        {
            return entities.Select(e => EntityToResponse(e)).ToList();
        }

        private static CourseResponse EntityToResponse(Course entity)
        {
            return new CourseResponse(entity.Id, entity.Name, entity.TotalHours);
        }
    }
}
