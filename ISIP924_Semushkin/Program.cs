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
            if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Название не может быть пустым!");
            if (string.IsNullOrWhiteSpace(Author)) throw new ArgumentException("Автор не может быть пустым!");
            if (!Enum.IsDefined(typeof(Genre), Genre)) throw new ArgumentException("Неверный жанр!");
            if (PYear <= 0) throw new ArgumentException("Неверный год!");
            if (Price < 0) throw new ArgumentException("Цена не может быть отрицательной!");
            id = nextId++;
            name = Name;
            author = Author;
            genre = Genre;
            pYear = PYear;
            price = Price;
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
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID!");
                return;
            }
            Book book = books.FirstOrDefault(b => b.id == id);
            if (book != null)
            {
                books.Remove(book);
                Console.WriteLine("Книга удалена!");
            }
            else
            {
                Console.WriteLine("Книга не найдена!");
            }
        }
        static void FindBooks(List<Book> books)
        {
            Console.Write("Поиск (1 - название, 2 - автор, 3 - жанр): ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 3)
            {
                Console.WriteLine("Неверный выбор!");
                return;
            }
            Console.Write("Введите значение: ");
            string value = Console.ReadLine() ?? "";
            if (string.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine("Пустое значение!");
                return;
            }
            var result = books.Where(b => choice == 1 ? b.name.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0 : choice == 2 ? b.author.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0 : b.genre.ToString().Replace('_', ' ').IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0);
            if (!result.Any()) Console.WriteLine("Книги не найдены!");
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
