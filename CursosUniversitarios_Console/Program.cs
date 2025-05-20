using InventarioLab_Console;

internal class Program
{
    public static Dictionary<string, Course> CourseList = new();
    private static void Main(string[] args)
    {
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

    }

    private static void ShowProfessors()
    {
        Console.Clear();
        Console.WriteLine("Mostrar professores de um curso");

        Console.Write("Nome do curso: ");
        string courseName = Console.ReadLine();

        if (CourseList.ContainsKey(courseName))
        {
            var professors = CourseList[courseName].Professors;

            if (professors.Count == 0)
            {
                Console.WriteLine("Nenhum professor cadastrado para este curso.");
            }
            else
            {
                Console.WriteLine($"Professores do curso '{courseName}':");
                foreach (var p in professors)
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

    private static void ShowSubjects()
    {
        Console.Clear();
        Console.WriteLine("Mostrar disciplinas de um curso");

        Console.Write("Nome do curso: ");
        string courseName = Console.ReadLine();

        if (CourseList.ContainsKey(courseName))
        {
            var subjects = CourseList[courseName].Subjects;

            if (subjects.Count == 0)
            {
                Console.WriteLine("Nenhuma disciplina cadastrada para este curso.");
            }
            else
            {
                Console.WriteLine($"Disciplinas do curso '{courseName}':");
                foreach (var s in subjects)
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

    private static void ShowCourses()
    {
        Console.Clear();
        Console.WriteLine("Cursos disponíveis:\n");

        if (CourseList.Count == 0)
        {
            Console.WriteLine("Nenhum curso cadastrado.");
        }
        else
        {
            foreach (var course in CourseList.Values)
            {
                Console.WriteLine($"- {course.Name}");
            }
        }

        Console.ReadKey();
    }

    private static void RegisterProfessor()
    {
        Console.Clear();
        Console.WriteLine("Adicionar disciplina a um curso");
        Console.Write("Nome do curso: ");
        string courseName = Console.ReadLine();

        if (CourseList.ContainsKey(courseName))
        {
            Console.Write("Nome do professor: ");
            string professorName = Console.ReadLine();
            Professor newProfessor = new(professorName);
            CourseList[courseName].AddProfessor(newProfessor);
            newProfessor.AddCourse(CourseList[courseName]);
            Console.WriteLine($"Disciplina '{professorName}' adicionada ao curso '{courseName}'.");
        }
        else
        {
            Console.WriteLine("Curso não encontrado.");
        }
        Console.ReadKey();
    }

    private static void RegisterSubject()
    {
        Console.Clear();
        Console.WriteLine("Adicionar disciplina a um curso");
        Console.Write("Nome do curso: ");
        string courseName = Console.ReadLine();

        if (CourseList.ContainsKey(courseName))
        {
            Console.Write("Nome da disciplina: ");
            string subjectName = Console.ReadLine();
            Subject newSubject = new Subject(subjectName);
            CourseList[courseName].AddSubject(newSubject);
            Console.WriteLine($"Disciplina '{subjectName}' adicionada ao curso '{courseName}'.");
        }
        else
        {
            Console.WriteLine("Curso não encontrado.");
        }
        Console.ReadKey();
    }

    private static void RegisterCourse()
    {
        Console.Clear();
        Console.WriteLine("Registro de novo curso");
        Console.Write("Nome do curso: ");
        string name = Console.ReadLine().Trim().ToLower();
        if (CourseList.ContainsKey(name))
        {
            Console.WriteLine($"O curso '{name}' já está registrado.");
        }
        else
        {
            Course c = new Course(name);
            CourseList.Add(name, c);
            Console.WriteLine($"Curso '{name}' registrado com sucesso!");
        }
    }
}

    
