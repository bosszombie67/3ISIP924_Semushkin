using System;
using System.Collections.Generic;
using System.Text;
namespace ISIP924_Semushkin
{
    interface IInformation
    {
        void PrintInfo();
    }
    abstract class Person
    {
        private string name;
        private int age;
        private string contact;

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Имя не может быть пустым.");
                }
                name = value.Trim();
            }
        }
        public int Age
        {
            get { return age; }
            set
            {
                if (value < 1 || value > 120)
                {
                    throw new ArgumentException("Возраст должен быть от 1 до 120.");
                }
                age = value;
            }
        }
        public string Contact
        {
            get { return contact; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Контакт не может быть пустым.");
                }
                contact = value.Trim();
            }
        }
        protected Person(string name, int age, string contact)
        {
            Name = name;
            Age = age;
            Contact = contact;
        }

        protected void PrintPersonInfo()
        {
            Console.WriteLine("Имя: " + Name);
            Console.WriteLine("Возраст: " + Age);
            Console.WriteLine("Контакт: " + Contact);
        }
    }
    class Student : Person, IInformation
    {
        public Student(string name, int age, string contact) : base(name, age, contact)
        {
        }

        public void PrintInfo()
        {
            Console.WriteLine("Студент:");
            PrintPersonInfo();
        }
    }
    class Teacher : Person, IInformation
    {
        public Teacher(string name, int age, string contact) : base(name, age, contact)
        {
        }
        public void PrintInfo()
        {
            Console.WriteLine("Преподаватель:");
            PrintPersonInfo();
        }
    }
    class Course
    {
        private string name;
        private List<Student> students = new List<Student>();
        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Название курса не может быть пустым.");
                }
                name = value.Trim();
            }
        }
        public Teacher Teacher { get; private set; }
        public Course(string name, Teacher teacher)
        {
            Name = name;
            if (teacher == null)
            {
                throw new ArgumentException("Нужно выбрать преподавателя.");
            }
            Teacher = teacher;
        }
        public void EnrollStudent(Student student)
        {
            if (student == null)
            {
                throw new ArgumentException("Нужно выбрать студента.");
            }
            if (students.Contains(student))
            {
                throw new ArgumentException("Студент уже записан на этот курс.");
            }
            students.Add(student);
        }
        public bool HasStudent(Student student)
        {
            return students.Contains(student);
        }
        public void PrintInfo()
        {
            Console.WriteLine("Курс: " + Name);
            Console.WriteLine("Преподаватель: " + Teacher.Name);
            Console.WriteLine("Количество студентов: " + students.Count);
        }
        public void PrintStudents()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("На курс пока никто не записан.");
                return;
            }
            foreach (Student student in students)
            {
                student.PrintInfo();
                Console.WriteLine();
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            List<Student> students = new List<Student>();
            List<Teacher> teachers = new List<Teacher>();
            List<Course> courses = new List<Course>();
            int choice = -1;
            while (choice != 0)
            {
                Console.WriteLine();
                Console.WriteLine("1 - Добавить студента");
                Console.WriteLine("2 - Добавить преподавателя");
                Console.WriteLine("3 - Создать курс и назначить преподавателя");
                Console.WriteLine("4 - Записать студента на курс");
                Console.WriteLine("5 - Все студенты");
                Console.WriteLine("6 - Все преподаватели");
                Console.WriteLine("7 - Все курсы");
                Console.WriteLine("8 - Информация о студенте и его курсах");
                Console.WriteLine("9 - Информация о преподавателе и его курсах");
                Console.WriteLine("10 - Информация о курсе и его студентах");
                Console.WriteLine("0 - Выход");
                Console.Write("Выберите действие: ");
                string input = Console.ReadLine();
                if (input == null)
                {
                    return;
                }
                if (!int.TryParse(input, out choice))
                {
                    Console.WriteLine("Введите целое число.");
                    choice = -1;
                    continue;
                }
                try
                {
                    switch (choice)
                    {
                        case 1:
                            {
                                Console.Write("Имя: ");
                                string name = Console.ReadLine();
                                if (name == null)
                                {
                                    return;
                                }
                                Console.Write("Возраст: ");
                                string ageInput = Console.ReadLine();
                                if (ageInput == null)
                                {
                                    return;
                                }
                                int age;
                                if (!int.TryParse(ageInput, out age))
                                {
                                    Console.WriteLine("Возраст нужно вводить целым числом.");
                                    break;
                                }
                                Console.Write("Контакт: ");
                                string contact = Console.ReadLine();
                                if (contact == null)
                                {
                                    return;
                                }
                                Student student = new Student(name, age, contact);
                                students.Add(student);
                                Console.WriteLine("Студент добавлен.");
                                break;
                            }
                        case 2:
                            {
                                Console.Write("Имя: ");
                                string name = Console.ReadLine();
                                if (name == null)
                                {
                                    return;
                                }
                                Console.Write("Возраст: ");
                                string ageInput = Console.ReadLine();
                                if (ageInput == null)
                                {
                                    return;
                                }
                                int age;
                                if (!int.TryParse(ageInput, out age))
                                {
                                    Console.WriteLine("Возраст нужно вводить целым числом.");
                                    break;
                                }
                                Console.Write("Контакт: ");
                                string contact = Console.ReadLine();
                                if (contact == null)
                                {
                                    return;
                                }
                                Teacher teacher = new Teacher(name, age, contact);
                                teachers.Add(teacher);
                                Console.WriteLine("Преподаватель добавлен.");
                                break;
                            }
                        case 3:
                            {
                                if (teachers.Count == 0)
                                {
                                    Console.WriteLine("Сначала добавьте преподавателя.");
                                    break;
                                }
                                Console.Write("Название курса: ");
                                string title = Console.ReadLine();
                                if (title == null)
                                {
                                    return;
                                }
                                for (int i = 0; i < teachers.Count; i++)
                                {
                                    Console.WriteLine((i + 1) + ". " + teachers[i].Name);
                                }
                                Console.Write("Номер преподавателя (0 - отмена): ");
                                input = Console.ReadLine();
                                if (input == null)
                                {
                                    return;
                                }
                                int teacherNumber;
                                if (!int.TryParse(input, out teacherNumber) || teacherNumber < 0 || teacherNumber > teachers.Count)
                                {
                                    Console.WriteLine("Неверный номер преподавателя.");
                                    break;
                                }
                                if (teacherNumber == 0)
                                {
                                    break;
                                }
                                Course course = new Course(title, teachers[teacherNumber - 1]);
                                courses.Add(course);
                                Console.WriteLine("Курс создан.");
                                break;
                            }
                        case 4:
                            {
                                if (students.Count == 0 || courses.Count == 0)
                                {
                                    Console.WriteLine("Сначала добавьте студента и создайте курс.");
                                    break;
                                }
                                for (int i = 0; i < students.Count; i++)
                                {
                                    Console.WriteLine((i + 1) + ". " + students[i].Name);
                                }
                                Console.Write("Номер студента (0 - отмена): ");
                                input = Console.ReadLine();
                                if (input == null)
                                {
                                    return;
                                }
                                int studentNumber;
                                if (!int.TryParse(input, out studentNumber) || studentNumber < 0 || studentNumber > students.Count)
                                {
                                    Console.WriteLine("Неверный номер студента.");
                                    break;
                                }
                                if (studentNumber == 0)
                                {
                                    break;
                                }
                                for (int i = 0; i < courses.Count; i++)
                                {
                                    Console.WriteLine((i + 1) + ". " + courses[i].Name);
                                }
                                Console.Write("Номер курса (0 - отмена): ");
                                input = Console.ReadLine();
                                if (input == null)
                                {
                                    return;
                                }
                                int courseNumber;
                                if (!int.TryParse(input, out courseNumber) || courseNumber < 0 || courseNumber > courses.Count)
                                {
                                    Console.WriteLine("Неверный номер курса.");
                                    break;
                                }
                                if (courseNumber == 0)
                                {
                                    break;
                                }
                                courses[courseNumber - 1].EnrollStudent(students[studentNumber - 1]);
                                Console.WriteLine("Студент записан на курс.");
                                break;
                            }
                        case 5:
                            {
                                if (students.Count == 0)
                                {
                                    Console.WriteLine("Список пуст.");
                                }
                                foreach (Student student in students)
                                {
                                    IInformation information = student;
                                    information.PrintInfo();
                                    Console.WriteLine();
                                }
                                break;
                            }
                        case 6:
                            {
                                if (teachers.Count == 0)
                                {
                                    Console.WriteLine("Список пуст.");
                                }
                                foreach (Teacher teacher in teachers)
                                {
                                    IInformation information = teacher;
                                    information.PrintInfo();
                                    Console.WriteLine();
                                }
                                break;
                            }
                        case 7:
                            {
                                if (courses.Count == 0)
                                {
                                    Console.WriteLine("Курсов пока нет.");
                                }
                                foreach (Course item in courses)
                                {
                                    item.PrintInfo();
                                    Console.WriteLine();
                                }
                                break;
                            }
                        case 8:
                            {
                                List<Person> selectedPeople = new List<Person>();
                                foreach (Student student in students)
                                {
                                    selectedPeople.Add(student);
                                }
                                if (selectedPeople.Count == 0)
                                {
                                    Console.WriteLine("Список пуст.");
                                    break;
                                }
                                for (int i = 0; i < selectedPeople.Count; i++)
                                {
                                    Console.WriteLine((i + 1) + ". " + selectedPeople[i].Name);
                                }
                                Console.Write("Номер человека (0 - отмена): ");
                                input = Console.ReadLine();
                                if (input == null)
                                {
                                    return;
                                }
                                int personNumber;
                                if (!int.TryParse(input, out personNumber) || personNumber < 0 || personNumber > selectedPeople.Count)
                                {
                                    Console.WriteLine("Неверный номер человека.");
                                    break;
                                }
                                if (personNumber == 0)
                                {
                                    break;
                                }
                                Person selectedPerson = selectedPeople[personNumber - 1];
                                IInformation selectedInformation = (IInformation)selectedPerson;
                                selectedInformation.PrintInfo();
                                Console.WriteLine("Курсы:");
                                int count = 0;
                                foreach (Course item in courses)
                                {
                                    bool found = item.HasStudent((Student)selectedPerson);
                                    if (found)
                                    {
                                        item.PrintInfo();
                                        Console.WriteLine();
                                        count++;
                                    }
                                }
                                if (count == 0)
                                {
                                    Console.WriteLine("Связанных курсов пока нет.");
                                }
                                break;
                            }
                        case 9:
                            {
                                List<Person> selectedPeople = new List<Person>();
                                foreach (Teacher teacher in teachers)
                                {
                                    selectedPeople.Add(teacher);
                                }
                                if (selectedPeople.Count == 0)
                                {
                                    Console.WriteLine("Список пуст.");
                                    break;
                                }
                                for (int i = 0; i < selectedPeople.Count; i++)
                                {
                                    Console.WriteLine((i + 1) + ". " + selectedPeople[i].Name);
                                }
                                Console.Write("Номер человека (0 - отмена): ");
                                input = Console.ReadLine();
                                if (input == null)
                                {
                                    return;
                                }
                                int personNumber;
                                if (!int.TryParse(input, out personNumber) || personNumber < 0 || personNumber > selectedPeople.Count)
                                {
                                    Console.WriteLine("Неверный номер человека.");
                                    break;
                                }
                                if (personNumber == 0)
                                {
                                    break;
                                }
                                Person selectedPerson = selectedPeople[personNumber - 1];
                                IInformation selectedInformation = (IInformation)selectedPerson;
                                selectedInformation.PrintInfo();
                                Console.WriteLine("Курсы:");
                                int count = 0;
                                foreach (Course item in courses)
                                {
                                    bool found = item.Teacher == selectedPerson;
                                    if (found)
                                    {
                                        item.PrintInfo();
                                        Console.WriteLine();
                                        count++;
                                    }
                                }
                                if (count == 0)
                                {
                                    Console.WriteLine("Связанных курсов пока нет.");
                                }
                                break;
                            }
                        case 10:
                            {
                                if (courses.Count == 0)
                                {
                                    Console.WriteLine("Курсов пока нет.");
                                    break;
                                }
                                for (int i = 0; i < courses.Count; i++)
                                {
                                    Console.WriteLine((i + 1) + ". " + courses[i].Name);
                                }
                                Console.Write("Номер курса (0 - отмена): ");
                                input = Console.ReadLine();
                                if (input == null)
                                {
                                    return;
                                }
                                int selectedCourseNumber;
                                if (!int.TryParse(input, out selectedCourseNumber) || selectedCourseNumber < 0 || selectedCourseNumber > courses.Count)
                                {
                                    Console.WriteLine("Неверный номер курса.");
                                    break;
                                }
                                if (selectedCourseNumber == 0)
                                {
                                    break;
                                }
                                Course selectedCourse = courses[selectedCourseNumber - 1];
                                selectedCourse.PrintInfo();
                                Console.WriteLine("Студенты курса:");
                                selectedCourse.PrintStudents();
                                break;
                            }
                        case 0:
                            {
                                Console.WriteLine("Выход из программы.");
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Такого пункта нет.");
                                break;
                            }
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
