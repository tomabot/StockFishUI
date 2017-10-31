using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessParser
{
    public abstract class Pawn : ChessPiece
    {
        public Pawn(Color c) : base(c) { }
    }
}
