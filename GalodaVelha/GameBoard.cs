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
            //Declare the infoBoard array
            this.infoBoard = new Piece[4,4];
            //Set hasWin as false
            this.hasWin = false;
        }
        /// <summary>
        /// Call AskForPiece() and AskForCoords() methods and insert the
        /// results to the board matrix.
        /// </summary>
        /// <param name="gameTurn">Integer game turn.</param>
        public void AskForInputs(int gameTurn)
        {
            string sep = "\n==============================================\n";
            Console.WriteLine(sep);
            Console.WriteLine($"Turn: {gameTurn}\n");
            //Check if gameTurn is 1
            if (gameTurn == 1)
            {
                //If true print a tutorial to the console
                ColoredText("\nThis game is played by two players. Starting with whoever the players choose, the 1st player in the round will choose a piece for the next player to play. The player that plays second chooses the coordinates of where they want to place the piece chosen by the 1st player. On the next turn, the player that played first the round before will now be playing last, switching roles with the previous 2nd player. Each piece has four traits (Size, Color, Shape and Fill), each one varying between two possibilities (big or tiny, light or dark, square or circular, filled or empty.\n",ConsoleColor.Yellow);
                ColoredText("\nThe player that is responsible of choosing which pice will be placed thatround must insert a code of 4 letters, referencing each specific trait of the chosen piece (e.x ' tlse ' for a tiny piece with a light color, square body and that has an empty body)\n",ConsoleColor.Yellow);
                ColoredText("\nThe player choosing the coordinates must insert a code of 2 characters - one letter, referencing the column and one number for the row (e.x ' B3 ' refers to the cell found by intersecting column 2 and row 3)\n",ConsoleColor.Yellow);
                ColoredText("\nThe game is won by the player that manages to place the piece that completes a row, column or diagonal where it is at least verified a common trait amongst the possible 4 between all 4 pieces.\n\n",ConsoleColor.Yellow);
            }

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
            //Update hasWin
            hasWin = CheckForGameWin(placeCoords);
            //Print game board
            PrintBoard();
            //Check hasWin
            if (hasWin)
            {
                //If true print message to the console
                Console.WriteLine(sep);
                ColoredText($"{WhoPlays(gameTurn)} has won !!!",ConsoleColor.Yellow);
                Console.WriteLine();
            }
            //Check if 16 turns have passed
            else if (gameTurn == 16)
            {
                //If true and hasWin is false print message to the console
                Console.WriteLine(sep);
                ColoredText("There is a draw between the 2 players.",ConsoleColor.Cyan);
                Console.WriteLine();
                hasWin = true;
            }
        }
        /// <summary>
        /// See what player is doing an action.
        /// </summary>
        /// <param name="gameTurn">Integer game turn.</param>
        /// <returns>String of who is playing.</returns>
        private string WhoPlays(int gameTurn)
        {
            //Initialize string variable
            string player;
            //If module of gameTurn/2 is 0
            if (gameTurn % 2 == 0)
            {
                //If true set player as Player 2
                player = "Player 2";
            }
            else
            {
                //If false set player as Player 1
                player = "Player 1";
            }
            //Return string variable
            return player;
        }
        /// <summary>
        /// Ask the adversary what piece they want to give to the player.
        /// </summary>
        /// <param name="gameTurn">Integer game turn.</param>
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
        /// <param name="gameTurn">Integer game turn.</param>
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
        /// <summary>
        /// Check if a player has won the game.
        /// </summary>
        /// <param name="lastCoords"></param>
        /// <returns></returns>
        private bool CheckForGameWin(int[] lastCoords)
        {
            //Initialize bool variable as false
            bool check = false;
            //Go for each PieceTrait
            for (int i = 0; i < 4; i++)
            {
                //Check if there was a win in the x axis of the lastCoord called
                check = CheckXWin(0,lastCoords[1],i);
                //If check is true break loop
                if (check) break;
                //Check if there was a win in the y axis of the lastCoord called
                check = CheckYWin(lastCoords[0],0,i);
                //If check is true break loop
                if (check) break;
                //Check if there was a win on the positive diagonal
                check = CheckDiagonalPosWin(0,0,i);
                //If check is true break loop
                if (check) break;
                //Check if there was a win on the negative diagonal
                check = CheckDiagonalNegWin(3,0,i);
                //If check is true break loop
                if (check) break;
            }
            //Return bool variable
            return check;
        }
        /// <summary>
        /// Check for a win in the X axis of the given coords.
        /// </summary>
        /// <param name="x">X coord.</param>
        /// <param name="y">Y coord.</param>
        /// <param name="traitCheck">Trait to be checked.</param>
        /// <returns>Bool value.</returns>
        private bool CheckXWin(int x, int y, int traitCheck)
        {
            //Initialize bool variable as true
            bool check = true;
            //Check if the current cell and next cell in the matrix are not null
            if (infoBoard[x,y] != null && infoBoard[x + 1,y] != null)
            {
                //If true check if the specifeid trait is equal among them
                if (infoBoard[x,y].GetTrait()[traitCheck] == infoBoard[x + 1,y].GetTrait()[traitCheck])
                {
                    //If true check if in the last cell
                    if (x < 2)
                    {
                        //If true call CheckXWin() with the next cell
                        check = CheckXWin(x + 1,y,traitCheck);
                    }
                }
                else
                {
                    //If false set check to false
                    check = false;
                }
            }
            else
            {
                //If false set check to false
                check = false;
            }
            //Return bool variable
            return check;
        }
        /// <summary>
        /// Check for a win in the Y axis of the given coords.
        /// </summary>
        /// <param name="x">X coord.</param>
        /// <param name="y">Y coord.</param>
        /// <param name="traitCheck">Trait to be checked.</param>
        /// <returns>Bool value.</returns>
        private bool CheckYWin(int x, int y, int traitCheck)
        {
            //Initialize bool variable as true
            bool check = true;
            //Check if the current cell and next cell in the matrix are not null
            if (infoBoard[x,y] != null && infoBoard[x,y + 1] != null)
            {
                //If true check if the specifeid trait is equal among them
                if (infoBoard[x,y].GetTrait()[traitCheck] == infoBoard[x,y + 1].GetTrait()[traitCheck])
                {
                    //If true check if in the last cell
                    if (y < 2)
                    {
                        //If true call CheckXWin() with the next cell
                        check = CheckYWin(x,y + 1,traitCheck);
                    }
                }
                else
                {
                    //If false set check to false
                    check = false;
                }
            }
            else
            {
                //If false set check to false
                check = false;
            }
            //Return bool variable
            return check;
        }
        /// <summary>
        /// Check for a win in the positive diagonal.
        /// </summary>
        /// <param name="x">X coord.</param>
        /// <param name="y">Y coord.</param>
        /// <param name="traitCheck">Trait to be checked.</param>
        /// <returns>Bool value.</returns>
        private bool CheckDiagonalPosWin(int x, int y, int traitCheck)
        {
            //Initialize bool variable as true
            bool check = true;
            //Check if the current cell and next cell in the matrix are not null
            if (infoBoard[x,y] != null && infoBoard[x + 1,y + 1] != null)
            {
                //If true check if the specifeid trait is equal among them
                if (infoBoard[x,y].GetTrait()[traitCheck] == infoBoard[x + 1,y + 1].GetTrait()[traitCheck])
                {
                    //If true check if in the last cell
                    if (x < 2 && y < 2)
                    {
                        //If true call CheckXWin() with the next cell
                        check = CheckDiagonalPosWin(x + 1,y + 1,traitCheck);
                    }
                }
                else
                {
                    //If false set check to false
                    check = false;
                }
            }
            else
            {
                //If false set check to false
                check = false;
            }
            //Return bool variable
            return check;
        }
        /// <summary>
        /// Check for a win in the negative diagonal.
        /// </summary>
        /// <param name="x">X coord.</param>
        /// <param name="y">Y coord.</param>
        /// <param name="traitCheck">Trait to be checked.</param>
        /// <returns>Bool value.</returns>
        private bool CheckDiagonalNegWin(int x, int y, int traitCheck)
        {
            //Initialize bool variable as true
            bool check = true;
            //Check if the current cell and next cell in the matrix are not null
            if (infoBoard[x,y] != null && infoBoard[x - 1,y + 1] != null)
            {
                //If true check if the specifeid trait is equal among them
                if (infoBoard[x,y].GetTrait()[traitCheck] == infoBoard[x - 1,y + 1].GetTrait()[traitCheck])
                {
                    //If true check if in the last cell
                    if (x > 0 && y < 2)
                    {
                        //If true call CheckXWin() with the next cell
                        check = CheckDiagonalNegWin(x - 1,y + 1,traitCheck);
                    }
                }
                else
                {
                    //If false set check to false
                    check = false;
                }
            }
            else
            {
                //If false set check to false
                check = false;
            }
            //Return bool variable
            return check;
        }

    }
}