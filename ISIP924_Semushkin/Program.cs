using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ISIP924_Semushkin
{
    class Book
    {
        private int id { get; }
        public int nextId = 1;
        public string name { get; private set; }
        public string author { get; private set; }
        public Genre genre { get; private set; }
        public int pYear { get; private set; }
        public decimal price { get; private set; }
        public Book(string Name, string Author, Genre Genre, int PYear, decimal Price)
        {
            id = nextId++;
            if (!string.IsNullOrWhiteSpace(Name)) name = Name;
            if (!string.IsNullOrEmpty(Author)) author = Author;
            genre = Genre;
            if (pYear == 0) pYear = PYear;
            if (price <= 0) price = Price;
        }
    }
    public enum Genre
    {
        Сатира = 1,
        Эпическая_Поэма = 2,
        Психологический_Роман = 3,
        Экзистенциализм = 4,
        Нуар = 5
    }
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
