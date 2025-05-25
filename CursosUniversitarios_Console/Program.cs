using CursosUniversitarios_Console;
using CursosUniversitarios.Shared.Data.DB;

internal class Program
{
    private static void Main(string[] args)
    {

        var CourseDAL = new DAL<Course>();

        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("=========MENU=========\n");
            Console.WriteLine("1 - Registrar um novo curso");
            Console.WriteLine("2 - Registrar disciplina em um curso");
            Console.WriteLine("3 - Registrar professor em um curso");
            Console.WriteLine("4 - Mostrar todos os cursos");
            Console.WriteLine("5 - Mostrar disciplinas de um curso");
            Console.WriteLine("6 - Mostrar professores de um curso");
            Console.WriteLine("0 - Sair");

            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    RegisterCourse();
                    break;

                case 2:
                    RegisterSubject();
                    break;

                case 3:
                    RegisterProfessor();
                    break;

                case 4:
                    ShowCourses();
                    break;

                case 5:
                    ShowSubjects();
                    break;

                case 6:
                    ShowProfessors();
                    break;

                case 0:
                    exit = true;
                    Console.WriteLine("Encerrando programa.");
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }

        void RegisterCourse()
        {
            Console.Clear();
            Console.WriteLine("Registro de novo curso");
            Console.Write("Nome do curso: ");
            string name = Console.ReadLine().Trim();

            var existing = CourseDAL.ReadBy(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (existing != null)
            {
                Console.WriteLine($"O curso '{name}' já está registrado.");
            }
            else
            {
                Course c = new Course(name);
                CourseDAL.Create(c);
                Console.WriteLine($"Curso '{name}' registrado com sucesso!");
            }
            Console.ReadKey();
        }


        void RegisterSubject()
        {
            Console.Clear();
            Console.WriteLine("Adicionar disciplina a um curso");
            Console.Write("Nome do curso: ");
            string courseName = Console.ReadLine();

            var targetCourse = CourseDAL.ReadBy(c => c.Name.Equals(courseName, StringComparison.OrdinalIgnoreCase));
            if (targetCourse != null)
            {
                Console.Write("Nome da disciplina: ");
                string subjectName = Console.ReadLine();
                targetCourse.AddSubject(new Subject(subjectName));
                CourseDAL.Update(targetCourse);
                Console.WriteLine($"Disciplina '{subjectName}' adicionada ao curso '{courseName}'.");
            }
            else
            {
                Console.WriteLine("Curso não encontrado.");
            }
            Console.ReadKey();
        }


        void RegisterProfessor()
        {
            Console.Clear();
            Console.WriteLine("Registrar professor em um curso");
            Console.Write("Nome do curso: ");
            string courseName = Console.ReadLine();

            var targetCourse = CourseDAL.ReadBy(c => c.Name.Equals(courseName, StringComparison.OrdinalIgnoreCase));
            if (targetCourse != null)
            {
                Console.Write("Nome do professor: ");
                string professorName = Console.ReadLine();
                Professor newProfessor = new(professorName);

                targetCourse.AddProfessor(newProfessor);
                newProfessor.AddCourse(targetCourse);

                CourseDAL.Update(targetCourse);
                Console.WriteLine($"Professor '{professorName}' adicionado ao curso '{courseName}'.");
            }
            else
            {
                Console.WriteLine("Curso não encontrado.");
            }
            Console.ReadKey();
        }


        void ShowCourses()
        {
            Console.Clear();
            Console.WriteLine("Cursos disponíveis:\n");

            var courses = CourseDAL.Read();
            if (courses.Count() == 0)
            {
                Console.WriteLine("Nenhum curso cadastrado.");
            }
            else
            {
                foreach (var course in courses)
                {
                    Console.WriteLine($"- {course.Name}");
                }
            }

            Console.ReadKey();
        }


        void ShowSubjects()
        {
            Console.Clear();
            Console.WriteLine("Mostrar disciplinas de um curso");
            Console.Write("Nome do curso: ");
            string courseName = Console.ReadLine();

            var targetCourse = CourseDAL.ReadBy(c => c.Name.Equals(courseName, StringComparison.OrdinalIgnoreCase));
            if (targetCourse != null)
            {
                if (targetCourse.Subjects.Count == 0)
                {
                    Console.WriteLine("Nenhuma disciplina cadastrada.");
                }
                else
                {
                    Console.WriteLine($"Disciplinas do curso '{courseName}':");
                    foreach (var s in targetCourse.Subjects)
                    {
                        Console.WriteLine($"- {s.Name}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Curso não encontrado.");
            }
            Console.ReadKey();
        }


        void ShowProfessors()
        {
            Console.Clear();
            Console.WriteLine("Mostrar professores de um curso");
            Console.Write("Nome do curso: ");
            string courseName = Console.ReadLine();

            var targetCourse = CourseDAL.ReadBy(c => c.Name.Equals(courseName, StringComparison.OrdinalIgnoreCase));
            if (targetCourse != null)
            {
                if (targetCourse.Professors.Count == 0)
                {
                    Console.WriteLine("Nenhum professor cadastrado.");
                }
                else
                {
                    Console.WriteLine($"Professores do curso '{courseName}':");
                    foreach (var p in targetCourse.Professors)
                    {
                        Console.WriteLine($"- {p.Name}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Curso não encontrado.");
            }
            Console.ReadKey();
        }


    }

}