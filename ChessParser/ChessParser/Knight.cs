using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessParser
{
    public class Knight : ChessPiece
    {
        public Knight(Color c) : base(c) { }

        public override string ToString() {
            return (this.Color == Color.Black) ? "n" : "N";
        }

        public override Boolean LegalMove(int srcRow, int srcCol, int dstRow, int dstCol, ChessBoard chessBoard) {
            // if theres a piece of the same color on the destination square, return false
            if (DestinationChecker(dstRow, dstCol, chessBoard) == false)
            {
                return false;
            }

            if (
                ((Math.Abs(srcCol - dstCol) == 1) && (Math.Abs(srcRow - dstRow) == 2)) ||
                ((Math.Abs(srcCol - dstCol) == 2) && (Math.Abs(srcRow - dstRow) == 1))
            ) {
                chessBoard.move(srcRow, srcCol, dstRow, dstCol);
                return true;
            }

            return false;
        }
    }
}
