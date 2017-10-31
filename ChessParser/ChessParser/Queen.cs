using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessParser
{
    public class Queen : ChessPiece
    {
        public Queen(Color c) : base(c) { }

        public override string ToString() {
            return (this.Color == Color.Black) ? "q" : "Q";
        }

        public override Boolean LegalMove(int srcRow, int srcCol, int dstRow, int dstCol, ChessBoard chessBoard) {
            // if theres a piece of the same color on the destination square, return false
            if (DestinationChecker(dstRow, dstCol, chessBoard) == true)
            {
                if (ChessUtil.ValidRowColumnMove(srcRow, srcCol, dstRow, dstCol, chessBoard))
                {
                    chessBoard.move(srcRow, srcCol, dstRow, dstCol);
                    return true;
                }
                if (ChessUtil.ValidDiagonalMove(srcRow, srcCol, dstRow, dstCol, chessBoard) == true)
                {
                    chessBoard.move(srcRow, srcCol, dstRow, dstCol);
                    return true;
                }
            }

            return false;
        }
    }
}
