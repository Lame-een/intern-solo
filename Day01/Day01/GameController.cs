using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01
{
    class GameController
    {
        private bool _enemyEscaped = false;
        private Player _player;
        private PairUtility<Mob, Attack> _currentEnemy;

        private Item[] _itemList;
        private Attack[] _attackList;
        private PairUtility<Mob, Attack>[] _mobList;

        private void GenerateEnemy()
        {
            _currentEnemy = Globals.RandomArrayElement<PairUtility<Mob, Attack>>(_mobList).DeepClone();

            Console.WriteLine("As you're walking through the forest you encounter a {0}", _currentEnemy.First.Name);
            Console.WriteLine("Prepare to fight.\n");
        }
        private void NameInput()
        {
            while (true)
            {
                Console.Write("Player enter your name: ");

                string playerName = Console.ReadLine();
                Console.Write($"\"{playerName}\" is your name, is that right?\n[y/N] ");

                string confirmationString = Console.ReadLine().ToLower();
                if (confirmationString == "y")
                {
                    Console.WriteLine("Gotcha, your name is {0}.\n\n", playerName);
                    _player = new Player(playerName);
                    _player.Equip(_itemList[1]);
                    _player.Equip(_itemList[4]);
                    return;
                }

                Console.WriteLine("It's not? What's your actual name?");
            }
        }

        public void InitGame()
        {
            _itemList = new Item[]
                {
                    new Item("Dagger", ItemType.Weapon, 5, 0),
                    new Item("Short Sword", ItemType.Weapon, 10, 0),
                    new Item("Long Sword", ItemType.Weapon, 25, 0),
                    new Item("Club", ItemType.Weapon, 25, 0),
                    new Item("Basic Helmet", ItemType.Helmet, 0, 5)
                };

            Item.EnchantItem(ref _itemList[1]);

            _attackList = new Attack[]
                {
                    Globals.NullAttack,
                    new Attack("Bite", 10, 90),
                    new Attack("Strong Bite", 50, 80),
                    new Attack("Paw swipe", 5, 70),
                    new Attack(_itemList[0], 80),
                    new Attack(_itemList[1], 85),
                    new Attack(_itemList[2], 75),
                    new Attack(_itemList[3], 70),
                    new Attack("Punch", 5, 80)
                };
            _mobList = new PairUtility<Mob, Attack>[]
                {
                    new PairUtility<Mob, Attack>(new Mob("Sheep", 10, false), new List<Attack>{_attackList[0]}),
                    new PairUtility<Mob, Attack>(new Mob("Wolf", 50, true), new List<Attack>{_attackList[1],_attackList[3]}),
                    new PairUtility<Mob, Attack>(new Mob("Troll", 1000, true), new List<Attack>{_attackList[1],_attackList[2],_attackList[7] })
                };

            NameInput();

            GenerateEnemy();
        }
        public void GameLoop()
        {
            var randomGenerator = new Random();

            while (_player.Health > 0)
            {
                if ((_currentEnemy.First.Health <= 0) || (_enemyEscaped))
                {
                    GenerateEnemy();

                    if (randomGenerator.Next(0, 2) == 0)
                    {
                        _currentEnemy.First.DoAction(_currentEnemy.RandomSecond(), _player, ref _enemyEscaped);
                        Console.WriteLine();
                        Console.ReadKey();
                    }
                }
                if (_enemyEscaped)
                    continue;

                _player.DoAction(_attackList[5], _currentEnemy.First, ref _enemyEscaped);
                Console.WriteLine();
                Console.ReadKey();

                if (_currentEnemy.First.Health <= 0) continue;

                _currentEnemy.First.DoAction(_currentEnemy.RandomSecond(), _player, ref _enemyEscaped);
                Console.WriteLine();
                Console.ReadKey();

            }
        }
    }
}
