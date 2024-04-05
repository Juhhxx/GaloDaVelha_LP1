using System;
using System.Net;

namespace GalodaVelha
{
    class Program
    {
        static void Main(string[] args)
        {
            
            

        }
        static private void ColoredText(string str, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(str);
            Console.ResetColor();
        }
    }
}
