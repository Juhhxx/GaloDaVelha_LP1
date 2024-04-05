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
    public class Piece
    {
        PieceTraits myPiece;
        string code;
        static string[] piecesCreated;
        PieceTraits newPiece;
        static int piecesCount;
        public bool validity = true;
        string name;
        public Piece(string code)
        {
            this.code = code;
            this.name = "A";
            this.newPiece = Decode(code);

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
        static Piece()
        {
            piecesCreated = new string[16];
            piecesCount = 0;
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