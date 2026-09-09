using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP924_Semushkin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            int choice = 0, count = 0;
            string trata;
            Console.WriteLine("Введите количество операций, которые будут записаны:");
            count = int.Parse(Console.ReadLine());
            if (count < 2 && count > 40)
            {
                Console.WriteLine("Вы ввели либо меньше двух, либо больше сорока операций");
            }
            else
            {
                for (int i = 1; i <= count; i++)
                {
                    Console.WriteLine("Введите ваши траты в рублях, в формате(Название услуги или товара; Количество денег):");
                    trata = Console.ReadLine();
                    Console.WriteLine("1. Вывод данных");
                    Console.WriteLine("2. Статистика (среднее, максимальное, минимальное, сумма)");
                    Console.WriteLine("3. Сортировка по цене (пузырьковая сортировка)");
                    Console.WriteLine("4. Конвертация валюты (пользователь вводит курс или выбирает из списка)");
                    Console.WriteLine("5. Поиск по названию ");
                    Console.WriteLine("0. Выход");
                    Console.WriteLine("Ваш выбор операции:");
                    choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        case 0:
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
