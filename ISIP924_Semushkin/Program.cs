using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ISIP924_Semushkin
{
    class Book
    {
        public int id { get; }
        private static  int nextId = 1;
        public string name { get; private set; }
        public string author { get; private set; }
        public Genre genre { get; private set; }
        public int pYear { get; private set; }
        public decimal price { get; private set; }
        public Book(string Name, string Author, Genre Genre, int PYear, decimal Price)
        {
            id = nextId++;
            if (!string.IsNullOrWhiteSpace(Name)) name = Name;
            if (!string.IsNullOrWhiteSpace(Author)) author = Author;
            genre = Genre;
            if (pYear >= 0) pYear = PYear;
            if (price > 0) price = Price;
        }
        static void AddBook(List<Book> books)
        {
            Console.Write("Введите название: ");
            string name = Console.ReadLine();
            Console.Write("Введите автора: ");
            string author = Console.ReadLine();
            Console.WriteLine("Выберите жанр:");
            Console.WriteLine("1 - Сатира");
            Console.WriteLine("2 - Эпическая поэма");
            Console.WriteLine("3 - Психологический роман");
            Console.WriteLine("4 - Экзистенциализм");
            Console.WriteLine("5 - Нуар");
            if (!int.TryParse(Console.ReadLine(), out int choice)
                || !Enum.IsDefined(typeof(Genre), choice))
            {
                Console.WriteLine("Неверный жанр!");
                return;
            }
            Genre genre = (Genre)choice;
            Console.Write("Введите год издания: ");
            if (!int.TryParse(Console.ReadLine(), out int year) || year <= 0)
            {
                Console.WriteLine("Неверный год издания!");
                return;
            }
            Console.Write("Введите цену: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price < 0)
            {
                Console.WriteLine("Неверная цена!");
                return;
            }
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(author))
            {
                Console.WriteLine("Название и автор не могут быть пустыми!");
                return;
            }
            books.Add(new Book(name, author, genre, year, price));
            Console.WriteLine("Книга успешно добавлена!");
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
