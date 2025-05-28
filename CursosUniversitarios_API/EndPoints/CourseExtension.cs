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
            app.MapGet("/Course", ([FromServices] DAL<Course> dal) =>
            {
                var courseList = dal.Read();

                if (courseList is null)
                {
                    return Results.NotFound();
                }

                var courseResponseList = EntityListToResponseList(courseList);
                return Results.Ok(courseResponseList);
            });

            app.MapPost("/Course", ([FromServices] DAL<Course> dal, [FromBody] CourseRequest crs) =>
            {
                dal.Create(new Course(crs.Name, crs.TotalHours));
                return Results.NoContent();
            });

            app.MapDelete("/Course/{id}", ([FromServices] DAL<Course> dal, int id) =>
            {
                var crs = dal.ReadBy(c => c.Id == id);

                if (crs is null)
                {
                    return Results.NotFound();
                }

                dal.Delete(crs);
                return Results.NoContent();
            });

            app.MapPut("/Course", ([FromServices] DAL<Course> dal, [FromBody] CourseEditRequest crs) =>
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
