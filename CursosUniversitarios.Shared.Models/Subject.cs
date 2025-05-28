using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursosUniversitarios_Console
{
    public class Subject
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public int Semester { get; set; }

        // 1:N - Referência ao curso ao qual pertence
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        public Subject(string name, int credits, int semester, int courseId)
        {
            Name = name;
            Credits = credits;
            Semester = semester;
            CourseId = courseId;
        }

        public override string ToString()
        {
            return $"Disciplina: {Name} (ID: {Id})";
        }


    }
}
