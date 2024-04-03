using System;

namespace GalodaVelha
{
    [Flags]
    enum PieceTraits
    {
        Size = 1 << 0, //if this trait is active then the piece is big,
                    // otherwise it is small
        Color = 1 << 1, //if this trait is active then the piece is light,
                    //otherwise it is dark
        Shape = 1 << 2, //if this trait is active then the piece is cubic,
                    //otherwise it is cilindrical
        Fill = 1 << 3,  //if this trait is active then the piece has a filling,
                    //otherwise it has a hole
        Validity = 1 << 4,  //if this trait is active then the piece is OffBoard
                    //otherwise it is OnBoard and invalid to be chosen



    }
}
