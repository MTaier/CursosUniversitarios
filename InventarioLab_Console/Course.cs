using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioLab_Console
{
    internal class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // 1:N - Um curso possui várias disciplinas
        public List<Subject> Subjects { get; set; } = new();
        // N:N - Um curso possui vários professores
        public List<Professor> Professors { get; set; } = new();

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
