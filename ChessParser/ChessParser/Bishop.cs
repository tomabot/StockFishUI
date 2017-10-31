using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessParser
{
    public class Bishop : ChessPiece
    {
        public Bishop(Color c) : base(c) { }

        public override string ToString() {
            return (this.Color == Color.Black) ? "b" : "B";
        }

        public override Boolean LegalMove(int srcRow, int srcCol, int dstRow, int dstCol, ChessBoard chessBoard) {
            // if theres a piece of the same color on the destination square, return false
            if (DestinationChecker(dstRow, dstCol, chessBoard) == false)
            {
                return false;
            }

            // The destination square is either vacant, or occupied by a piece
            // of the other color. See if this is a valid diagonal move
            if(ChessUtil.ValidDiagonalMove(srcRow, srcCol, dstRow, dstCol, chessBoard) == true)
            { 
                // it's a valid diagonal move, move the piece on the board
                chessBoard.move(srcRow, srcCol, dstRow, dstCol);
                return true;
            }
            return false;
        }
    }

}
