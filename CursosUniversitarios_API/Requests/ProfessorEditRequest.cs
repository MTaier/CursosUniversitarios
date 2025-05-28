namespace CursosUniversitarios_API.Requests
{
    public record ProfessorEditRequest (int id, string name, string email, string phoneNumber, List<int> courseIds);
}
