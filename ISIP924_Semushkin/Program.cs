using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP924_Semushkin
{
    class TextStats
    {
        public string Text;
        public int WordCount;
        public int SentenceCount;
        public int VowelCount;
        public int ConsonantCount;
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
                    Console.Clear();
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
                            Console.Clear();
                            return;
                        }
                    }
                }
                Console.WriteLine("Товар не найден.");
            }
            public static void SupplyGoods(List<Goods> Products)
            {

            }
            public static void ShowGoods(List<Goods> Products)
            {

            }
            public static void FindGoods(List<Goods> Products)
            {

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
            Console.WriteLine("sosal");
        }
    }
}