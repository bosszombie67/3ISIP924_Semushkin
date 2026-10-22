using System;
using System.Collections.Generic;
using System.Text;

namespace ISIP924_Semushkin
{
    enum ItemType { Weapon, Armor, Potion }
    enum SkeletonType { Normal, Kovalsky, Pestov }
    class Item
    {
        public string Name { get; private set; }
        public ItemType Type { get; private set; }
        public int Power { get; private set; }
        public Item(string name, ItemType type, int power)
        {
            if (string.IsNullOrWhiteSpace(name) || !Enum.IsDefined(typeof(ItemType), type)) throw new ArgumentException("Неверный предмет.");
            if (power < 0 || (type != ItemType.Potion && power == 0)) throw new ArgumentException("Характеристика предмета должна быть положительной.");
            Name = name;
            Type = type;
            Power = power;
        }
        public void PrintInfo()
        {
            if (Type == ItemType.Weapon) Console.WriteLine(Name + ": атака " + Power);
            if (Type == ItemType.Armor) Console.WriteLine(Name + ": защита " + Power);
            if (Type == ItemType.Potion) Console.WriteLine(Name + ": полное лечение");
        }
    }
    class Player
    {
        public int HP { get; private set; } = 100;
        public Item Weapon { get; private set; } = new Item("Старый меч", ItemType.Weapon, 20);
        public Item Armor { get; private set; } = new Item("Кожаный доспех", ItemType.Armor, 5);
        public bool Frozen { get; private set; }
        public void Equip(Item item)
        {
            if (item == null || item.Type == ItemType.Potion)
                throw new ArgumentException("Нужно выбрать оружие или доспех.");
            if (item.Type == ItemType.Weapon) Weapon = item;
            else Armor = item;
        }
        public void Heal()
        {
            HP = 100;
            Console.WriteLine("Здоровье полностью восстановлено.");
        }
        public void Freeze()
        {
            Frozen = true;
            Console.WriteLine("Заморозка! Следующее действие игрока будет пропущено.");
        }
        public bool SkipTurn()
        {
            if (!Frozen) return false;
            Frozen = false;
            Console.WriteLine("Игрок пропускает действие из-за заморозки.");
            return true;
        }
        public bool ReceiveAttack(int attack, bool ignoreArmor, bool defending, Random random)
        {
            if (attack < 0) throw new ArgumentException("Атака не может быть отрицательной.");
            if (defending && random.Next(100) < 40)
            {
                Console.WriteLine("Вы уклонились!");
                return false;
            }
            int defense = Armor.Power;
            if (ignoreArmor) defense = 0;
            int damage = Math.Max(0, attack - defense);
            if (defending)
            {
                int percent = random.Next(70, 101);
                int block = defense * percent / 100;
                damage = Math.Max(0, damage - block);
                Console.WriteLine("Блок: " + percent + "% защиты, снято урона: " + block);
            }
            HP = Math.Max(0, HP - damage);
            Console.WriteLine("Получено урона: " + damage + ". HP: " + HP + "/100");
            return true;
        }
        public void PrintInfo()
        {
            Console.WriteLine("Игрок: " + HP + "/100 HP");
            Weapon.PrintInfo();
            Armor.PrintInfo();
        }
    }
    abstract class Enemy
    {
        public string Name { get; protected set; }
        public int HP { get; protected set; }
        public int Attack { get; protected set; }
        public int Defense { get; protected set; }
        protected Enemy(string name, int hp, int attack, int defense)
        {
            if (string.IsNullOrWhiteSpace(name) || hp <= 0 || attack <= 0 || defense < 0) throw new ArgumentException("Неверные характеристики врага.");
            Name = name;
            HP = hp;
            Attack = attack;
            Defense = defense;
        }
        public void ReceiveAttack(int attack)
        {
            if (attack <= 0) throw new ArgumentException("Атака должна быть положительной.");
            int damage = Math.Max(1, attack - Defense);
            HP = Math.Max(0, HP - damage);
            Console.WriteLine("Вы нанесли урона: " + damage);
        }
        public void PrintInfo()
        {
            Console.WriteLine(Name + ": HP " + HP + ", атака " + Attack + ", защита " + Defense);
        }
        public virtual void MakeAttack(Player player, bool defending, Random random)
        {
            player.ReceiveAttack(Attack, false, defending, random);
        }
    }
    class Goblin : Enemy
    {
        public int CritChance { get; private set; } = 20;
        public Goblin(bool boss = false) : base("Гоблин", 40, 10, 5)
        {
            if (!boss) return;
            Name = "ВВГ";
            HP *= 2;
            Attack = Attack * 15 / 10;
            Defense = Defense * 12 / 10;
            CritChance += 10;
        }
        public override void MakeAttack(Player player, bool defending, Random random)
        {
            int damage = Attack;
            if (random.Next(100) < CritChance)
            {
                damage *= 2;
                Console.WriteLine("Критический удар!");
            }
            player.ReceiveAttack(damage, false, defending, random);
        }
    }
    class Skeleton : Enemy
    {
        public int FreezeChance { get; private set; }
        public Skeleton(SkeletonType type = SkeletonType.Normal) : base("Скелет", 50, 10, 5)
        {
            if (!Enum.IsDefined(typeof(SkeletonType), type)) throw new ArgumentException("Неизвестный тип скелета.");
            if (type == SkeletonType.Kovalsky)
            {
                Name = "Ковальский";
                HP = HP * 25 / 10;
                Attack = Attack * 13 / 10;
                Defense = Defense * 14 / 10;
            }
            if (type == SkeletonType.Pestov)
            {
                Name = "Пестов C--";
                HP = HP * 13 / 10;
                Attack = Attack * 18 / 10;
                Defense = Defense * 6 / 10;
                FreezeChance = Mage.BaseFreezeChance + 15;
            }
        }
        public override void MakeAttack(Player player, bool defending, Random random)
        {
            Console.WriteLine("Атака игнорирует доспех и блок!");
            bool hit = player.ReceiveAttack(Attack, true, defending, random);
            if (hit && FreezeChance > 0 && random.Next(100) < FreezeChance) player.Freeze();
        }
    }
    class Mage : Enemy
    {
        public const int BaseFreezeChance = 20;
        public int FreezeChance { get; private set; } = BaseFreezeChance;
        public Mage(bool boss = false) : base("Маг", 30, 10, 10)
        {
            if (!boss) return;
            Name = "Архимаг C++";
            HP = HP * 18 / 10;
            Attack = Attack * 16 / 10;
            Defense = Defense * 11 / 10;
            FreezeChance += 10;
        }
        public override void MakeAttack(Player player, bool defending, Random random)
        {
            bool hit = player.ReceiveAttack(Attack, false, defending, random);
            if (hit && random.Next(100) < FreezeChance) player.Freeze();
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.OutputEncoding = Encoding.UTF8;
            Run(new Player(), new Random());
        }
        internal static void Run(Player player, Random random)
        {
            int turn = 0;
            Console.WriteLine("Мини-рогалик. На каждом десятом ходу вас ждёт босс.");
            while (player.HP > 0)
            {
                Console.WriteLine();
                Console.WriteLine("1 - Следующий ход");
                Console.WriteLine("2 - Характеристики");
                Console.WriteLine("0 - Выход");
                string choice = Console.ReadLine();
                if (choice == null || choice == "0") return;
                if (choice == "2") { player.PrintInfo(); continue; }
                if (choice != "1") { Console.WriteLine("Нет такого действия."); continue; }
                turn++;
                Console.WriteLine("Ход: " + turn);
                if (turn % 10 == 0)
                {
                    Console.WriteLine("БОСС!");
                    if (!Fight(player, CreateEnemy(true, random), random)) return;
                }
                else if (random.Next(2) == 0)
                {
                    if (!OpenChest(player, random)) return;
                }
                else if (!Fight(player, CreateEnemy(false, random), random)) return;
            }
            Console.WriteLine("Вы погибли. Пройдено ходов: " + turn);
        }
        internal static Enemy CreateEnemy(bool boss, Random random)
        {
            if (boss)
            {
                switch (random.Next(4))
                {
                    case 0: return new Goblin(true);
                    case 1: return new Skeleton(SkeletonType.Kovalsky);
                    case 2: return new Mage(true);
                    default: return new Skeleton(SkeletonType.Pestov);
                }
            }
            switch (random.Next(3))
            {
                case 0: return new Goblin();
                case 1: return new Skeleton();
                default: return new Mage();
            }
        }
        internal static bool Fight(Player player, Enemy enemy, Random random)
        {
            enemy.PrintInfo();
            while (player.HP > 0 && enemy.HP > 0)
            {
                bool defending = false;
                Console.WriteLine("Ваше HP: " + player.HP + "; HP врага: " + enemy.HP);
                if (!player.SkipTurn())
                {
                    Console.WriteLine("1 - Атака");
                    Console.WriteLine("2 - Защита");
                    Console.WriteLine("0 - Выход из игры");
                    string choice = Console.ReadLine();
                    if (choice == null || choice == "0") return false;
                    switch (choice)
                    {
                        case "1": enemy.ReceiveAttack(player.Weapon.Power); break;
                        case "2": defending = true; break;
                        default: Console.WriteLine("Нет такого действия."); continue;
                    }
                }
                if (enemy.HP > 0) enemy.MakeAttack(player, defending, random);
            }
            if (enemy.HP == 0) Console.WriteLine("Победа! " + enemy.Name + " повержен.");
            return true;
        }
        internal static bool OpenChest(Player player, Random random)
        {
            Console.WriteLine("Вы нашли сундук!");
            List<Item> loot = new List<Item>
            {
                new Item("Лечебное зелье", ItemType.Potion, 0),
                new Item("Новый меч", ItemType.Weapon, random.Next(15, 36)),
                new Item("Новый доспех", ItemType.Armor, random.Next(3, 21))
            };
            Item item = loot[random.Next(loot.Count)];
            Console.WriteLine("Найдено:");
            item.PrintInfo();
            if (item.Type == ItemType.Potion) { player.Heal(); return true; }
            Console.WriteLine("Текущая экипировка:");
            player.Weapon.PrintInfo();
            player.Armor.PrintInfo();
            while (true)
            {
                Console.WriteLine("1 - Взять и заменить текущий предмет");
                Console.WriteLine("2 - Выбросить");
                Console.WriteLine("0 - Выход из игры");
                string choice = Console.ReadLine();
                if (choice == null || choice == "0") return false;
                if (choice == "1") { player.Equip(item); Console.WriteLine("Предмет надет."); return true; }
                if (choice == "2") { Console.WriteLine("Предмет выброшен."); return true; }
                Console.WriteLine("Нет такого действия.");
            }
        }
    }
}