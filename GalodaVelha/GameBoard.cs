using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalodaVelha
{
    public class GameBoard
    {
        string[,] board;
        string player = "testValue";
        int[] placeCoords;
        Piece placePiece;
        public GameBoard()
        {
            this.board = new string[4,4] {{" "," "," "," "},
                                          {" "," "," "," "},
                                          {" "," "," "," "},
                                          {" "," "," "," "}};
        }

        private Piece AskForPiece()
        {
            Console.Write($"{player} choose a piece:\n>");
            string pieceCode = Console.ReadLine();
            Console.WriteLine();

            Piece playerChoice = new Piece(pieceCode);

            if (!playerChoice.validity)
            {
                playerChoice = AskForPiece();
            }

            return playerChoice;
        }
       
    }
}