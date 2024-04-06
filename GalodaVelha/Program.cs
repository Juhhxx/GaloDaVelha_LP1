using System;
using System.Net;
using System.Text;

namespace GalodaVelha
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            char a = '\u25A0';
            char b = '\u25A1';
            char c = '\u25AA';
            char d = '\u25AB';
            char e = '\u25CF';
            char f = '\u25CB';
            char g = '\u2022';
            char h = '\u25E6';

            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.WriteLine(c);
            Console.WriteLine(d);
            Console.WriteLine(e);
            Console.WriteLine(f);
            Console.WriteLine(g);
            Console.WriteLine(h);

            GameManager Game = new GameManager();

            Game.GameStart();

        }
    }
}
