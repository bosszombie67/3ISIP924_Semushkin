using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP924_Semushkin
{
    internal class Program
    {
        class Goods
        {
            private int uCode { get; }
            public static int NextCode = 1;
            private string Name { get; set; }
            private decimal Price { get; set; }
            private int Quantity { get; set; }
            private bool State => Quantity > 0;
            private Category Category { get; set; }
            public Goods(string name, decimal price, int quantity, Category category)
            {
                uCode = NextCode++;
                Name = name;
                Price = price;
                Quantity = quantity;
                Category = category;
            }
            public static void AddGoods(List<Goods> Products)
            {
                Console.Clear();
                Console.Write("Введите название товара: ");
                string name = Console.ReadLine();
                Console.Write("Введите цену товара: ");
                decimal price = decimal.Parse(Console.ReadLine());
                Console.Write("Введите количество товара: ");
                int quantity = int.Parse(Console.ReadLine());
                Category category = ChooseCategory();
                Goods newGoods = new Goods(name, price, quantity, category);
                Products.Add(newGoods);
                Console.WriteLine("Товар успешно добавлен");
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
                int Code = FindCode(Products);
                int Quantity;
                while (true) {
                    Console.Write("Введите количество товара, которое требуется поставить: ");
                    if (int.TryParse(Console.ReadLine(), out Quantity))
                    {
                        if (Quantity > 0)
                        {
                            break;
                        }
                        Console.WriteLine("Введите количество товара должно быть больше нуля");
                    }
                }
                foreach (Goods g in Products)
                {
                    if (g.uCode == Code)
                    {
                        g.Quantity += Quantity;
                    }
                }
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
                int Code = FindCode(Products);
                foreach (Goods g in Products)
                {
                    if(g.uCode == Code)
                    {
                        Console.WriteLine($"Товар: {g.Name}");
                        Console.WriteLine($"Доступно: {g.Quantity}");
                        Console.Write("Введите кол-во товара для продажи:");
                        if (!int.TryParse(Console.ReadLine(), out int Quantity) || Quantity <= 0)
                        {
                            Console.WriteLine("Некорректное количество.");
                            return;
                        }
                        if (Quantity > g.Quantity)
                        {
                            Console.WriteLine("На складе недостаточно товара.");
                            return;
                        }
                        g.Quantity -= Quantity;
                        Console.WriteLine($"Товар {g.Name} продан");
                        Console.WriteLine($"Осталось на складе: {g.Quantity}");
                    }
                }
            }
            private static int FindCode(List<Goods> Products)
            {
               Console.Clear();
               Console.Write("Введите код нужного товара: ");
               if (int.TryParse(Console.ReadLine(), out int Code))
               {
                  foreach (Goods g in Products)
                  {
                     if (g.uCode == Code)
                     {
                        return g.uCode;
                     }
                  }
                }
                Console.WriteLine("Не удалось найти код, возвращается 0");
                return 0;
            }
            private static void FindName(List<Goods> Products)
            {
                Console.Clear();
                Console.Write("Введите название товара: ");
                string name = Console.ReadLine();
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
            Goods.ShowAllGoods(Products);
            Goods.SellGoods(Products);
        }
    }
}