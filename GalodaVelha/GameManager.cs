using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalodaVelha
{
    public class GameManager
    {
        int gameTurn;
        GameBoard mainGame;
        public GameManager()
        {
            gameTurn = 1;
        }
        private void GameSetup()
        {
            mainGame = new GameBoard();
            Piece.ResetPiecesArray();
        }
        public void GameStart()
        {
            GameSetup();

            while (!mainGame.hasWin)
            {
                mainGame.AskForInputs(gameTurn);
                gameTurn += 1;
            }

            PlayAgain();
        }
        public void PlayAgain()
        {
            Console.WriteLine();
            Console.Write("Do you want to play again ? (y/n)\n> ");
            string plrChoice = Console.ReadLine();

            switch (plrChoice)
            {
                case "y":
                    GameStart();
                    break;
                case "n":
                    break;
                default:
                    Console.WriteLine("\nInvalid answer. Try again,");
                    PlayAgain();
                    break;
            }
        }
    }
}