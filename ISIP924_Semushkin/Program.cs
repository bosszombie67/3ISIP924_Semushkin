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
        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Contact { get; private set; }
        private List<Course> courses = new List<Course>();
        protected Person(string name, int age, string contact)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя не может быть пустым.");
            if (age < 1 || age > 120)
                throw new ArgumentException("Возраст должен быть от 1 до 120.");
            if (string.IsNullOrWhiteSpace(contact))
                throw new ArgumentException("Контакт не может быть пустым.");
            Name = name.Trim();
            Age = age;
            Contact = contact.Trim();
        }
        protected void PrintPersonInfo()
        {
            Console.WriteLine($"Имя: {Name}; Возраст: {Age}; Контакт: {Contact}");
        }
        internal void AddCourse(Course course)
        {
            if (course == null) throw new ArgumentException("Курс не выбран.");
            if (!courses.Contains(course)) courses.Add(course);
        }
        public void PrintCourses()
        {
            Console.WriteLine("Курсы:");
            if (courses.Count == 0) Console.WriteLine("Связанных курсов пока нет.");
            foreach (Course course in courses) course.PrintInfo();
        }
    }
    class Student : Person, IInformation
    {
        public Student(string name, int age, string contact) : base(name, age, contact) { }
        public void PrintInfo()
        {
            Console.WriteLine("Студент:");
            PrintPersonInfo();
        }
    }
    class Teacher : Person, IInformation
    {
        public Teacher(string name, int age, string contact) : base(name, age, contact) { }
        public void PrintInfo()
        {
            Console.WriteLine("Преподаватель:");
            PrintPersonInfo();
        }
    }
    class Course
    {
        public string Name { get; private set; }
        public Teacher Teacher { get; private set; }
        private List<Student> students = new List<Student>();
        public Course(string name, Teacher teacher)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Название курса не может быть пустым.");
            if (teacher == null)
                throw new ArgumentException("Нужно выбрать преподавателя.");
            Name = name.Trim();
            Teacher = teacher;
            teacher.AddCourse(this);
        }
        public void EnrollStudent(Student student)
        {
            if (student == null) throw new ArgumentException("Нужно выбрать студента.");
            if (students.Contains(student))
                throw new ArgumentException("Студент уже записан на этот курс.");
            students.Add(student);
            student.AddCourse(this);
        }
        public void PrintInfo()
        {
            Console.WriteLine($"Курс: {Name}; преподаватель: {Teacher.Name}; студентов: {students.Count}");
        }
        public void PrintStudents()
        {
            if (students.Count == 0) Console.WriteLine("На курс пока никто не записан.");
            foreach (Student student in students) student.PrintInfo();
        }
    }
    internal class Program
    {
        static List<Student> students = new List<Student>();
        static List<Teacher> teachers = new List<Teacher>();
        static List<Course> courses = new List<Course>();
        static void Main(string[] args)
        {
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
                if (input == null) return;
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
                        case 1: AddPerson(true); break;
                        case 2: AddPerson(false); break;
                        case 3: CreateCourse(); break;
                        case 4: EnrollStudent(); break;
                        case 5: ShowStudents(); break;
                        case 6: ShowTeachers(); break;
                        case 7: ShowCourses(); break;
                        case 8: ShowPersonCourses(ChooseStudent()); break;
                        case 9: ShowPersonCourses(ChooseTeacher()); break;
                        case 10: ShowCourseStudents(); break;
                        case 0: Console.WriteLine("Выход из программы."); break;
                        default: Console.WriteLine("Такого пункта нет."); break;
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        static void AddPerson(bool isStudent)
        {
            Console.Write("Имя: ");
            string name = Console.ReadLine();
            if (name == null) return;
            Console.Write("Возраст: ");
            string input = Console.ReadLine();
            if (input == null) return;
            int age;
            if (!int.TryParse(input, out age)) throw new ArgumentException("Возраст нужно вводить целым числом.");
            Console.Write("Контакт: ");
            string contact = Console.ReadLine();
            if (contact == null) return;
            if (isStudent)
            {
                students.Add(new Student(name, age, contact));
                Console.WriteLine("Студент добавлен.");
            }
            else
            {
                teachers.Add(new Teacher(name, age, contact));
                Console.WriteLine("Преподаватель добавлен.");
            }
        }
        static void CreateCourse()
        {
            if (teachers.Count == 0)
            {
                Console.WriteLine("Сначала добавьте преподавателя.");
                return;
            }
            Console.Write("Название курса: ");
            string title = Console.ReadLine();
            if (title == null) return;
            Teacher teacher = ChooseTeacher();
            if (teacher == null) return;
            courses.Add(new Course(title, teacher));
            Console.WriteLine("Курс создан.");
        }
        static void EnrollStudent()
        {
            if (students.Count == 0 || courses.Count == 0)
            {
                Console.WriteLine("Сначала добавьте студента и создайте курс.");
                return;
            }
            Student student = ChooseStudent();
            if (student == null) return;
            Course course = ChooseCourse();
            if (course == null) return;
            course.EnrollStudent(student);
            Console.WriteLine("Студент записан на курс.");
        }
        static void ShowStudents()
        {
            if (students.Count == 0) Console.WriteLine("Список пуст.");
            foreach (IInformation person in students) person.PrintInfo();
        }
        static void ShowTeachers()
        {
            if (teachers.Count == 0) Console.WriteLine("Список пуст.");
            foreach (IInformation person in teachers) person.PrintInfo();
        }
        static void ShowCourses()
        {
            if (courses.Count == 0) Console.WriteLine("Курсов пока нет.");
            foreach (Course course in courses) course.PrintInfo();
        }
        static void ShowPersonCourses(Person person)
        {
            if (person == null) return;
            IInformation information = (IInformation)person;
            information.PrintInfo();
            person.PrintCourses();
        }
        static void ShowCourseStudents()
        {
            Course course = ChooseCourse();
            if (course == null) return;
            course.PrintInfo();
            Console.WriteLine("Студенты курса:");
            course.PrintStudents();
        }
        static Student ChooseStudent()
        {
            if (students.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return null;
            }
            for (int i = 0; i < students.Count; i++) Console.WriteLine((i + 1) + ". " + students[i].Name);
            Console.Write("Номер студента (0 - отмена): ");
            string input = Console.ReadLine();
            if (input == null) return null;
            int number;
            if (!int.TryParse(input, out number) || number < 0 || number > students.Count) throw new ArgumentException("Неверный номер студента.");
            if (number == 0) return null;
            return students[number - 1];
        }
        static Teacher ChooseTeacher()
        {
            if (teachers.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return null;
            }
            for (int i = 0; i < teachers.Count; i++) Console.WriteLine((i + 1) + ". " + teachers[i].Name);
            Console.Write("Номер преподавателя (0 - отмена): ");
            string input = Console.ReadLine();
            if (input == null) return null;
            int number;
            if (!int.TryParse(input, out number) || number < 0 || number > teachers.Count) throw new ArgumentException("Неверный номер преподавателя.");
            if (number == 0) return null;
            return teachers[number - 1];
        }
        static Course ChooseCourse()
        {
            if (courses.Count == 0)
            {
                Console.WriteLine("Курсов пока нет.");
                return null;
            }
            for (int i = 0; i < courses.Count; i++) Console.WriteLine((i + 1) + ". " + courses[i].Name);
            Console.Write("Номер курса (0 - отмена): ");
            string input = Console.ReadLine();
            if (input == null) return null;
            int number;
            if (!int.TryParse(input, out number) || number < 0 || number > courses.Count) throw new ArgumentException("Неверный номер курса.");
            if (number == 0) return null;
            return courses[number - 1];
        }
    }
}
