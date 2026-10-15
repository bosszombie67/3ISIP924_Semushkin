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
            if (PYear <= 0 || PYear > DateTime.Now.Year) throw new ArgumentException("Неверный год!");
            if (Price < 0) throw new ArgumentException("Цена не может быть отрицательной!");
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
        Сатира = 1,
        Эпическая_Поэма = 2,
        Психологический_Роман = 3,
        Экзистенциализм = 4,
        Нуар = 5
    }
    internal class Program
    {
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
            if (!int.TryParse(Console.ReadLine(), out int choice) || !Enum.IsDefined(typeof(Genre), choice))
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
            var result = books.Where(b =>
            {
                if (choice == 1) return b.name.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
                if (choice == 2) return b.author.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
                return b.genre.ToString().Replace('_', ' ').IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;
            });
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
        static void ImportBooks(List<Book> books)
        {
            Console.WriteLine("Введите книги в формате:");
            Console.WriteLine("Название;Автор;Жанр;Год;Цена");
            Console.WriteLine("Жанр указывается числом от 1 до 5.");
            Console.WriteLine("Пустая строка завершает ввод.");
            int count = 0;
            while (true)
            {
                Console.Write("Книга: ");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) break;
                string[] data = input.Split(';');
                if (data.Length != 5)
                {
                    Console.WriteLine("Неверный формат!");
                    continue;
                }
                string name = data[0].Trim();
                string author = data[1].Trim();
                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(author) || !int.TryParse(data[2], out int genreId) || !Enum.IsDefined(typeof(Genre), genreId) || !int.TryParse(data[3], out int year) || year <= 0 || !decimal.TryParse(data[4], out decimal price) || price < 0)
                {
                    Console.WriteLine("Некорректные данные!");
                    continue;
                }
                Genre genre = (Genre)genreId;
                books.Add(new Book(name, author, genre, year, price));
                count++;
                Console.WriteLine("Книга добавлена!");
            }
            Console.WriteLine($"Всего импортировано книг: {count}");
        }
        static void AddToCart(List<Book> books, List<Book> cart)
        {
            Console.Write("Введите ID книги: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Неверный ID!");
                return;
            }
            Book book = books.FirstOrDefault(b => b.id == id);
            if (book == null)
            {
                Console.WriteLine("Книга не найдена!");
                return;
            }
            cart.Add(book);
            Console.WriteLine("Книга добавлена в корзину!");
        }
        static void ShowCart(List<Book> cart)
        {
            if (cart.Count == 0)
            {
                Console.WriteLine("Корзина пуста!");
                return;
            }
            Console.WriteLine("Ваша корзина:");
            foreach (var book in cart)
            {
                Console.WriteLine($"{book.name} - {book.price} руб.");
            }
            decimal total = cart.Sum(b => b.price);
            Console.WriteLine($"Итоговая стоимость: {total} руб.");
        }
        static void Main(string[] args)
        {
            List<Book> books = new List<Book>
            {
                new Book("Мастер и Маргарита", "Булгаков", Genre.Сатира, 1967, 500),
                new Book("Илиада", "Гомер", Genre.Эпическая_Поэма, 1850, 400),
                new Book("Преступление и наказание", "Достоевский", Genre.Психологический_Роман, 1866, 650),
                new Book("Посторонний", "Камю", Genre.Экзистенциализм, 1942, 350),
                new Book("Мальтийский сокол", "Хэммет", Genre.Нуар, 1930, 700)
            };
            List<Book> cart = new List<Book>();
            while (true)
            {
                Console.WriteLine("1. Добавить книгу");
                Console.WriteLine("2. Удалить книгу");
                Console.WriteLine("3. Найти книгу");
                Console.WriteLine("4. Сортировка по названию");
                Console.WriteLine("5. Сортировка по году");
                Console.WriteLine("6. Самая дорогая и дешёвая книга");
                Console.WriteLine("7. Группировка по авторам");
                Console.WriteLine("8. Импорт книг");
                Console.WriteLine("9. Добавить в корзину");
                Console.WriteLine("10. Показать корзину");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Неверный ввод!");
                    continue;
                }
                switch (choice)
                {
                    case 1:
                        {
                            AddBook(books);
                            break;
                        }
                    case 2:
                        {
                            DeleteBook(books);
                            break;
                        }
                    case 3:
                        {
                            FindBooks(books);
                            break;
                        }
                    case 4:
                        {
                            SortByName(books);
                            break;
                        }
                    case 5:
                        {
                            SortByYear(books);
                            break;
                        }
                    case 6:
                        {
                            PriceBooks(books);
                            break;
                        }
                    case 7:
                        {
                            GroupBooks(books);
                            break;
                        }
                    case 8:
                        {
                            ImportBooks(books);
                            break;
                        }
                    case 9:
                        {
                            AddToCart(books, cart);
                            break;
                        }
                    case 10:
                        {
                            ShowCart(cart);
                            break;
                        }
                    case 0:
                        {
                            return;
                        }
                    default:
                        {
                            Console.WriteLine("Нет такой команды!");
                            break;
                        }
                }
            }
        }
    }
}