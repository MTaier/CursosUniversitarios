using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioLab_Console
{
    internal class Professor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // N:N - Um professor pode estar em vários cursos
        public List<Course> Courses { get; set; } = new();

        public Professor(string name)
        {
            Name = name;
        }

        public void AddCourse(Course course)
        {
            bool exists = false;
            foreach (Course c in Courses)
            {
                if (c.Name.Equals(course.Name, StringComparison.OrdinalIgnoreCase))
                {
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                Courses.Add(course);
            }
        }

        public override string ToString()
        {
            return $"Professor: {Name} (ID: {Id})";
        }




    }
}
