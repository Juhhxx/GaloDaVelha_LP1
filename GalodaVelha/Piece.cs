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
            this.name = "A";
            //Decode the given 4 letter string 
            this.newPiece = Decode(code);
            validity = true;

            InitializePiece();
        }
        static Piece()
        {
            piecesCreated = new string[16];
            piecesCount = 0;
        }  
        private void InitializePiece()
        {
            if (code.Length >= 5)
            {
                Console.WriteLine("Invalid code, too many characters.");
                Console.WriteLine("Please try again.\n");
                validity = false;
            }
            else if ((newPiece & PieceTraits.Wrong) == PieceTraits.Wrong )
            {
                Console.WriteLine("Invalid code, non existant trait.");
                Console.WriteLine("Please try again.\n");
                validity = false;
            }
            else if(InArray(GetTrait()))
            {
                Console.WriteLine("This piece was already placed.");
                Console.WriteLine("Please try again.\n");
                validity = false;
            }
            else
            {
                this.myPiece = newPiece;
                piecesCreated[piecesCount] = GetTrait();
                piecesCount += 1;
            }
        }
        private PieceTraits Decode(string code)
        {  
            foreach(char c in code)
            {
                switch (c)
                {
                    case 'b':
                        newPiece ^= PieceTraits.Size;
                        break;

                    case 'l':
                        newPiece ^= PieceTraits.Color;
                        break;

                    case 's':
                        newPiece ^= PieceTraits.Shape;
                        break;

                    case 'f':
                        newPiece ^= PieceTraits.Fill;
                        break;

                    default:
                        if (c == 't' || c == 'd' || c == 'c' || c == 'e') 
                        {
                            break;
                        }
                        else
                        {
                            newPiece ^= PieceTraits.Wrong;
                            break;
                        }
                }
            }
            return newPiece;
        }
        private bool InArray(string piece)
        {
            bool check = false;
            foreach(string p in piecesCreated)
            {
                Console.WriteLine(p);
                if (piece == p)
                {
                    check = true;
                }
            }
            Console.WriteLine($"InArray - Piece Code: {newPiece} Result: {check}");
            return check;
        }
        public string GetTrait()
        {
            string traits = "";

            traits += CheckForTrait(PieceTraits.Size,"big ","tiny ");
            traits += CheckForTrait(PieceTraits.Color,"light ","dark ");
            traits += CheckForTrait(PieceTraits.Shape,"square ","circle ");
            traits += CheckForTrait(PieceTraits.Fill,"filled ","empty ");
            
            return traits;
        }
        public bool GetInArray()
        {
            return InArray(GetTrait());
        }
        public string GetName()
        {
            return name;
        }
        private string CheckForTrait(PieceTraits trait,string res1,string res2)
        {
            string traitName;

            if ((myPiece & trait) == trait)
            {
                traitName = res1;
            }
            else
            {
                traitName = res2;
            }

            return traitName;
        }
    }
}