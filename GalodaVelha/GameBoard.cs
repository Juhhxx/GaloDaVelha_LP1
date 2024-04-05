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
        private int[] AskForCoords()
        {
            int[] coords = new int[2];

            Console.Write($"{player} choose a coordinate to place:\n>");
            string coordsInput = Console.ReadLine();
            Console.WriteLine();

            string coordX = coordsInput[0].ToString();
            string coordY = coordsInput[1].ToString();

            if (coordsInput.Length == 2)
            {
                if (CheckCoordInRange(coordX) & int.Parse(coordY) <= 4)
                {
                    coords[0] = (int)Enum.Parse(typeof(XCoords),coordX) - 1;
                    coords[1] = int.Parse(coordY) - 1;

                    if (!CoordIsEmpty(coords))
                    {
                        Console.WriteLine("Invalid coord, already occupied!");
                        Console.WriteLine("Please try again.\n");
                        coords = AskForCoords();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid coord, out of board range!");
                    Console.WriteLine("Please try again.\n");
                    coords = AskForCoords();
                }
            }
            else
            {
                Console.WriteLine("Invalid coord, more than 2 values given!");
                Console.WriteLine("Please try again.\n");
                coords = AskForCoords();
            }

            return coords;
        }
        private bool CheckCoordInRange(string coord)
        {
            bool presence = false;

            foreach (string letter in Enum.GetNames(typeof(XCoords)))
            {
                if (coord == letter)
                {
                    presence = true;
                }
            }
            return presence;
        }
        
        private bool CoordIsEmpty(int[] coord)
        {
            bool check = false;

            if (board[coord[0],coord[1]] == " ")
            {
                check = true;
            }
            return check;
        }
       
    }
}