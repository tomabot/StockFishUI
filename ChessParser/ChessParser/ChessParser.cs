using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessParser
{
    class ChessParser
    {
        public ChessBoard chessBoard;

        public ChessParser()
        {
            chessBoard = new ChessBoard();
        }

        public Boolean LegalMove(String moveStr)
        {
            // extract the source row/column from 
            // the move string 
            int srcRow = ChessUtil.getSrcRow(moveStr);
            int srcCol = ChessUtil.getSrcCol(moveStr);

            ChessPiece chessPiece = chessBoard.getChessPiece(srcRow, srcCol);

            // if the source square is empty, bad move
            if (chessPiece == null)
            {
                return false;
            }

            // extract the destination row/column 
            // from the move string
            int dstRow = ChessUtil.getDstRow(moveStr);
            int dstCol = ChessUtil.getDstCol(moveStr);

            // see if the chess piece says it's a legal move
            if (chessPiece.LegalMove(srcRow, srcCol, dstRow, dstCol, chessBoard) == false)
            {
                return false;   // illegal move
            }

            return true;
        }

        public override string ToString()
        {
            return chessBoard.ToString();
        }

    }
}
