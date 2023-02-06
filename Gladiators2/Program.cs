namespace Gladiators
{
    class Programm
    {
        static void Main()
        {
            Menu menu = new Menu();
            menu.ShowMainMenu();
        }
    }

    class Menu
    {
        private const string MenuChooseGladiators = "1";
        private const string MenuFight = "2";
        private const string MenuExit = "0";
        private const string MenuGladiator1 = "1";
        private const string MenuGladiator2 = "2";
        private const string MenuGladiator3 = "3";
        private const string MenuGladiator4 = "4";
        private const string MenuGladiator5 = "5";
        private const string MenuBack = "0";

        Arena arena = new Arena();
        Fighter fighter = new Fighter();
        Rouge rouge = new Rouge();
        Knight knight = new Knight();
        Cleric cleric = new Cleric();
        Doppelganger doppelganger = new Doppelganger();

        public void ShowMainMenu()
        {
            bool isExit = false;
            string userInput;

            while (isExit == false)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine(MenuChooseGladiators + " - Выбор гладиаторов");
                Console.WriteLine(MenuFight + " - Бой!");
                Console.WriteLine(MenuExit + " - Выход");

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case MenuChooseGladiators:
                        ShowFightersMenu();
                        break;

                    case MenuFight:
                        arena.Fight();
                        break;
                    case MenuExit:
                        isExit = true;
                        break;
                }
            }
        }

        public void ShowFightersMenu()
        {
            bool isBack = false;
            string userInput;

            Gladiator[] gladiators = new Gladiator[]
            {
                new Fighter(),
                new Rouge(),
                new Knight(),
                new Cleric(),
                new Doppelganger(),
            };

            while (isBack == false)
            {
                Console.WriteLine("Список гладиаторов:");

                ShowAllGladiators();

                Console.WriteLine("\nВыберете гладиатора:");
                Console.WriteLine(MenuGladiator1 + " - " + fighter.Name);
                Console.WriteLine(MenuGladiator2 + " - " + rouge.Name);
                Console.WriteLine(MenuGladiator3 + " - " + knight.Name);
                Console.WriteLine(MenuGladiator4 + " - " + cleric.Name);
                Console.WriteLine(MenuGladiator5 + " - " + doppelganger.Name);
                Console.WriteLine(MenuBack + " - Назад");

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case MenuGladiator1:
                        Console.WriteLine("Выбран " + fighter.Name);
                        AddGladiatorToArena(new Fighter());
                        break;

                    case MenuGladiator2:
                        Console.WriteLine("Выбран " + rouge.Name);
                        AddGladiatorToArena(new Rouge());
                        break;

                    case MenuGladiator3:
                        Console.WriteLine("Выбран " + knight.Name);
                        AddGladiatorToArena(new Knight());
                        break;

                    case MenuGladiator4:
                        Console.WriteLine("Выбран " + cleric.Name);
                        AddGladiatorToArena(new Cleric());
                        break;
                    case MenuGladiator5:
                        Console.WriteLine("Выбран " + doppelganger.Name);
                        AddGladiatorToArena(new Doppelganger());
                        break;

                    case MenuExit:
                        isBack = true;
                        break;
                }

                if (arena.Gladiators.Count == 2)
                {
                    Console.WriteLine($"Идущие на смерть {arena.Gladiators[0].Name} и {arena.Gladiators[1].Name} приветствуют тебя!");
                    isBack = true;
                }
            }
        }

        private void ShowAllGladiators()
        {
            fighter.ShowStats();
            Console.WriteLine("Сбалансированный боец со случайным начальным уроном, который останется постоянным, каждый удар.");
            rouge.ShowStats();
            Console.WriteLine("Вор - боец с небольшим базовым уроном, но возможностью нанести критический удар или полностью уклониться от атаки.");
            knight.ShowStats();
            Console.WriteLine("Рыцарь. Весь в броне и со щитом, которым может воспользоваться в любой момент и увеличить свою броню.");
            cleric.ShowStats();
            Console.WriteLine("Боевой храмовник. Может вылечить себя после удара.");
            doppelganger.ShowStats();
            Console.WriteLine("Странная раздвоенная сущность. Может, как нанести двойной урон, так и разделить полученный между сущностями.");
        }

        private void AddGladiatorToArena(Gladiator gladiator)
        {
            arena.Gladiators.Add(gladiator);
        }
    }

    class Arena
    {
        public List<Gladiator> Gladiators = new List<Gladiator>();

        public Arena()
        {
        }

        public void Fight()
        {
            if (Gladiators.Count == 2)
            {
                Gladiators[0].ShowStats();
                Gladiators[1].ShowStats();

                while (Convert.ToInt32(Gladiators[0].Health) > 0 && Convert.ToInt32(Gladiators[1].Health) > 0)
                {
                    Gladiators[0].ShowStats();
                    Gladiators[0].TakeDamage(Gladiators[1].Damage());

                    Console.WriteLine("------------------------------------------------------------");

                    Gladiators[1].ShowStats();
                    Gladiators[1].TakeDamage(Gladiators[0].Damage());

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("============================================================");
                    Console.ResetColor();
                }

                if (Gladiators[0].Health <= 0 && Gladiators[1].Health > 0)
                {
                    Console.WriteLine(Gladiators[0].Name + " Повержен! Победил " + Gladiators[1].Name + "!");
                    Gladiators.Clear();
                }
                else if (Gladiators[0].Health > 0 && Gladiators[1].Health <= 0)
                {
                    Console.WriteLine(Gladiators[1].Name + " Повержен! Победил " + Gladiators[0].Name + "!");
                    Gladiators.Clear();
                }
                else if (Gladiators[0].Health <= 0 && Gladiators[1].Health <= 0)
                {
                    Console.WriteLine("Все мертвы! Нет победителя.");
                    Gladiators.Clear();
                }
            }
            else
            {
                Console.WriteLine("Для боя нужно 2 гладиатора!");
            }
        }
    }

    class Gladiator
    {
        protected static Random _random = new Random();

        public string Name { get; protected set; }
        protected int _health { get; set; }
        protected int _hitDamage { get; set; }
        protected int _baseDamage { get; set; } = 10;
        protected int _armor { get; set; }

        public Gladiator(string name, int health, int hitDamage, int armor)
        {
            Name = name;
            _health = health;
            _hitDamage = hitDamage;
            _armor = armor;
        }

        public Gladiator()
        {
        }

        public int Health
        {
            get
            {
                return _health;
            }
        }

        public virtual int Damage()
        {
            int damage = _hitDamage;
            return damage;
        }

        public void ShowStats()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"\nГладиатор: {Name} ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Здоровье: {_health} ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"Урон: {_hitDamage} ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Броня: {_armor}");
            Console.ResetColor();
        }

        public virtual void TakeDamage(int damage)
        {
            int healthBeforeDamage = _health;

            UseDefenceAbility();

            if (damage < _armor)
            {
                _health -= _baseDamage;
            }
            else
            {
                _health = Math.Max(0, _health - Math.Max(_baseDamage, damage - _armor));
            }

            Console.WriteLine("Получено урона: " + (healthBeforeDamage - _health));
        }

        protected virtual void UseDefenceAbility()
        {
        }
    }

    class Fighter : Gladiator
    {
        public Fighter(string name, int health, int hitDamage, int armor) : base(name, health, hitDamage, armor)
        {
        }

        public Fighter()
        {
            Name = "Fighter Criss";
            _health = 1000;
            _hitDamage = _random.Next(90, 130);
            _armor = 20;
        }
    }

    class Rouge : Gladiator
    {
        private bool _isDoged;

        public Rouge(string name, int health, int hitDamage, int armor) : base(name, health, hitDamage, armor)
        {
        }

        public Rouge()
        {
            Name = "Rouge Karl";
            _health = 1000;
            _hitDamage = 70;
            _armor = 5;
        }

        public override int Damage()
        {
            return CriticalHit();
        }

        private int CriticalHit()
        {
            Random random = new Random();
            double critMultiplier = 3;
            int critChance = 25;
            int baseDamage = _hitDamage;

            if (random.Next(0, 100) < critChance)
            {
                baseDamage = Convert.ToInt32(Math.Round(baseDamage * critMultiplier));
                Console.WriteLine($"Получен критический удар!");
            }

            return baseDamage;
        }

        public override void TakeDamage(int damage)
        {
            int healthBeforeDamage = _health;

            UseDefenceAbility();

            if (_isDoged == true)
            {
                damage = 0;
                _baseDamage = 0;
            }
            else if (_isDoged == false && _armor >= damage)
            {
                _health -= _baseDamage;
            }
            else
            {
                _health = Math.Max(0, _health - (damage - _armor));
            }

            Console.WriteLine("Получено урона: " + (healthBeforeDamage - _health));

            _baseDamage = 10;
        }

        protected override void UseDefenceAbility()
        {
            _isDoged = IsDoge();
        }

        private bool IsDoge()
        {
            Random random = new Random();
            int dogeChance = 20;
            _isDoged = false;

            if (random.Next(0, 100) < dogeChance)
            {
                Console.WriteLine($"Промах! Урон не прошел!");
                _isDoged = true;
            }
            else
            {
                _isDoged= false;
            }

            return _isDoged;
        }
    }

    class Knight : Gladiator
    {
        private int _shieldArmor;

        public Knight(string name, int health, int hitDamage, int armor) : base(name, health, hitDamage, armor)
        {
        }

        public Knight()
        {
            Name = "Knight Robert";
            _health = 1000;
            _hitDamage = 100;
            _armor = 30;
        }

        protected override void UseDefenceAbility()
        {
            if (_shieldArmor > 0)
            {
                _armor -= _shieldArmor;
            }

            _shieldArmor = RiseShield();
            _armor += _shieldArmor;
        }

        private int RiseShield()
        {
            Random random = new Random();
            int getArmorChance = 30;
            int addArmor = 20;
            int extraArmor = 0;

            if (random.Next(0, 100) < getArmorChance)
            {
                extraArmor += addArmor;
                Console.WriteLine($"Поднят щит, добавлено {extraArmor} брони!");
            }

            return extraArmor;
        }
    }

    class Cleric : Gladiator
    {
        public Cleric(string name, int health, int hitDamage, int armor) : base(name, health, hitDamage, armor)
        {
        }

        public Cleric()
        {
            Name = "Cleric Flatis";
            _health = 1000;
            _hitDamage = 100;
            _armor = 30;
        }

        protected override void UseDefenceAbility()
        {
            int healAmmount = Heal();

            if (_health + healAmmount > 1000)
            {
                _health += _health + Heal() - 1000;
            }
            else 
            {
                _health += healAmmount;
            }
        }

        private int Heal()
        {
            Random random = new Random();
            int getHealthChance = 20;
            int addHealth = 50;
            int extraHealth = 0;

            if (random.Next(0, 100) < getHealthChance)
            {
                extraHealth += addHealth;
                Console.WriteLine("Применено лечение, восстановлено: " + addHealth + " здоровья!");
            }

            return extraHealth;
        }

        public override void TakeDamage(int damage)
        {
            _health += Heal();

            int healthBeforeDamage = _health;

            if (damage < _armor)
            {
                _health -= _baseDamage;
            }
            else
            {
                _health = Math.Max(0, _health - (damage - _armor));
            }

            Console.WriteLine("Получено урона: " + (healthBeforeDamage - _health));
        }
    }

    class Doppelganger : Gladiator
    {
        public Doppelganger(string name, int health, int hitDamage, int armor) : base(name, health, hitDamage, armor)
        {
        }

        public Doppelganger()
        {
            Name = "Doppelganger M'aik-M'aik";
            _health = 1000;
            _hitDamage = 60;
            _armor = 15;
        }

        public override int Damage()
        {
            return DoubleHit();
        }

        private int DoubleHit()
        {
            Random random = new Random();
            double hitMultiplier = 2;
            int doubleHitChance = 50;
            int baseDamage = _hitDamage;

            if (random.Next(0, 100) < doubleHitChance)
            {
                baseDamage = Convert.ToInt32(Math.Round(baseDamage * hitMultiplier));
                Console.WriteLine($"Получен двойной удар!");
            }

            return baseDamage;
        }

        public override void TakeDamage(int damage)
        {
            int healthBeforeDamage = _health;
            damage = DevidedDamage(damage);

            if (damage <= _armor)
            {
                _health -= DevidedDamage(_baseDamage);
            }
            else
            {
                _health = Math.Max(0, _health - (damage - _armor));
            }

            Console.WriteLine("Получено урона: " + (healthBeforeDamage - _health));
        }

        private int DevidedDamage(int damage)
        {
            Random random = new Random();
            int devideDamageChance = 50;
            int devideDamageBy = 2;

            if (random.Next(0, 100) < devideDamageChance)
            {
                damage = damage / devideDamageBy;
                Console.WriteLine($"Урон разделён между сущностями!");
            }

            return damage;
        }
    }
}