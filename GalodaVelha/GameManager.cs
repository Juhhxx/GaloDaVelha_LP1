using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalodaVelha
{
    /// <summary>
    /// Class that manages the game loop.
    /// </summary>
    public class GameManager
    {
        //Initialize gameTurn
        int gameTurn;
        //Initialize mainGame
        GameBoard mainGame;
        /// <summary>
        /// Constructor for GameManager class.
        /// </summary>
        public GameManager()
        {
            //Declare gameTurn as 1
            gameTurn = 1;
        }
        /// <summary>
        /// Set up a new game.
        /// </summary>
        private void GameSetup()
        {
            //Create instance of a GameBoard class
            mainGame = new GameBoard();
            //Reset the Pieces array
            Piece.ResetPiecesArray();
        }
        /// <summary>
        /// Start a new game.
        /// </summary>
        public void GameStart()
        {
            //Call GameSetup()
            GameSetup();
            //Start main game loop while hasWin in not true
            while (!mainGame.hasWin)
            {
                //Call AskForInputs with gameTurn as a argument
                mainGame.AskForInputs(gameTurn);
                //Increment gameTurn by 1
                gameTurn += 1;
            }
            //Call PlayAgain()
            PlayAgain();
        }
        /// <summary>
        /// Ask if the user want's to play again.
        /// </summary>
        public void PlayAgain()
        {
            //Write a message to the console
            Console.WriteLine();
            Console.Write("Do you want to play again ? (y/n)\n> ");
            //Get input from player
            string plrChoice = Console.ReadLine();
            //Switch case with pltChoice
            switch (plrChoice)
            {
                //If "y" call GameStart()
                case "y":
                    GameStart();
                    break;
                //If "n" continue
                case "n":
                    break;
                //If any other input print message to the console
                default:
                    Console.WriteLine("\nInvalid answer. Try again,");
                    PlayAgain();
                    break;
            }
        }
    }
}