namespace CursosUniversitarios_API.Requests
{
    public record ProfessorRequest (string name, string email, string phoneNumber, ICollection<CourseReferenceRequest> courses = null);
}
