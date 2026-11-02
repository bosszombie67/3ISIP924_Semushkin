using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace ISIP924_Semushkin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int currentUserId = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== GGWON Маркетплейс ===");
                if (currentUserId == 0)
                {
                    Console.WriteLine("1. Регистрация");
                    Console.WriteLine("2. Вход");
                }
                else
                {
                    Console.WriteLine("1. Выйти из аккаунта");
                }
                Console.WriteLine("3. Просмотр товаров");
                if (currentUserId != 0)
                {
                    Console.WriteLine("4. Добавить товар в корзину");
                    Console.WriteLine("5. Посмотреть корзину");
                    Console.WriteLine("6. Изменить количество");
                    Console.WriteLine("7. Удалить из корзины");
                    Console.WriteLine("8. Купить один товар");
                    Console.WriteLine("9. Купить всю корзину");
                    Console.WriteLine("10. История заказов");
                }
                Console.WriteLine("0. Выход");
                Console.Write("Выбор: "); string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        if (currentUserId == 0)
                        {
                            Register();
                        }
                        else
                        {
                            currentUserId = 0;
                            Console.WriteLine("Вы вышли из аккаунта.");
                        }
                        break;
                    case "2":
                        if (currentUserId == 0)
                        {
                            currentUserId = Login();
                        }
                        break;
                    case "3": ShowProducts(); break;
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                        if (currentUserId == 0)
                        {
                            Console.WriteLine("Сначала войдите в аккаунт!");
                        }
                        else if (choice == "4")
                        {
                            AddToCart(currentUserId);
                        }
                        else if (choice == "5")
                        {
                            ShowCart(currentUserId);
                        }
                        else
                        {
                            EditCart(currentUserId, choice == "7");
                        }
                        break;
                    case "8":
                    case "9":
                    case "10":
                        if (currentUserId == 0)
                        {
                            Console.WriteLine("Сначала войдите в аккаунт!");
                        }
                        else if (choice == "10")
                        {
                            ShowOrders(currentUserId);
                        }
                        else
                        {
                            Buy(currentUserId, choice == "9");
                        }
                        break;
                    case "0": return;
                    default: Console.WriteLine("Нет такого пункта меню!"); break;
                }
                Console.WriteLine("\nНажмите любую клавишу...");
                Console.ReadKey();
            }
        }
        static void Register()
        {
            Console.Write("Логин: "); string login = Console.ReadLine();
            Console.Write("Пароль: "); string password = Console.ReadLine();
            Console.Write("Повторите пароль: "); string repeat = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password) || login.Length > 50 || password.Length > 100)
            {
                Console.WriteLine("Некорректный логин или пароль!");
                return;
            }
            if (password != repeat)
            {
                Console.WriteLine("Пароли не совпадают!");
                return;
            }
            using (var db = new ShopDatabaseEntities())
            {
                foreach (User u in db.Set<User>())
                {
                    if (string.Equals(u.Username, login, StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Логин занят!");
                        return;
                    }
                }
                User user = new User { Username = login, Password = password };
                db.Set<User>().Add(user);
                db.SaveChanges();
                Console.WriteLine("Регистрация успешна!");
            }
        }
        static int Login()
        {
            Console.Write("Логин: "); string login = Console.ReadLine();
            Console.Write("Пароль: "); string password = Console.ReadLine();
            using (var db = new ShopDatabaseEntities())
            {
                foreach (User u in db.Set<User>())
                {
                    if (u.Username == login && u.Password == password)
                    {
                        Console.WriteLine("Вход выполнен!");
                        return u.Id;
                    }
                }
            }
            Console.WriteLine("Неверный логин или пароль!");
            return 0;
        }
        static void ShowProducts()
        {
            using (var db = new ShopDatabaseEntities())
            {
                Console.WriteLine("\n=== ТОВАРЫ ===");
                foreach (Product p in db.Set<Product>())
                {
                    Console.WriteLine($"{p.Id}. {p.Name} — {p.Price:0.00} руб.");
                }
            }
        }
        static void AddToCart(int currentUserId)
        {
            ShowProducts();
            Console.Write("ID товара: "); int productId;
            if (!int.TryParse(Console.ReadLine(), out productId))
            {
                Console.WriteLine("Введите целое число!");
                return;
            }
            using (var db = new ShopDatabaseEntities())
            {
                Product product = db.Set<Product>().Find(productId);
                if (product == null)
                {
                    Console.WriteLine("Товар не найден!");
                    return;
                }
                Console.Write("Количество: "); int quantity;
                if (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
                {
                    Console.WriteLine("Количество должно быть целым числом больше нуля!");
                    return;
                }
                CartItem item = null;
                foreach (CartItem c in db.Set<CartItem>())
                {
                    if (c.UserId == currentUserId && c.ProductId == productId)
                    {
                        item = c;
                        break;
                    }
                }
                if (item == null)
                {
                    db.Set<CartItem>().Add(new CartItem { UserId = currentUserId, ProductId = productId, Quantity = quantity });
                }
                else
                {
                    item.Quantity += quantity;
                }
                db.SaveChanges();
                Console.WriteLine("Товар добавлен в корзину!");
            }
        }
        static void ShowCart(int currentUserId)
        {
            using (var db = new ShopDatabaseEntities())
            {
                List<CartItem> items = new List<CartItem>();
                foreach (CartItem c in db.Set<CartItem>())
                {
                    if (c.UserId == currentUserId)
                    {
                        items.Add(c);
                    }
                }
                if (items.Count == 0)
                {
                    Console.WriteLine("Корзина пуста!");
                    return;
                }
                Console.WriteLine("\n=== КОРЗИНА ===");
                decimal total = 0;
                foreach (CartItem item in items)
                {
                    Product product = db.Set<Product>().Find(item.ProductId);
                    if (product == null)
                    {
                        continue;
                    }
                    decimal sum = product.Price * item.Quantity;
                    total += sum;
                    Console.WriteLine($"Позиция {item.Id}: {product.Name} — " + $"{product.Price:0.00} руб. × " + $"{item.Quantity} = {sum:0.00} руб.");
                }
                Console.WriteLine($"Итого: {total:0.00} руб.");
            }
        }
        static void EditCart(int currentUserId, bool delete)
        {
            Console.Write("Номер позиции из корзины: "); int id;
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Введите целое число!");
                return;
            }
            using (var db = new ShopDatabaseEntities())
            {
                CartItem item = db.Set<CartItem>().Find(id);
                if (item == null || item.UserId != currentUserId)
                {
                    Console.WriteLine("В вашей корзине нет такой позиции!");
                    return;
                }
                if (delete)
                {
                    Console.Write($"Удалить позицию {item.Id} из корзины? (1 — да, 0 — отмена): ");
                    if (Console.ReadLine() != "1")
                    {
                        Console.WriteLine("Удаление отменено.");
                        return;
                    }
                    db.Set<CartItem>().Remove(item);
                }
                else
                {
                    Console.Write("Новое количество: "); int quantity;
                    if (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
                    {
                        Console.WriteLine("Количество должно быть целым числом больше нуля!");
                        return;
                    }
                    item.Quantity = quantity;
                }
                db.SaveChanges();
                Console.WriteLine(delete ? "Товар удалён!" : "Количество изменено!");
            }
        }
        static void Buy(int currentUserId, bool fromCart)
        {
            Console.WriteLine("\nДля отмены покупки введите 0.");
            using (var db = new ShopDatabaseEntities())
            {
                Order order = new Order { UserId = currentUserId, CreatedAt = DateTime.Now };
                List<CartItem> cart = new List<CartItem>();
                if (fromCart)
                {
                    foreach (CartItem item in db.Set<CartItem>())
                    {
                        if (item.UserId == currentUserId)
                        {
                            cart.Add(item);
                        }
                    }
                    if (cart.Count == 0)
                    {
                        Console.WriteLine("Корзина пуста!");
                        return;
                    }
                    foreach (CartItem item in cart)
                    {
                        Product product = db.Set<Product>().Find(item.ProductId);
                        if (product == null)
                        {
                            Console.WriteLine($"Товар с ID {item.ProductId} не найден!");
                            return;
                        }
                        order.OrderProduct.Add(new OrderProduct { ProductId = product.Id, Quantity = item.Quantity, Price = product.Price });
                    }
                }
                else
                {
                    ShowProducts();
                    Console.Write("ID товара (0 — отмена): "); int productId;
                    if (!int.TryParse(Console.ReadLine(), out productId))
                    {
                        Console.WriteLine("Введите целое число!");
                        return;
                    }
                    if (productId == 0)
                    {
                        Console.WriteLine("Покупка отменена.");
                        return;
                    }
                    Product product = db.Set<Product>().Find(productId);
                    if (product == null)
                    {
                        Console.WriteLine("Товар не найден!");
                        return;
                    }
                    Console.Write("Количество (0 — отмена): "); string input = Console.ReadLine();
                    if (input == "0")
                    {
                        Console.WriteLine("Покупка отменена.");
                        return;
                    }
                    int quantity;
                    if (!int.TryParse(input, out quantity) || quantity <= 0)
                    {
                        Console.WriteLine("Количество должно быть целым числом больше нуля!");
                        return;
                    }
                    order.OrderProduct.Add(new OrderProduct { ProductId = product.Id, Quantity = quantity, Price = product.Price });
                }
                decimal total = 0;
                foreach (OrderProduct item in order.OrderProduct)
                {
                    total += item.Price * item.Quantity;
                }
                Console.WriteLine($"\nСумма заказа: {total:0.00} руб.");
                Console.WriteLine("\n=== ПУНКТЫ ВЫДАЧИ ===");
                bool hasPickupPoints = false;
                foreach (PickupPoint point in db.Set<PickupPoint>())
                {
                    Console.WriteLine($"{point.Id}. {point.Name} — {point.Address}");
                    hasPickupPoints = true;
                }
                if (!hasPickupPoints)
                {
                    Console.WriteLine("Нет доступных пунктов выдачи!");
                    return;
                }
                Console.Write("ID пункта выдачи (0 — отмена): "); int pickupPointId;
                if (!int.TryParse(Console.ReadLine(), out pickupPointId))
                {
                    Console.WriteLine("Введите целое число!");
                    return;
                }
                if (pickupPointId == 0)
                {
                    Console.WriteLine("Покупка отменена.");
                    return;
                }
                PickupPoint selectedPoint = db.Set<PickupPoint>().Find(pickupPointId);
                if (selectedPoint == null)
                {
                    Console.WriteLine("Пункт выдачи не найден!");
                    return;
                }
                order.PickupPointId = selectedPoint.Id;
                Console.Write($"Подтвердить покупку на {total:0.00} руб.? (1 — купить, 0 — отмена): ");
                if (Console.ReadLine() != "1")
                {
                    Console.WriteLine("Покупка отменена.");
                    return;
                }
                db.Set<Order>().Add(order);
                foreach (CartItem item in cart)
                {
                    db.Set<CartItem>().Remove(item);
                }
                db.SaveChanges();
                Console.WriteLine($"Заказ №{order.Id} оформлен!");
                Console.WriteLine($"Пункт выдачи: {selectedPoint.Name}, {selectedPoint.Address}");
            }
        }
        static void ShowOrders(int currentUserId)
        {
            using (var db = new ShopDatabaseEntities())
            {
                List<Order> orders = new List<Order>();
                foreach (Order order in db.Set<Order>())
                {
                    if (order.UserId == currentUserId)
                    {
                        orders.Add(order);
                    }
                }
                if (orders.Count == 0)
                {
                    Console.WriteLine("У вас пока нет заказов!");
                    return;
                }
                orders.Sort(CompareOrders);
                List<OrderProduct> items = new List<OrderProduct>();
                foreach (OrderProduct item in db.Set<OrderProduct>())
                {
                    items.Add(item);
                }
                Console.WriteLine("\n=== ИСТОРИЯ ЗАКАЗОВ ===");
                foreach (Order order in orders)
                {
                    Console.WriteLine($"\nЗаказ №{order.Id} от {order.CreatedAt:dd.MM.yyyy HH:mm}");
                    PickupPoint point = db.Set<PickupPoint>().Find(order.PickupPointId);
                    if (point != null)
                    {
                        Console.WriteLine($"Пункт выдачи: {point.Name}, {point.Address}");
                    }
                    decimal total = 0;
                    foreach (OrderProduct item in items)
                    {
                        if (item.OrderId != order.Id)
                        {
                            continue;
                        }
                        Product product = db.Set<Product>().Find(item.ProductId);
                        string name = product == null ? $"Товар с ID {item.ProductId}" : product.Name;
                        decimal sum = item.Price * item.Quantity;
                        total += sum;
                        Console.WriteLine($"{name} — {item.Price:0.00} руб. × " + $"{item.Quantity} = {sum:0.00} руб.");
                    }
                    Console.WriteLine($"Итого: {total:0.00} руб.");
                }
            }
        }
        static int CompareOrders(Order first, Order second)
        {
            return second.CreatedAt.CompareTo(first.CreatedAt);
        }
    }
}