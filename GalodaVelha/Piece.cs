using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace GalodaVelha
{
    /// <summary>
    /// Class that controls how pieces are created, work and their traits.
    /// </summary>
    public class Piece
    {
        //Intialize myPiece enum code
        PieceTraits myPiece;
        //Initialize newPiece enum code
        PieceTraits newPiece;
        //Initialize code string
        string code;
        //Initialize piece name representator
        string name;
        //Initialize piece color
        ConsoleColor color;
        //Initialize piece validity
        public bool validity;
        //Initialize static array piecesCreated that will keep track of
        //what pieces were created
        static string[] piecesCreated;
        //Initialize static int piecesCount that counts how many pieces were
        //created
        static int piecesCount;

        /// <summary>
        /// Constructor for Piece class.
        /// </summary>
        /// <param name="code">4 letter code that describes the piece.</param>
        public Piece(string code)
        {
            //Declare instance variables
            this.code = code;
            //Decode the given 4 letter string 
            this.newPiece = Decode(code);
            //Declare default value of validity as true
            validity = true;
            //Initialize piece code
            InitializePiece();
        }
        /// <summary>
        /// Static constructor for Piece class.
        /// </summary>
        static Piece()
        {
            //Initialize empty array
            piecesCreated = new string[16];
            //Initialize piecesCount as 0
            piecesCount = 0;
        }  
        /// <summary>
        /// Initialize piece code and verify piece validity.
        /// </summary>
        private void InitializePiece() 
        {
            //Get pieceInfo array
            string[] pcInfo = GetTrait();
            //Put info into a string
            string info = $"{pcInfo[0]},{pcInfo[1]},{pcInfo[2]},{pcInfo[3]}";
            //Check if code is bigger than 4 characters
            if (code.Length >= 5)
            {
                //If true print message to the console and set validity to false
                Console.WriteLine("Invalid code, too many characters.");
                Console.WriteLine("Please try again.\n");
                validity = false;
            }
            //Check if piece was marked as wrong
            else if ((newPiece & PieceTraits.Wrong) == PieceTraits.Wrong )
            {
                //If true print message to the console and set validity to false
                Console.WriteLine("Invalid code, non existant trait.");
                Console.WriteLine("Please try again.\n");
                validity = false;
            }
            //Check if piece is on the piecesCreated array
            else if (InArray(info))
            {
                //If true print message to the console and set validity to false
                Console.WriteLine("This piece was already placed.");
                Console.WriteLine("Please try again.\n");
                validity = false;
            }
            else
            {
                //If all is false declare myPiece value
                this.myPiece = newPiece;
                //Set piece name representator
                SetName();
                //Set piece color
                SetColor();
                //Insert piece into piecesCreated array
                piecesCreated[piecesCount] = info;
                //Add 1 to piecesCount
                piecesCount += 1;
            }
        }
        /// <summary>
        /// Decode the string given by the player.
        /// </summary>
        /// <param name="code">4 letter code in string type.</param>
        /// <returns>PieceTrait enum code that identifies the piece.</returns>
        private PieceTraits Decode(string code)
        {  
            //Go for each character in the given string
            foreach(char c in code)
            {
                //Get the given character
                switch (c)
                {
                    //Check if Size trait is active
                    case 'b':
                        //If true add it to newPice code
                        newPiece ^= PieceTraits.Size;
                        break;
                    //Check if Color trait is active
                    case 'l':
                        //If true add it to newPiece code
                        newPiece ^= PieceTraits.Color;
                        break;
                    //Check if Shape trait is active
                    case 's':
                        //If true add it to newPiece code
                        newPiece ^= PieceTraits.Shape;
                        break;
                    //Check if Fill trait is active
                    case 'f':
                        //If true add it to newPiece code
                        newPiece ^= PieceTraits.Fill;
                        break;

                    default:
                        //Check if character was one inactive trait
                        if (c == 't' || c == 'd' || c == 'c' || c == 'e') 
                        {
                            //If true continue
                            break;
                        }
                        else
                        {
                            //If false it means there is an invalid character
                            //Add Wrong trait to newPiece code
                            newPiece ^= PieceTraits.Wrong;
                            break;
                        }
                }
            }
            //Return newPiece enum
            return newPiece;
        }
        /// <summary>
        /// Check if the given piece was already placed.
        /// </summary>
        /// <param name="piece">Piece traits in string type.</param>
        /// <returns>True if the piece isn't placed in the board and false if
        /// otherwise.</returns>
        private bool InArray(string piece)
        {
            //Initialize bool type variable as false
            bool check = false;

            //Go for each value of piecesCreated array
            foreach(string p in piecesCreated)
            {
                //Check if given piece is equals to the array value
                if (piece == p)
                {
                    //If true change check variable to true
                    check = true;
                }
            }
            //Return bool variable
            return check;
        }
        /// <summary>
        /// Set piece instance name identifier.
        /// </summary>
        private void SetName()
        {
            //Intialize int variables as 0
            int size = 0;
            int shape = 0;
            int filled = 0;
            //Initialize a 3D array
            string[][][] allNames = new string[2][][];
            //Initialize big and tiny arrays
            allNames[0] = new string[2][]; // tinny
            allNames[1] = new string[2][]; // big
            //Initialize circle and square arrays
            allNames[0][0] = new string[2]; // tiny circle
            allNames[0][1] = new string[2]; // tiny square
            allNames[1][0] = new string[2]; // big circle
            allNames[1][1] = new string[2]; // big square
            //Initialize empty and filled arrays with the specified unicode
            //characters to represent the piece
            allNames[0][0][0] = "\u25E6"; // tiny circle empty
            allNames[0][0][1] = "\u2022"; // tiny circle filled
            allNames[0][1][0] = "\u25AB"; // tiny square empty
            allNames[0][1][1] = "\u25AA"; // tiny square filled
            allNames[1][0][0] = "\u25CB"; // big circle empty
            allNames[1][0][1] = "\u25CF"; // big circle filled
            allNames[1][1][0] = "\u25A1"; // big square empty
            allNames[1][1][1] = "\u25A0"; // big square filled
            //Check if myPiece Size trait is active
            if ((myPiece & PieceTraits.Size) == PieceTraits.Size)
            {
                //If true set size as 1
                size = 1;
            }
            //Check if myPiece Shape trait is active
            if ((myPiece & PieceTraits.Shape) == PieceTraits.Shape)
            {
                //If true set shape as 1
                shape = 1;
            }
            //Check if myPiece Fill trait is active
            if ((myPiece & PieceTraits.Fill) == PieceTraits.Fill)
            {
                //If true set filled as 1
                filled = 1;
            }
            //Set piece name as the specified unicode character based on the
            //integer variables
            name = allNames[size][shape][filled];
        }
        /// <summary>
        /// Set piece instance color.
        /// </summary>
        private void SetColor()
        {
            //Initialize ConsoleColor type variable
            ConsoleColor col;
            //Check if myPiece color trait is active
            if ((myPiece & PieceTraits.Color) == PieceTraits.Color)
            {
                //If true set color as ConsoleColor.Mangenta
                col = ConsoleColor.Magenta;
            }
            else
            {
                //If false set color as ConsoleColor.Red
                col = ConsoleColor.Red;
            }
            //Set piece color as col
            color = col;
        }
        /// <summary>
        /// Get piece instance traits in a string.
        /// </summary>
        /// <returns>String with all piece traits.</returns>
        public string[] GetTrait()
        {
            //Initialize string array
            string[] traits = new string[4];
            //Check Size trait and add the corret trait to array
            traits[0] = CheckForTrait(PieceTraits.Size,"big","tiny");
            //Check Color trait and add the corret trait to array
            traits[1] = CheckForTrait(PieceTraits.Color,"light","dark");
            //Check Shape trait and add the corret trait to array
            traits[2] = CheckForTrait(PieceTraits.Shape,"square","circle");
            //Check Fill trait and add the corret trait to array
            traits[3] = CheckForTrait(PieceTraits.Fill,"filled","empty");
            //Return string array
            return traits;
        }
        /// <summary>
        /// Get name identifier of the piece instance.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetName()
        {
            //Return string value
            return name;
        }
        /// <summary>
        /// Get color of piece instance.
        /// </summary>
        /// <returns>ConsoleColor type variable.</returns>
        public ConsoleColor GetColor()
        {
            //Return ConsoleColor
            return color;
        }
        /// <summary>
        /// Check for a soecific trait in a piece instance and return a proper
        /// string value.
        /// </summary>
        /// <param name="trait">PiceTrait to be evaluated.</param>
        /// <param name="res1">Result if trait is active.</param>
        /// <param name="res2">Result if trait is inactive.</param>
        /// <returns>String value describing the given trait.</returns>
        private string CheckForTrait(PieceTraits trait,string res1,string res2)
        {
            //Initialize string variable
            string traitName;
            //Check if piece has the given trait
            if ((newPiece & trait) == trait)
            {
                //If true change traitName string to res1
                traitName = res1;
            }
            else
            {
                //If false change traitName string to res2
                traitName = res2;
            }
            //Return string variable
            return traitName;
        }
        /// <summary>
        /// Reset the array containing all pieces that were created during 
        /// the game.
        /// </summary>
        public static void ResetPiecesArray()
        {
            //Initialize new piecesCreated array
            Piece.piecesCreated = new string[16];
        }
    }
}