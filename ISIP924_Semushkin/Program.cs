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
        static void Main(string[] args)
        {

        }
    }
}
