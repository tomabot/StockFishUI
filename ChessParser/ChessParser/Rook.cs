using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessParser
{
    public class Rook : ChessPiece
    {
        public Boolean HasMoved { get; set; }

        public Rook(Color c) : base(c) { HasMoved = false; }

        public override string ToString() {
            return ( this.Color == Color.Black ) ? "r" : "R";
        }

        public override Boolean LegalMove(int srcRow, int srcCol, int dstRow, int dstCol, ChessBoard chessBoard)
        {
            // if theres a piece of the same color on the destination square, return false
            if (DestinationChecker(dstRow, dstCol, chessBoard) == true)
            {
                if (ChessUtil.ValidRowColumnMove(srcRow, srcCol, dstRow, dstCol, chessBoard) == true)
                {
                    chessBoard.move(srcRow, srcCol, dstRow, dstCol);
                    HasMoved = true;
                    return true;
                }
            }

            return false;
        }
    }
}
