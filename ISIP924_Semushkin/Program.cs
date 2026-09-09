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
            int choice = 7, count = 0, choice2 = 0, price = 0;
            string trata;
            Console.WriteLine("Введите количество операций, которые будут записаны:");
            count = int.Parse(Console.ReadLine());
            List<(string Name, decimal Amount)> Expense = new List<(string Name, decimal Amount)>(count);
            Console.Clear();
            if (count < 2 && count > 40)
            {
                Console.WriteLine("Вы ввели либо меньше двух, либо больше сорока операций");
            }
            else
            {
                for (int i = 1; i <= count; i++)
                {
                    Console.WriteLine($"Операция номер {i} из {count}");
                    Console.WriteLine("Пример: Влажные салфетки \"Лента\"; 235");
                    Console.WriteLine("Введите название покупки/услуги:");
                    trata = Console.ReadLine();
                    Console.WriteLine("Введите цену покупки/услуги:");
                    price = int.Parse(Console.ReadLine());
                    Expense.Add((trata, price));
                    Console.Clear();
                }
                while (choice != 0)
                {
                    Console.WriteLine("---------------------------------Меню----------------------------------");
                    Console.WriteLine("1. Вывод данных");
                    Console.WriteLine("2. Статистика (среднее, максимальное, минимальное, сумма)");
                    Console.WriteLine("3. Сортировка по цене (пузырьковая сортировка)");
                    Console.WriteLine("4. Конвертация валюты (пользователь вводит курс или выбирает из списка)");
                    Console.WriteLine("5. Поиск по названию ");
                    Console.WriteLine("0. Выход");
                    Console.WriteLine("-----------------------------------------------------------------------");
                    Console.WriteLine("Ваш выбор операции:");
                    choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine("Список всех трат(покупок и услуг):");
                            foreach (var golda in Expense)
                            {
                                Console.WriteLine(golda);
                            }
                            break;
                        case 2:
                            Console.Clear();
                            Console.WriteLine("Введите какая вам нужна статистика(1 - среднее, 2 - максимальное, 3 - минимальное, 4 - сумма):");
                            choice2 = int.Parse(Console.ReadLine());
                            switch (choice2)
                            {
                                case 1:
                                    break;
                                case 2:
                                    break;
                                case 3:
                                    break;
                                case 4:
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        case 0:
                            return;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
