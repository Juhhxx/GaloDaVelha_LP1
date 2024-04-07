using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalodaVelha
{
    /// <summary>
    /// Class that controls the game board and how the players will interact
    /// with it.
    /// </summary>
    public class GameBoard
    {
        //Initialize board array
        string[,] board;
        //Initialize infoBoard array
        Piece[,] infoBoard;
        //Initialize public variable hasWin
        public bool hasWin;
        
        /// <summary>
        /// Constructor for GameBoard class.
        /// </summary>
        public GameBoard()
        {
            //Declare the array as "empty", only blank spaces
            this.board = new string[4,4] {{" "," "," "," "},
                                          {" "," "," "," "},
                                          {" "," "," "," "},
                                          {" "," "," "," "}};
            this.infoBoard = new Piece[4,4];
            this.hasWin = false;
        }
        /// <summary>
        /// Call AskForPiece() and AskForCoords() methods and insert the
        /// results to the board matrix.
        /// </summary>
        public void AskForInputs(int gameTurn)
        {
            string sep = "\n==============================================\n";
            Console.WriteLine(sep);
            Console.WriteLine($"Turn: {gameTurn}\n");
            //Get what piece the adversary wants to be played
            Piece placePiece = AskForPiece(gameTurn); 
            //Get the coordinates where the player wants to put the piece
            int[] placeCoords = AskForCoords(gameTurn);
            //Insert the piece name into the board matrix with the 
            //specified coords
            board[placeCoords[0],placeCoords[1]] = placePiece.GetName();
            //Insert the piece into the board matrix with the specified coords
            infoBoard[placeCoords[0],placeCoords[1]] = placePiece;

            Console.WriteLine(sep);

            hasWin = CheckForGameWin(placeCoords);
            PrintBoard();

            if (hasWin)
            {
                Console.WriteLine(sep);
                ColoredText($"{WhoPlays(gameTurn)} has won !!!",ConsoleColor.Yellow);
                Console.WriteLine();
            }
            else if (gameTurn == 16)
            {
                Console.WriteLine(sep);
                ColoredText("There is a draw between the 2 players.",ConsoleColor.Cyan);
                Console.WriteLine();
                hasWin = true;
            }
        }
        private string WhoPlays(int gameTurn)
        {
            string player;
            if (gameTurn % 2 == 0)
            {
                player = "Player 2";
            }
            else
            {
                player = "Player 1";
            }

            return player;
        }
        /// <summary>
        /// Ask the adversary what piece they want to give to the player.
        /// </summary>
        /// <returns>Piece class instance with the specified traits.</returns>
        private Piece AskForPiece(int turn)
        {
            //Ask the adversary for a piece
            ColoredText(WhoPlays(turn + 1),ConsoleColor.Blue);
            Console.Write($" choose a piece:\n> ");
            string pieceCode = Console.ReadLine();
            Console.WriteLine();

            //Instanciate a new piece with the code given by the adversary
            Piece playerChoice = new Piece(pieceCode);

            //Check if the choosen piece is valid
            if (!playerChoice.validity)
            {
                //If false call AskForPiece() again
                playerChoice = AskForPiece(turn);
            }

            //Return the specified piece
            return playerChoice;
        }
        /// <summary>
        /// Ask the player for the coordiantes where he wants to insert the 
        /// given piece.
        /// </summary>
        /// <returns>Array of integers with the XY coordinates.</returns>
        private int[] AskForCoords(int turn)
        {
            //Initialize an integer array to store the coords
            int[] coords = new int[2];

            //Ask the player for the coordinates
            ColoredText(WhoPlays(turn),ConsoleColor.Blue);
            Console.Write($" choose a coordinate to place:\n> ");
            string coordsInput = Console.ReadLine();
            Console.WriteLine();

            //Split the string into two char values and convert
            //them to strings
            string coordX = coordsInput[0].ToString(); 
            string coordY = coordsInput[1].ToString();

            //Check if the player input has the right length
            //It can only accept two values
            if (coordsInput.Length == 2)
            {
                //If true check if the given values are within the values of
                //the 4x4 board matrix
                if (CheckCoordInRange(coordX) & int.Parse(coordY) <= 4)
                {
                    //If true convert the values into integers and insert
                    //them to the array
                    coords[0] = (int)Enum.Parse(typeof(XCoords),coordX) - 1;
                    coords[1] = int.Parse(coordY) - 1;

                    //Check if the specified coords are empty inside the board
                    //matrix
                    if (!CoordIsEmpty(coords))
                    {
                        //If false print a message to the console and
                        //call AskForCoords() again
                        Console.WriteLine("Invalid coord, already occupied!");
                        Console.WriteLine("Please try again.\n");
                        coords = AskForCoords(turn);
                    }
                }
                else
                {
                    //If false print a message to the console and
                    //call AskForCoords() again
                    Console.WriteLine("Invalid coord, out of board range!");
                    Console.WriteLine("Please try again.\n");
                    coords = AskForCoords(turn);
                }
            }
            else
            {
                //If false print a message to the console and
                //call AskForCoords() again
                Console.WriteLine("Invalid coord, more than 2 values given!");
                Console.WriteLine("Please try again.\n");
                coords = AskForCoords(turn);
            }

            //Return the integer array with the XY coordinates
            return coords;
        }
        /// <summary>
        /// Check if the given X coordinate is in range of the game board.
        /// </summary>
        /// <param name="coord">Type string coordinate.</param>
        /// <returns>True if the coordinate is in range and false
        /// if otherwise.</returns>
        private bool CheckCoordInRange(string coord)
        {
            //Intialize a bool type variable as false
            bool check = false;

            //Go for each value of the XCoord enum and get their name
            foreach (string letter in Enum.GetNames(typeof(XCoords)))
            {
                //Check if the name is equal to the coord
                if (coord == letter)
                {
                    //If true change check variable to true
                    check = true;
                }
            }
            //Return bool variable
            return check;
        }
        /// <summary>
        /// Check if the given coordinate in the board matrix is empty.
        /// </summary>
        /// <param name="coord">Integer array with the XY coordinates.</param>
        /// <returns>True if the given coordinate is empty and false 
        /// if it has a value.</returns>
        private bool CoordIsEmpty(int[] coord)
        {
            //Initialize bool type varaible as false
            bool check = false;

            //Check if the given position of the matrix is empty
            if (board[coord[0],coord[1]] == " ")
            {
                //If true change check variable to true
                check = true;
            }
            //Return bool type variable
            return check;
        }
        /// <summary>
        /// Print the game board to the console.
        /// </summary>
        public void PrintBoard()
        {
            string separator = "   +---+---+---+---+";

            ColoredText("     A   B   C   D\n",ConsoleColor.Yellow);
            Console.WriteLine(separator);
            ColoredText(" 1",ConsoleColor.Green);
            Console.Write(" | ");
            for (int i = 0; i < 4; i++)
            {
                if (infoBoard[i,0] != null)
                {
                    ColoredText($"{board[i,0]}",infoBoard[i,0].GetColor());
                }
                else
                {
                    Console.Write(" ");
                }
                
                Console.Write(" | ");
            }
            Console.WriteLine();
            Console.WriteLine(separator);
            ColoredText(" 2",ConsoleColor.Green);
            Console.Write(" | ");
            for (int i = 0; i < 4; i++)
            {
                if (infoBoard[i,1] != null)
                {
                    ColoredText($"{board[i,1]}",infoBoard[i,1].GetColor());
                }
                else
                {
                    Console.Write(" ");
                }
                Console.Write(" | ");
            }
            Console.WriteLine();
            Console.WriteLine(separator);
            ColoredText(" 3",ConsoleColor.Green);
            Console.Write(" | ");
            for (int i = 0; i < 4; i++)
            {
                if (infoBoard[i,2] != null)
                {
                    ColoredText($"{board[i,2]}",infoBoard[i,2].GetColor());
                }
                else
                {
                    Console.Write(" ");
                }
                Console.Write(" | ");
            }
            Console.WriteLine();
            Console.WriteLine(separator);
            ColoredText(" 4",ConsoleColor.Green);
            Console.Write(" | ");
            for (int i = 0; i < 4; i++)
            {
                if (infoBoard[i,3] != null)
                {
                    ColoredText($"{board[i,3]}",infoBoard[i,3].GetColor());
                }
                else
                {
                    Console.Write(" ");
                };
                Console.Write(" | ");
            }
            Console.WriteLine();
            Console.WriteLine(separator);
        }
        /// <summary>
        /// Print colored text to the console.
        /// </summary>
        /// <param name="str">String to print.</param>
        /// <param name="color">Color to print in.</param>
        private void ColoredText(string str, ConsoleColor color)
        {
            //Change console foreground color
            Console.ForegroundColor = color;
            //Print given string to the console
            Console.Write(str);
            //Reset console color
            Console.ResetColor();
        }
        private bool CheckForGameWin(int[] lastCoords)
        {
            bool check = false;

            for (int i = 0; i < 4; i++)
            {
                check = CheckXWin(0,lastCoords[1],i);
                if (check) break;
                check = CheckYWin(lastCoords[0],0,i);
                if (check) break;
                check = CheckDiagonalPosWin(0,0,i);
                if (check) break;
                check = CheckDiagonalNegWin(3,0,i);
                if (check) break;
            }

            return check;
        }
        private bool CheckXWin(int x, int y, int traitCheck)
        {
            bool check = true;

            if (infoBoard[x,y] != null && infoBoard[x + 1,y] != null)
            {
                if (infoBoard[x,y].GetTrait()[traitCheck] == infoBoard[x + 1,y].GetTrait()[traitCheck])
                {
                    if (x < 2)
                    {
                        check = CheckXWin(x + 1,y,traitCheck);
                    }
                }
                else
                {
                    check = false;
                }
            }
            else
            {
                check = false;
            }
            return check;
        }
        private bool CheckYWin(int x, int y, int traitCheck)
        {
            bool check = true;

            if (infoBoard[x,y] != null && infoBoard[x,y + 1] != null)
            {
                if (infoBoard[x,y].GetTrait()[traitCheck] == infoBoard[x,y + 1].GetTrait()[traitCheck])
                {
                    if (y < 2)
                    {
                        check = CheckYWin(x,y + 1,traitCheck);
                    }
                }
                else
                {
                    check = false;
                }
            }
            else
            {
                check = false;
            }

            return check;
        }
        private bool CheckDiagonalPosWin(int x, int y, int traitCheck)
        {
            bool check = true;

            if (infoBoard[x,y] != null && infoBoard[x + 1,y + 1] != null)
            {
                if (infoBoard[x,y].GetTrait()[traitCheck] == infoBoard[x + 1,y + 1].GetTrait()[traitCheck])
                {
                    if (x < 2 && y < 2)
                    {
                        check = CheckDiagonalPosWin(x + 1,y + 1,traitCheck);
                    }
                }
                else
                {
                    check = false;
                }
            }
            else
            {
                check = false;
            }
            return check;
        }
        private bool CheckDiagonalNegWin(int x, int y, int traitCheck)
        {
            bool check = true;

            if (infoBoard[x,y] != null && infoBoard[x - 1,y + 1] != null)
            {
                if (infoBoard[x,y].GetTrait()[traitCheck] == infoBoard[x - 1,y + 1].GetTrait()[traitCheck])
                {
                    if (x > 0 && y < 2)
                    {
                        check = CheckDiagonalNegWin(x - 1,y + 1,traitCheck);
                    }
                }
                else
                {
                    check = false;
                }
            }
            else
            {
                check = false;
            }
            return check;
        }

    }
}