using System;
using System.Net;
using System.Text;

namespace GalodaVelha
{
    class Program
    {
        static void Main(string[] args)
        {
            //Set Console Encoding to accept UTF8 characters
            Console.OutputEncoding = Encoding.UTF8;
            //Instantiate new GameManager object
            GameManager Game = new GameManager();
            //Call GameStart()
            Game.GameStart();

        }
    }
}
