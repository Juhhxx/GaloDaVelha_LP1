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
        PieceTraits myPiece = 0;
        string code;
        static PieceTraits[] piecesCreated; //= new PieceTraits[16];
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
            else if(InArray(newPiece))
            {
                Console.WriteLine("This piece was already placed.");
                Console.WriteLine("Please try again.\n");
                validity = false;
            }
            else
            {
                this.myPiece = newPiece;
                piecesCreated[piecesCount] = myPiece;
                piecesCount += 1;
            }

        }
        static Piece()
        {
            piecesCreated = new PieceTraits[16];
            piecesCount = 0;
        }  
        private PieceTraits Decode(string code)
        {  
            foreach(char c in code)
            {
                switch (c)
                {
                    case 'b':
                        myPiece ^= PieceTraits.Size;
                        break;

                    case 'l':
                        myPiece ^= PieceTraits.Color;
                        break;

                    case 's':
                        myPiece ^= PieceTraits.Shape;
                        break;

                    case 'f':
                        myPiece ^= PieceTraits.Fill;
                        break;

                    default:
                        if (c == 't' || c == 'd' || c == 'c' || c == 'e') 
                        {
                            break;
                        }
                        else
                        {
                            myPiece ^= PieceTraits.Wrong;
                            break;
                        }
                }
            }
            return myPiece;
        }
        private bool InArray(PieceTraits myPiece)
        {
            bool check = false;
            foreach(PieceTraits p in piecesCreated)
            {
                if (myPiece == p)
                {
                    check = true;
                }
            }
            return check;
        }
        public string GetTrait()
        {
            string check = "";

            check += CheckForTrait(PieceTraits.Size,"big ","tiny ");
            check += CheckForTrait(PieceTraits.Color,"light ","dark ");
            check += CheckForTrait(PieceTraits.Shape,"cubic  ","spherical ");
            check += CheckForTrait(PieceTraits.Fill,"filled ","empty ");
            
            return check;
        }
        public bool GetInArray()
        {
            return InArray(this.myPiece);
        }
        public string GetName()
        {
            return name;
        }
        private string CheckForTrait(PieceTraits trait,string result1,string result2)
        {
            string traitName = "";

            if ((myPiece & trait) == trait)
            {
                traitName = result1;
            }
            else
            {
                traitName = result2;
            }

            return traitName;
        }
    }
}