using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP924_Semushkin
{
    class TextAnalysis
    {
        public string Text { get; set; }
        public int WordCount { get; set; }
        public int SentenceCount { get; set; }
        public string ShortestWord { get; set; }
        public string LongestWord { get; set; }
        public int VowelCount { get; set; }
        public int ConsonantCount { get; set; }
    }
    internal class Program
    {
        class Goods
        {
            private int uCode { get; }
            public static int NextCode = 1;
            private string Name { get; set; }
            private decimal Price { get; set; }
            private int Quantity { get; set; }
            private Category Category { get; set; }
            public Goods(string name, decimal price, int quantity, Category category)
            {
                if (string.IsNullOrWhiteSpace(name))
                    throw new ArgumentException("Название товара не может быть пустым.");
                if (price < 0)
                    throw new ArgumentException("Цена товара не может быть отрицательной.");
                if (quantity < 0)
                    throw new ArgumentException("Количество товара не может быть отрицательным.");
                uCode = NextCode++;
                Name = name;
                Price = price;
                Quantity = quantity;
                Category = category;
            }
            public static void AddGoods(List<Goods> Products)
            {
                Console.Clear();
                string name;
                while (true)
                {
                    Console.Write("Введите название товара: ");
                    name = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        break;
                    }
                    Console.WriteLine("Название товара не может быть пустым.");
                }
                decimal price;
                while (true)
                {
                    Console.Write("Введите цену товара: ");
                    if (decimal.TryParse(Console.ReadLine(), out price) && price >= 0)
                    {
                        break;
                    }
                    Console.WriteLine("Цена должна быть числом и не может быть отрицательной.");
                }

                int quantity;
                while (true)
                {
                    Console.Write("Введите количество товара: ");

                    if (int.TryParse(Console.ReadLine(), out quantity) && quantity >= 0)
                    {
                        break;
                    }

                    Console.WriteLine("Количество должно быть целым числом и не может быть отрицательным.");
                }
                Category category = ChooseCategory();
                Goods newGoods = new Goods(name, price, quantity, category);
                Products.Add(newGoods);
                Console.WriteLine("Товар успешно добавлен.");
            }
            static Category ChooseCategory()
            {
                while (true)
                {
                    Console.WriteLine("Категории:");
                    Console.WriteLine("1. Периферийные устройства");
                    Console.WriteLine("2. Компьютерное оборудование");
                    Console.WriteLine("3. Аудио/Видео оборудование");
                    Console.WriteLine("4. Консоли");
                    Console.Write("Выберите категорию: ");
                    if (int.TryParse(Console.ReadLine(), out int number))
                    {
                        if (number >= 1 && number <= 4)
                        {
                            return (Category)number;
                        }
                    }
                    Console.WriteLine("Вы выбрали некорректную категорию");
                }
            }
            public static void DeleteGoods(List<Goods> Products)
            {
                Console.Clear();
                Console.Write("Введите код товара для удаления: ");
                if (int.TryParse(Console.ReadLine(), out int code))
                {
                    for (int i = 0; i < Products.Count; i++)
                    {
                        if (Products[i].uCode == code)
                        {
                            Products.RemoveAt(i);
                            Console.WriteLine("Товар удален.");
                            return;
                        }
                    }
                }
                Console.WriteLine("Товар не найден.");
            }
            public static void SupplyGoods(List<Goods> Products)
            {
                Console.Clear();
                Console.WriteLine("=== ПОСТАВКА ТОВАРА ===");
                Goods product = FindCode(Products);
                if (product == null)
                {
                    return;
                }
                Console.WriteLine($"Товар: {product.Name}");
                Console.WriteLine($"Сейчас на складе: {product.Quantity}");
                int quantity;
                while (true)
                {
                    Console.Write("Введите количество товара для поставки: ");
                    if (int.TryParse(Console.ReadLine(), out quantity) && quantity > 0)
                    {
                        break;
                    }
                    Console.WriteLine("Количество должно быть целым числом больше нуля.");
                }
                product.Quantity += quantity;
                Console.WriteLine();
                Console.WriteLine($"Поставка товара \"{product.Name}\" выполнена.");
                Console.WriteLine($"Теперь на складе: {product.Quantity}");
            }
            public static void ShowAllGoods(List<Goods> Products)
            {
                Console.Clear();
                Console.WriteLine("Код|Название|Цена|Количество|Категория");
                foreach(Goods g in Products)
                {
                    Console.WriteLine($"{g.uCode}|{g.Name}|{g.Price}|{g.Quantity}|{g.Category}");
                }
            }
            public static void FindGoods(List<Goods> Products)
            {
                Console.Clear();
                Console.WriteLine("Меню поиска товара");
                Console.WriteLine("1. По коду");
                Console.WriteLine("2. По названию");
                Console.WriteLine("3. По категории");
                int choice;
                while (true)
                {
                    Console.Write("Выбор поиска: ");
                    if(int.TryParse(Console.ReadLine(), out choice) && (choice == 1 || choice == 2 || choice == 3)){
                        break;
                    }
                    Console.WriteLine("Введите корректный выбор");
                }
                switch (choice)
                {
                    case 1:
                        FindWCode(Products);
                        break;
                    case 2:
                        FindName(Products);
                        break;
                    case 3:
                        FindCategory(Products);
                        break;
                }
            }
            public static void SellGoods(List<Goods> Products)
            {
                Console.Clear();
                Goods product = FindCode(Products);
                if (product == null)
                {
                    return;
                }
                Console.WriteLine($"Товар: {product.Name}");
                Console.WriteLine($"Цена: {product.Price} руб.");
                Console.WriteLine($"Доступно на складе: {product.Quantity}");
                if (product.Quantity == 0)
                {
                    Console.WriteLine("Товара нет на складе.");
                    return;
                }
                int quantity;
                while (true)
                {
                    Console.Write("Введите количество товара для продажи: ");

                    if (!int.TryParse(Console.ReadLine(), out quantity))
                    {
                        Console.WriteLine("Введите целое число.");
                        continue;
                    }
                    if (quantity <= 0)
                    {
                        Console.WriteLine("Количество должно быть больше нуля.");
                        continue;
                    }
                    if (quantity > product.Quantity)
                    {
                        Console.WriteLine($"Недостаточно товара. На складе только: {product.Quantity}");
                        continue;
                    }
                    break;
                }
                product.Quantity -= quantity;
                decimal sum = product.Price * quantity;
                Console.WriteLine();
                Console.WriteLine("Продажа выполнена.");
                Console.WriteLine($"Товар: {product.Name}");
                Console.WriteLine($"Продано: {quantity} шт.");
                Console.WriteLine($"Сумма продажи: {sum} руб.");
                Console.WriteLine($"Осталось на складе: {product.Quantity}");
            }
            private static Goods FindCode(List<Goods> Products)
            {
                int code;
                while (true)
                {
                    Console.Write("Введите код товара: ");
                    if (int.TryParse(Console.ReadLine(), out code) && code > 0)
                    {
                        break;
                    }
                    Console.WriteLine("Код товара должен быть целым числом больше нуля.");
                }
                foreach (Goods g in Products)
                {
                    if (g.uCode == code)
                    {
                        return g;
                    }
                }
                Console.WriteLine("Товар с таким кодом не найден.");
                return null;
            }
            private static void FindName(List<Goods> Products)
            {
                Console.Clear();
                Console.Write("Введите название товара: ");
                string name;
                while (true)
                {
                    Console.Write("Введите название товара: ");
                    name = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        break;
                    }
                    Console.WriteLine("Название не может быть пустым.");
                }
                bool f = false;
                foreach (Goods g in Products)
                {
                    if (g.Name.ToLower().Contains(name.ToLower()))
                    {
                        ShowGood(g);
                        f = true;
                    }
                }
                if (!f)
                {
                    Console.WriteLine("Товар не найден.");
                }
            }
            private static void FindCategory(List<Goods> Products)
            {
                Console.Clear();
                Category category = ChooseCategory();
                bool f = false;
                foreach(Goods g in Products)
                {
                    if(g.Category == category){
                        ShowGood(g);
                        f = true;
                    }
                }
                if (!f)
                {
                    Console.WriteLine("Товар не найден.");
                }
            }
            private static void FindWCode(List<Goods> Products)
            {
                Console.Clear();
                int code;
                bool f = false;
                while (true)
                {
                    Console.Write("Введите код товара, который нужно найти: ");
                    if (int.TryParse(Console.ReadLine(), out code))
                    {
                        if (code > 0)
                        {
                            break;
                        }
                        Console.WriteLine("Введите корректный код");
                    }
                }
                foreach(Goods g in Products)
                {
                    if(g.uCode == code)
                    {
                        ShowGood(g);
                        f = true;
                    }
                }
                if (!f)
                {
                    Console.WriteLine("Товар не найден.");
                }
            }
            private static void ShowGood(Goods Product)
            {
                Console.WriteLine("Код|Название|Цена|Количество|Категория");
                Console.WriteLine($"{Product.uCode}|{Product.Name}|{Product.Price}|{Product.Quantity}|{Product.Category}");
            }
        }
        public enum Category
        {
            Peripherals = 1,
            Computer_Hardware = 2,
            Audio_Video_Equipment = 3,
            Consoles = 4
        }
        static void Main(string[] args)
        {
            List<Goods> Products = new List<Goods>();
            Products.Add(new Goods("RTX 5070", 110000, 69, Category.Computer_Hardware));
            Products.Add(new Goods("PlayStation 5 Pro", 140000, 67, Category.Consoles));
            Products.Add(new Goods("SteelSeries Arctis Nova Pro Wireless", 32000, 52, Category.Audio_Video_Equipment));
            Products.Add(new Goods("Logitech G102", 2000, 1488, Category.Peripherals));
            Products.Add(new Goods("AMD Ryzen Threadripper Pro 9995WX", 1567000, 2, Category.Computer_Hardware));
            while (true)
            {
                Console.Clear();
                Console.WriteLine("================================");
                Console.WriteLine("       УЧЁТ ТОВАРОВ СКЛАДА");
                Console.WriteLine("================================");
                Console.WriteLine("1. Показать все товары");
                Console.WriteLine("2. Добавить товар");
                Console.WriteLine("3. Удалить товар");
                Console.WriteLine("4. Поставить товар на склад");
                Console.WriteLine("5. Продать товар");
                Console.WriteLine("6. Найти товар");
                Console.WriteLine("0. Выход");
                Console.WriteLine("================================");
                Console.Write("Выберите действие: ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Введите число.");
                    Console.WriteLine("Нажмите Enter для продолжения...");
                    Console.ReadLine();
                    continue;
                }
                switch (choice)
                {
                    case 1:
                        Goods.ShowAllGoods(Products);
                        break;

                    case 2:
                        Goods.AddGoods(Products);
                        break;

                    case 3:
                        Goods.DeleteGoods(Products);
                        break;

                    case 4:
                        Goods.SupplyGoods(Products);
                        break;

                    case 5:
                        Goods.SellGoods(Products);
                        break;

                    case 6:
                        Goods.FindGoods(Products);
                        break;

                    case 0:
                        Console.WriteLine("Программа завершена.");
                        return;

                    default:
                        Console.WriteLine("Такого пункта меню нет.");
                        break;
                }
                Console.WriteLine("Нажмите Enter для продолжения...");
                Console.ReadLine();
            }
        }
    }
}