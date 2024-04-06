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

            GameManager Game = new GameManager();

            Game.GameStart();

        }
    }
}
