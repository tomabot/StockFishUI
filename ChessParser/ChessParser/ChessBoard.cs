using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessParser
{
    public class ChessBoard
    {
        private ChessPiece[][] board;

        public ChessBoard()
        {
            // allocate the array of rows
            board = new ChessPiece[8][];

            // allocate the columns
            board[0] = new ChessPiece[8] {
                new Rook(Color.Black),
                new Knight(Color.Black),
                new Bishop(Color.Black),
                new Queen(Color.Black),
                new King(Color.Black),
                new Bishop(Color.Black),
                new Knight(Color.Black),
                new Rook(Color.Black)
            };

            board[1] = new ChessPiece[8];
            for (int c = 0; c < 8; c++)
                board[1][c] = new BlackPawn();

            for (int r = 2; r < 6; r++)
            {
                board[r] = new ChessPiece[8];
                for (int c = 0; c < 8; c++)
                    board[r][c] = null;
            }

            board[6] = new ChessPiece[8];
            for (int c = 0; c < 8; c++)
                board[6][c] = new WhitePawn();

            board[7] = new ChessPiece[8] {
                new Rook(Color.White),
                new Knight(Color.White),
                new Bishop(Color.White),
                new Queen(Color.White),
                new King(Color.White),
                new Bishop(Color.White),
                new Knight(Color.White),
                new Rook(Color.White)
            };
        }

        public void move(int srcRow, int srcCol, int dstRow, int dstCol)
        {
            board[dstRow][dstCol] = board[srcRow][srcCol];
            board[srcRow][srcCol] = null;
        }

        public ChessPiece getChessPiece(int r, int c)
        {
            return board[r][c];
        }

        public void setChessPiece(int r, int c, ChessPiece cPiece)
        {
            board[r][c] = cPiece;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    sb.Append((board[row][col] == null) ? "." : board[row][col].ToString());
                }
                sb.Append("\n");
            }

            return sb.ToString();
        }
    }
}
