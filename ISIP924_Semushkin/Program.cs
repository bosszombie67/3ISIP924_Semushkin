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
            int choice = 7, count = 0, price = 0;
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
                            int choice1 = 1;
                            Console.Clear();
                            Console.WriteLine("Список всех трат(покупок и услуг):");
                            while (choice1 != 0)
                            {
                                foreach (var golda in Expense)
                                {
                                    Console.WriteLine(golda);
                                }
                                Console.Write("Введите 0, чтобы вернуться в меню: ");
                                choice1 = int.Parse(Console.ReadLine());
                            }
                            Console.Clear();
                            break;
                        case 2:
                            Console.Clear();
                            int choice2 = 1;
                            while (choice2 != 0)
                            {
                                Console.WriteLine("Средняя цена покупок/услуг: ", Expense.OfType<int>().Average());
                                Console.WriteLine("Максимальная цена покупок/услуг: ", Expense.OfType<int>().Max());
                                Console.WriteLine("Минимальная цена покупок/услуг: ", Expense.OfType<int>().Min());
                                Console.WriteLine("Сумма цены всех покупок/услуг:", Expense.OfType<int>().Sum());
                                Console.Write("Введите 0, чтобы вернуться в меню: ");
                                choice2 = int.Parse(Console.ReadLine());
                            }
                            Console.Clear();
                            break;
                        case 3:
                            Console.Clear();
                            bool swapped;
                            for (int i = 0; i < Expense.Count() - 1; i++)
                            {
                                swapped = false;
                                for (int j = 0; j < Expense.Count() - i - 1; j++)
                                {
                                    if (Expense[j].Amount > Expense[j + 1].Amount)
                                    {
                                        var temp = Expense[j];
                                        Expense[j] = Expense[j + 1];
                                        Expense[j + 1] = temp;
                                        swapped = true;
                                    }
                                }
                                if (!swapped) break;
                            }
                            int choice3 = 1;
                            while (choice3 != 0)
                            {
                                Console.WriteLine("Все траты успешно отсортированы по цене пузырьковой сортировкой");
                                Console.Write("Введите 0, чтобы вернуться в меню: ");
                                choice3 = int.Parse(Console.ReadLine());
                            }
                            Console.Clear();
                            break;
                        case 4:
                            Console.Clear();
                            Console.Write("Введите курс валюты - стоимость одной валюты в рублях: ");
                            decimal ERate = decimal.Parse(Console.ReadLine());
                            if (ERate <= 0)
                            {
                                Console.Write("Введите нормальный курс валюты");
                            }
                            Console.Write("Введите название валюты(например, USD): ");
                            string CName = Console.ReadLine();
                            foreach(var e in Expense)
                            {
                                decimal Converted = e.Amount / ERate;
                                Console.WriteLine($"{e.Name} - {Converted} - {CName:F2}");
                            }
                            int choice4 = 1;
                            while(choice4 != 0)
                            {
                                Console.Write("Введите 0, чтобы вернуться в меню: ");
                                choice4 = int.Parse(Console.ReadLine());
                            }
                            Console.Clear();
                            break;
                        case 5:

                            break;
                        case 0:
                            return;
                        default:
                            Console.WriteLine("Введите корректное число");
                            Console.Clear();
                            break;
                    }
                }
            }
        }
    }
}
