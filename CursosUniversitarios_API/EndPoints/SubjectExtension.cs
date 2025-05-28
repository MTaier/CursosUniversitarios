using CursosUniversitarios.Shared.Data.DB;
using CursosUniversitarios_API.Requests;
using CursosUniversitarios_API.Responses;
using CursosUniversitarios_Console;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace CursosUniversitarios_API.EndPoints
{
    public static class SubjectExtension
    {
        public static void AddEndpointsSubject(this WebApplication app)
        {
            app.MapGet("/Subject", ([FromServices] DAL<Subject> dal) =>
            {
                var subjectList = dal.Read();

                if (subjectList is null)
                {
                    return Results.NotFound();
                }

                var subjectResponseList = EntityListToResponseList(subjectList);
                return Results.Ok(subjectResponseList);
            });

            app.MapPost("/Subject", ([FromServices] DAL<Subject> dal, [FromBody] SubjectRequest subject) =>
            {
                dal.Create(new Subject(subject.name, subject.credits, subject.semester, subject.courseId));
                return Results.NoContent();
            });

            app.MapDelete("/Subject/{id}", ([FromServices] DAL<Subject> dal, int id) =>
            {
                var sbjct = dal.ReadBy(s => s.Id == id);

                if (sbjct is null)
                {
                    return Results.NotFound();
                }

                dal.Delete(sbjct);
                return Results.NoContent();
            });

            app.MapPut("/Subject", ([FromServices] DAL<Subject> dal, [FromBody] SubjectEditRequest subject) =>
            {
                var subjectToEdit = dal.ReadBy(s => s.Id == subject.id);

                if (subjectToEdit is null)
                {
                    return Results.NotFound();
                }

                subjectToEdit.Name = subject.name;
                subjectToEdit.Credits = subject.credits;
                subjectToEdit.Semester = subject.semester;
                subjectToEdit.CourseId = subject.courseId;

                dal.Update(subjectToEdit);
                return Results.Created();
            });
        }

        private static ICollection<SubjectResponse> EntityListToResponseList(IEnumerable<Subject> entities)
        {
            return entities.Select(e => EntityToResponse(e)).ToList();
        }

        private static SubjectResponse EntityToResponse(Subject entity)
        {
            return new SubjectResponse(
                entity.Id, 
                entity.Name, 
                entity.Course?.Id?? 0, 
                entity.Course?.Name?? "No course");
        }
    }
}
