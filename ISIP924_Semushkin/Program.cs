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
                Console.Clear();
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
            public static void ShowGoods(List<Goods> Products)
            {
                Console.Clear();
                if (Products.Count == 0)
                {
                    Console.WriteLine("Список товаров пуст.");
                    return;
                }
                foreach (Goods product in Products)
                {
                    Console.WriteLine($"Код: {product.uCode}");
                    Console.WriteLine($"Название: {product.Name}");
                    Console.WriteLine($"Цена: {product.Price}");
                    Console.WriteLine($"Количество: {product.Quantity}");
                    Console.WriteLine($"Категория: {product.Category}");
                    Console.WriteLine($"В наличии: {(product.State ? "Да" : "Нет")}");
                    Console.WriteLine();
                }
            }
            public static void SupplyGoods(List <Goods> Products)
            {
                Console.Clear();
                Console.Write("Введите код товара: ");
                if (!int.TryParse(Console.ReadLine(), out int code))
                {
                    Console.WriteLine("Некорректный код.");
                    return;
                }
                foreach (Goods product in Products)
                {
                    if (product.uCode == code)
                    {
                        Console.Write("Введите количество товара для поставки: ");
                        if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
                        {
                            product.Quantity += quantity;

                            Console.WriteLine("Поставка выполнена.");
                            Console.WriteLine($"Теперь на складе: {product.Quantity}");
                        }
                        else
                        {
                            Console.WriteLine("Некорректное количество.");
                        }
                        return;
                    }
                }
                Console.WriteLine("Товар не найден.");
            }
            public static void FindGoods(List<Goods> Products)
            {
                Console.Clear();
                Console.WriteLine("Поиск товара:");
                Console.WriteLine("1. По коду");
                Console.WriteLine("2. По названию");
                Console.WriteLine("3. По категории");
                Console.Write("Выберите способ поиска: ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Некорректный ввод.");
                    return;
                }
                switch (choice)
                {
                    case 1:
                        FindByCode(Products);
                        break;
                    case 2:
                        FindByName(Products);
                        break;
                    case 3:
                        FindByCategory(Products);
                        break;
                    default:
                        Console.WriteLine("Такого варианта нет.");
                        break;
                }
            }
            private static void FindByCode(List<Goods> Products)
            {
                Console.Write("Введите код товара: ");

                if (!int.TryParse(Console.ReadLine(), out int code))
                {
                    Console.WriteLine("Некорректный код.");
                    return;
                }
                foreach (Goods product in Products)
                {
                    if (product.uCode == code)
                    {
                        ShowProduct(product);
                        return;
                    }
                }
                Console.WriteLine("Товар не найден.");
            }
            private static void FindByName(List<Goods> Products)
            {
                Console.Write("Введите название товара: ");
                string name = Console.ReadLine();
                bool found = false;
                foreach (Goods product in Products)
                {
                    if (product.Name.ToLower().Contains(name.ToLower()))
                    {
                        ShowProduct(product);
                        found = true;
                    }
                }
                if (!found)
                {
                    Console.WriteLine("Товар не найден.");
                }
            }
            private static void FindByCategory(List<Goods> Products)
            {
                Category category = ChooseCategory();
                bool found = false;
                foreach (Goods product in Products)
                {
                    if (product.Category == category)
                    {
                        ShowProduct(product);
                        found = true;
                    }
                }
                if (!found)
                {
                    Console.WriteLine("Товары данной категории не найдены.");
                }
            }
            private static void ShowProduct(Goods product)
            {
                Console.WriteLine();
                Console.WriteLine($"Код: {product.uCode}");
                Console.WriteLine($"Название: {product.Name}");
                Console.WriteLine($"Цена: {product.Price}");
                Console.WriteLine($"Количество: {product.Quantity}");
                Console.WriteLine($"На складе: {(product.State ? "Да" : "Нет")}");
                Console.WriteLine($"Категория: {product.Category}");
                Console.WriteLine();
            }
            public static void SellGoods(List<Goods> Products)
            {

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
            Goods.AddGoods(Products);
            Goods.DeleteGoods(Products);
        }
    }
}
