using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursosUniversitarios_Console
{
    public class Professor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }   
        public string PhoneNumber { get; set; }
        // N:N - Um professor pode estar em vários cursos
        public virtual List<Course> Courses { get; set; } = new();

        public Professor(string name, string email, string phoneNumber)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
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
