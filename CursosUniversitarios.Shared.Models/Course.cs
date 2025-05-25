using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursosUniversitarios_Console
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalHours { get; set; }
        // 1:N - Um curso possui várias disciplinas
        public virtual ICollection<Subject> Subjects { get; set; } = new List<Subject>();
        // N:N - Um curso possui vários professores
        public virtual ICollection<Professor> Professors { get; set; } = new List<Professor>();

        public Course(string name, int totalHours)
        {
            Name = name;
            TotalHours = totalHours;
        }

        public Course(string name)
        {
            Name = name;
        }

        public void AddSubject(Subject subject)
        {
            Subjects.Add(subject);
        }

        public void AddProfessor(Professor professor)
        {
            bool exists = false;
            foreach (Professor p in Professors)
            {
                if (p.Name.Equals(professor.Name, StringComparison.OrdinalIgnoreCase))
                {
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                Professors.Add(professor);
            }
        }

        public override string ToString()
        {
            return $"Curso: {Name} (ID: {Id})";
        }

    }
}