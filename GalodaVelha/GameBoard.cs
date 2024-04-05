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
       
    }
}