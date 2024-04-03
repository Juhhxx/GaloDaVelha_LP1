using System;

namespace GalodaVelha
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "Test Print";
            ColoredText(s, ConsoleColor.Red);
            ColoredText(s, ConsoleColor.Green);
            ColoredText(s, ConsoleColor.Blue);
        }
        static private void ColoredText(string str, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(str);
            Console.ResetColor();
        }
    }
}
