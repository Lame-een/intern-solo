using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//TODO
//better inventory system - items should be deep cloned(?) and more easily enchanted
//attacks should have references to items or potentionally some other C# magic?

namespace Day01
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World");

            GameController gc = new GameController();

            gc.InitGame();

            gc.GameLoop();

            Console.WriteLine("\n\nG A M E    O V E R\n\n");
            Console.WriteLine("Thank you for playing.\nPress any key to exit...\n");
            Console.Read();
        }
    }
}
