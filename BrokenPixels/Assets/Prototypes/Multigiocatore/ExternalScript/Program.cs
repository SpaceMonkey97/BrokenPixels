using System;

namespace PrototipoSocket
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Prototype Server/Client";

            Server.Start(4, 130);

            Console.ReadKey();
            PressToContinue();
        }

        public static void PressToContinue()
        {
            Console.Write("\nPremi per continuare...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
