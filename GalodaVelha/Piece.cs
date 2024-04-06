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
        //Initialize piece name
        string name;
        //Initialize piece validity
        public bool validity;
        //Initialize static array piecesCreated that will keep track of
        //what pieces were created
        public static string[] piecesCreated;
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
            this.name = "A";
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
                //Insert piece into piecesCreated array
                piecesCreated[piecesCount] = info;
                //Add 1 to piecesCount
                piecesCount += 1;

                // Console.WriteLine($"{GetTrait()[0]},{GetTrait()[1]},{GetTrait()[2]},{GetTrait()[3]}");
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
                // Console.WriteLine($"{p} = {piece} ?\nResult: {check}");
            }
            //TESTPRINT: Print newPiece code and check result
            // Console.WriteLine($"InArray - Piece Code: {newPiece} Result: {check}");
            //Return bool variable
            return check;
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
        /// Get InArray() value from outside the class.
        /// </summary>
        /// <returns>Bool value.</returns>
        public bool GetInArray()
        {
            //Get pieceInfo array
            string[] pcInfo = GetTrait();
            //Return bool value
            return InArray($"{pcInfo[0]},{pcInfo[1]},{pcInfo[2]},{pcInfo[3]}");
        }
        /// <summary>
        /// Get name identifier of the piece instance.
        /// </summary>
        /// <returns>String value.</returns>
        public string GetName()
        {
            //Return string value.
            return name;
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
        public static void ResetPiecesArray()
        {
            Piece.piecesCreated = new string[16];
        }
    }
}