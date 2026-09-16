using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP924_Semushkin
{
    internal class Program
    {
        public enum Category
        {
            Peripherals = 1,
            Computer_Hardware = 2,
            Audio_Video_Equipment = 3,
            Consoles = 4
        }
        class Goods
        {
            private int uCode { get; }
            public int NextCode = 1;
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
            static void AddGoods(List<Goods> Products){
                Console.Write("Введите название товара: ");
                string name = Console.ReadLine();
                Console.Write("Введите цену товара: ");
                decimal price = decimal.Parse(Console.ReadLine());
                Console.Write("Введите количество товара: ");
                int quantity = int.Parse(Console.ReadLine());
                Console.Write("Выберите категорию товара: ");
                Category category = ChooseCategory();
                Goods newGoods = new Goods(name, price, quantity, category);
                Products.Add(newGoods);
            }
            static void DeleteGoods(List<Goods> Products)
            {

            }
            static void ShowGoods(List<Goods> Products)
            {

            }
            static void FindGoods(List<Goods> Products)
            {

            }
            static void SellGoods(List<Goods> Products)
            {

            }
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
                            return (Category)(number - 1);
                        }
                }
                Console.WriteLine("Вы выбрали некорректную категорию");
            }
        }
        static void Main(string[] args)
        {
            List<Goods> Products = new List<Goods>();
        }
    }
}
