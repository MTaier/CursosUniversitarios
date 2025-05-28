namespace CursosUniversitarios_API.Requests
{
    public record CourseRequest (
        string name, 
        int totalHours, 
        ICollection<ProfessorRequest> professors = null
    );
}
