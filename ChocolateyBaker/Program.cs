using System;
using System.Diagnostics;
using System.IO;

namespace ChocolateyBaker
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("This program requires arguments in order to function properly.");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(2);
            }
        }
    }

}

