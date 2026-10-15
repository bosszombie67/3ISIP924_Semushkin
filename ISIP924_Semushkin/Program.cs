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
        static void DeleteBook(List<Book> books)
        {
            Console.Write("Введите ID: ");
            int id = int.Parse(Console.ReadLine());
            Book book = books.FirstOrDefault(b => b.id == id);
            if (book != null)
                books.Remove(book);
        }
        static void FindBooks(List<Book> books)
        {
            Console.Write("Поиск (1 - название, 2 - автор, 3 - жанр): ");
            int choice = int.Parse(Console.ReadLine());
            Console.Write("Введите значение: ");
            string value = Console.ReadLine() ?? "";
            var result = books.Where(b => choice == 1 ? b.name.Contains(value) : choice == 2 ? b.author.Contains(value) : choice == 3 && b.genre.ToString().Contains(value));
            foreach (var b in result) PrintBook(b);
        }
        static void SortByName(List<Book> books)
        {
            foreach (var b in books.OrderBy(b => b.name)) PrintBook(b);
        }
        static void SortByYear(List<Book> books)
        {
            foreach (var b in books.OrderBy(b => b.pYear)) PrintBook(b);
        }
        static void PriceBooks(List<Book> books)
        {
            if (books.Count == 0) return;
            Book max = books.OrderByDescending(b => b.price).First();
            Book min = books.OrderBy(b => b.price).First();
            Console.WriteLine("Самая дорогая:");
            PrintBook(max);
            Console.WriteLine("Самая дешёвая:");
            PrintBook(min);
        }
        static void GroupBooks(List<Book> books)
        {
            var groups = books.GroupBy(b => b.author);
            foreach (var group in groups) Console.WriteLine($"{group.Key}: {group.Count()} книг");
        }
        static void PrintBook(Book b)
        {
            Console.WriteLine($"ID: {b.id}");
            Console.WriteLine($"Название: {b.name}");
            Console.WriteLine($"Автор: {b.author}");
            Console.WriteLine($"Жанр: {b.genre}");
            Console.WriteLine($"Год: {b.pYear}");
            Console.WriteLine($"Цена: {b.price}");
            Console.WriteLine();
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
