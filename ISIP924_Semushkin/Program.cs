using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP924_Semushkin
{
    abstract class Person
    {
        public string Name { get; }
        public int Age { get; }
        public string Contact { get; }
        protected Person(string name, int age, string contact)
        {
            Name = name;
            Age = age;
            Contact = contact;
        }
        public override string ToString() => $"{Name}, возраст: {Age}, контакт: {Contact}";
    }
    class Student : Person
    {
        public Student(string name, int age, string contact) : base(name, age, contact) { }
        public override string ToString() => "Студент: " + base.ToString();
    }
    class Teacher : Person
    {
        public Teacher(string name, int age, string contact) : base(name, age, contact) { }
        public override string ToString() => "Преподаватель: " + base.ToString();
    }
    class Course
    {
        private readonly List<Student> students = new();
        public string Name { get; }
        public Teacher Teacher { get; }
        public IReadOnlyCollection<Student> Students => students.AsReadOnly();
        public Course(string name, Teacher teacher)
        {
            Name = name;
            Teacher = teacher;
        }
        public bool Enroll(Student student)
        {
            if (students.Contains(student)) return false;
            students.Add(student);
            return true;
        }
        public override string ToString() => $"{Name}; преподаватель: {Teacher.Name}; студентов: {students.Count}";
    }
    internal class Program
    {
        static string Read(string text)
        {
            while (true)
            {
                Console.Write(text);
                string? value = Console.ReadLine();
                if (value == null) Environment.Exit(0);
                if (!string.IsNullOrWhiteSpace(value)) return value.Trim();
                Console.WriteLine("Введите непустое значение.");
            }
        }
        static int Number(string text, int min, int max)
        {
            while (true)
            {
                if (int.TryParse(Read(text), out int n) && n >= min && n <= max) return n;
                Console.WriteLine($"Введите целое число от {min} до {max}.");
            }
        }
        static T? Show<T>(IEnumerable<T> source, bool choose = false) where T : class
        {
            var list = source.ToList();
            if (list.Count == 0) { Console.WriteLine("Список пуст."); return null; }
            for (int i = 0; i < list.Count; i++) Console.WriteLine($"{i + 1}. {list[i]}");
            if (!choose) return null;
            int n = Number("Номер (0 — отмена): ", 0, list.Count);
            return n == 0 ? null : list[n - 1];
        }
        static void Main(string[] args)
        {

        }
    }
}
