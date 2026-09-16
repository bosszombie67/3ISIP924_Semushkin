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
            Peripherals,
            Computer_Hardware,
            Audio_Video_Equipment,
            Consoles
        }
        class Goods
        {
            private int uCode;
            public int NextCode = 1;
            private string Name { get; set; }
            private decimal Price { get; set; }
            private int Quantity { get; set; }
            private bool State => Quantity > 0;
            private Category Category { get; set; }
            public Goods(string name, decimal price, int quantity, Category category)
            {
                uCode = NextCode;
                NextCode++;
                Name = name;
                Price = price;
                Quantity = quantity;
                Category = category;
            }
        }
        static void Main(string[] args)
        {

        }
    }
}
