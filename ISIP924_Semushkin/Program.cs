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
    internal class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
