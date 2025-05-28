namespace CursosUniversitarios_API.Requests
{
    public record SubjectEditRequest (int id, string name, int credits, int semester, int courseId);
}
