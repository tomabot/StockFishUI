using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessParser
{
    public enum Color { Black, White };

    public abstract class ChessPiece {
        public Color Color { get; private set;  }


        public ChessPiece(Color c) {
            Color = c;
        }

        public abstract Boolean LegalMove(int srcRow, int srcCol, int dstRow, int dstCol, ChessBoard chessBoard);

        public Boolean DestinationChecker(int dstRow, int dstCol, ChessBoard chessBoard)
        {
            // If there's a piece on the destination square, see if it's 
            // the same color as the piece on the source square
            if (chessBoard.getChessPiece(dstRow, dstCol) != null)
            {
                if (this.Color == chessBoard.getChessPiece(dstRow, dstCol).Color)
                {
                    // there's a piece on the destination square and it's 
                    // the same color as the piece on the source square
                    return false;
                }
            }
            return true;
        }

        /*
        public Boolean DestinationChecker(int srcRow, int srcCol, int dstRow, int dstCol, ChessBoard chessBoard) {
            // If there's a piece on the destination square, see if it's 
            // the same color as the piece on the source square
            if (chessBoard.getChessPiece(dstRow, dstCol) != null)
            {
                if (chessBoard.getChessPiece(srcRow, srcCol).Color == chessBoard.getChessPiece(dstRow, dstCol).Color)
                {
                    // there's a piece on the destination square and it's 
                    // the same color as the piece on the source square
                    return false;
                }
            }
            return true;
        }
        */
    }
}
