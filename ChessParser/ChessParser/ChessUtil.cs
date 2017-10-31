using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessParser
{
    public static class ChessUtil
    {
        public static int getSrcRow(String moveStr)
        {
            return getRow(moveStr[1]);
        }

        public static int getSrcCol(String moveStr)
        {
            return getCol(moveStr[0]);
        }

        public static int getDstRow(String moveStr)
        {
            return getRow(moveStr[3]);
        }

        public static int getDstCol(String moveStr)
        {
            return getCol(moveStr[2]);
        }

        public static int getRow(char rank)
        {
            int rowIndex = 8 - Int32.Parse(rank.ToString());
            if (rowIndex < 0 || rowIndex > 7)
            {
                throw new ArgumentOutOfRangeException("Invalid rank coordinate");
            }
            return rowIndex;
        }

        public static int getCol(char file)
        {
            int fileIndex = file - 'a';
            if (fileIndex < 0 || fileIndex > 7)
            {
                throw new ArgumentOutOfRangeException("Invalid file coordinate");
            }
            return fileIndex;
        }

        public static Boolean ValidRowColumnMove(int srcRow, int srcCol, int dstRow, int dstCol, ChessBoard chessBoard)
        {
            if (srcRow == dstRow)
            {
                // the move is a column move
                int leftIndex = (srcCol < dstCol) ? srcCol : dstCol;
                int rightIndex = (srcCol > dstCol) ? srcCol : dstCol;
                return legalRowMove(srcRow, leftIndex, rightIndex, chessBoard);
            }

            if (srcCol == dstCol)
            {
                // the move is a row move
                int lowerIndex = (srcRow < dstRow) ? srcRow : dstRow;
                int upperIndex = (srcRow > dstRow) ? srcRow : dstRow;
                return legalColMove(srcCol, lowerIndex, upperIndex, chessBoard);
            }
            return false;
        }

        private static Boolean legalRowMove(int row, int srcCol, int dstCol, ChessBoard chessBoard)
        {
            // see if there are any pieces along on the row that are in the way
            for (int cIndex = srcCol + 1; cIndex < dstCol; cIndex++)
            {
                if (chessBoard.getChessPiece(row, cIndex) != null)
                {
                    return false;
                }
            }
            return true;
        }

        private static Boolean legalColMove(int col, int srcRow, int dstRow, ChessBoard chessBoard)
        {
            // see if there are any pieces along the column that are in the way
            for (int rIndex = srcRow + 1; rIndex < dstRow; rIndex++)
            {
                if (chessBoard.getChessPiece(rIndex, col) != null)
                {
                    return false;
                }
            }
            return true;
        }

        public static Boolean ValidDiagonalMove(int srcRow, int srcCol, int dstRow, int dstCol, ChessBoard chessBoard)
        {
            // is this a diagonal move? If not, return false
            if (Math.Abs(dstRow - srcRow) != Math.Abs(dstCol - srcCol))
            {
                return false;
            }

            // see if there's any pieces in the way of this move
            int incX = Convert.ToInt32(((float)(dstCol - srcCol)) / (float)(Math.Abs(dstCol - srcCol)));
            int incY = Convert.ToInt32(((float)(dstRow - srcRow)) / (float)(Math.Abs(dstRow - srcRow)));

            for (int r = srcRow + incY, c = srcCol + incX; r != dstRow && c != dstCol; r += incY, c += incX)
            {
                if (chessBoard.getChessPiece(r, c) != null)
                {
                    return false;
                }
            }
            return true;
        }
    }
}

