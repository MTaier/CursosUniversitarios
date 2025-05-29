using CursosUniversitarios_API.Requests;
using CursosUniversitarios_Console;

namespace CursosUniversitarios_API.Responses
{
    public record CourseResponseById (string name, string totalHours, List<SubjectRequest> subjects, List<ProfessorRequest> professors);
}
