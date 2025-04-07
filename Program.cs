namespace HeroRPG
{
    class Program
    {
        static void Main(string[] _)
        {
            // Create hero
            Console.WriteLine("What is your name?\n");
            Character hero = new Character();
            hero.Name = Console.ReadLine() ?? "Hero";
            hero.Health = 100;

            // Create Enemies
            Character enemyGoblin = new Character { Name = "Goblin", Health = 50 };
            Character enemySkeleton = new Character { Name = "Skeleton", Health = 60 };
            Character enemyOrc = new Character { Name = "Orc", Health = 70 };
            Character enemyGhost = new Character { Name = "Ghost", Health = 80 };

            Console.WriteLine("\nWho do you want to fight?");
            Console.WriteLine("\n1 - Goblin\n2 - Skeleton\n3 - Orc\n4 - Ghost\n");

            int choice;
            Character? selectedEnemy = null;

            // select enemy
            while (true)
            {
                bool valid = int.TryParse(Console.ReadLine(), out choice);

                if (!valid || choice < 1 || choice > 4)
                {
                    Console.WriteLine("Please enter a valid choice.");
                }

                switch (choice)
                {
                    case 1: selectedEnemy = enemyGoblin; break;
                    case 2: selectedEnemy = enemySkeleton; break;
                    case 3: selectedEnemy = enemyOrc; break;
                    case 4: selectedEnemy = enemyGhost; break;
                }

                if (selectedEnemy != null)
                {
                    Console.WriteLine($"You have chosen to fight the {selectedEnemy.Name}!");
                }

                break;
            }

            Random rng = new Random();

            // battle start

            if (selectedEnemy != null)
            {
                while (selectedEnemy.Health > 0 && hero.Health > 0)
                {
                    Console.WriteLine("\nPress Enter to attack...");
                    Console.ReadLine();

                    // hero's attack

                    hero.Attack();
                    bool enemyBlocked = rng.Next(2) == 0;
                    if (enemyBlocked)
                    {
                        Console.WriteLine($"{selectedEnemy.Name} blocked your attack!");
                    }
                    else
                    {
                        int heroDamage = rng.Next(20, 40);
                        selectedEnemy.TakeDamage(heroDamage);
                    }

                    if (selectedEnemy.Health <= 0)
                    {
                        Console.WriteLine($"\n{selectedEnemy.Name} has been defeated by {hero.Name}!");
                        break;
                    }

                    //enemy's attack

                    selectedEnemy.Attack();
                    bool heroBlocked = rng.Next(2) == 0;
                    if (heroBlocked)
                    {
                        Console.WriteLine($"{hero.Name} blocked the attack!");
                    }
                    else
                    {
                        int enemyDamage = rng.Next(10, 30);
                        hero.TakeDamage(enemyDamage);
                    }

                    if (hero.Health <= 0)
                    {
                        Console.WriteLine($"\n{hero.Name} has fallen!");
                    }
                }

                Console.WriteLine($"\nThe battle is finished! Press any key to exit...");
            }

            else
            {
                Console.WriteLine("Error: No enemy selected.");
            }
        }
    }

    // attack information and health calculation

    public class Character
    {
        public string? Name;
        public int Health;

        public void Attack()
        {
            Console.WriteLine($"{Name} attacks!");
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health <= 0) Health = 0;
            Console.WriteLine($"{Name} takes {damage} damage. Remaining health: {Health}");
        }
    }
}