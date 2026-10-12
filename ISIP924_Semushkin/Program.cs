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
            if (string.IsNullOrWhiteSpace(Name))
            if (string.IsNullOrEmpty(Author)
            if (pYear == 0)
            if (price <= 0)
            id = nextId++;
            name = Name;
            author = Author;
            genre = Genre;
            pYear = PYear;
            price = Price;
        }
    }
    public enum Genre
    {

    }
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
