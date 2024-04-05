using System;

namespace GalodaVelha
{
    [Flags]
    public enum PieceTraits
    {
        Size = 1 << 0, //if this trait is active then the piece is big,
                    // otherwise it is tiny
        Color = 1 << 1, //if this trait is active then the piece is light,
                    //otherwise it is dark
        Shape = 1 << 2, //if this trait is active then the piece is square,
                    //otherwise it is circular
        Fill = 1 << 3,  //if this trait is active then the piece has a filling,
                    //otherwise it is empty
        Wrong = 1 << 4 

    }
}