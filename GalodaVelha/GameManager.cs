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
        }
        public void GameStart()
        {
            GameSetup();

            while (!mainGame.hasWin)
            {
                mainGame.AskForInputs(gameTurn);
                gameTurn += 1;
            }
        }
    }
}