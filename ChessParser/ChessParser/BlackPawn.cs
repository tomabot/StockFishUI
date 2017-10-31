using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessParser {
    public class BlackPawn : Pawn {
        public BlackPawn() : base(Color.Black) { }

        public override Boolean LegalMove(int srcRow, int srcCol, int dstRow, int dstCol, ChessBoard chessBoard) {
            if (srcRow == 1)
                return CheckBlackPawnFirstMove(srcRow, srcCol, dstRow, dstCol, chessBoard);
            return CheckBlackPawnNotFirstMove(srcRow, srcCol, dstRow, dstCol, chessBoard);
        }

        private bool CheckBlackPawnFirstMove(int srcRow, int srcCol, int dstRow, int dstCol, ChessBoard chessBoard) {
            if (srcCol == dstCol) {
                // black pawn moving along same column
                if ((dstRow == 2) || (dstRow == 3)) {
                    // black pawn moving along same column to row 2 or 3
                    if (chessBoard.getChessPiece(dstRow, dstCol) == null) {
                        // black pawn moving along same column to row 2 or 3 to an unoccupied square
                        chessBoard.move(srcRow, srcCol, dstRow, dstCol);
                        return true;

                    } else {
                        // black pawn moving along same column to row 2 or 3 to an occupied square
                        return false;
                    }

                } else {
                    // black pawn trying to move zero rows, or more than two rows along the same column
                    return false;
                }

            } else if (((dstCol + 1) == srcCol) || ((dstCol - 1) == srcCol)) {
                // black pawn moving to adjacent column
                if (dstRow == 2) {
                    // black pawn moving to an adjacent column, down one row
                    if (chessBoard.getChessPiece(dstRow, dstCol) != null) {
                        // black pawn moving to adjacent column, down one row, to an occupied square
                        if (chessBoard.getChessPiece(dstRow, dstCol).Color == Color.White)
                        {
                            // black pawn moving to adjacent column, down one row, to a square occupied by a white piece
                            chessBoard.move(srcRow, srcCol, dstRow, dstCol);
                            return true;
                        } else {
                            // black pawn moving to adjacent column, down one row, to a square occupied by a black piece
                            return false;
                        }

                    } else {
                        // black pawn moving to an adjacent column, down one row to an unoccupied square 
                        return false;
                    }

                } else {
                    // black pawn moving to an ajacent column, to a row other than row 2
                    return false;
                }

            } else {
                // no idea what this move was supposed to be doing
                return false;
            }
        }
        private bool CheckBlackPawnNotFirstMove(int srcRow, int srcCol, int dstRow, int dstCol, ChessBoard chessBoard) {
            if (srcCol == dstCol) {
                // black pawn moving along same column
                if ((srcRow + 1) == dstRow) {
                    // black pawn moving along the same column, down one row
                    if (chessBoard.getChessPiece(dstRow, dstCol) == null) {
                        // black pawn moving along the same column, down one row to an unoccupied square	
                        chessBoard.move(srcRow, srcCol, dstRow, dstCol);
                        return true;
                    } else {
                        // black pawn moving along the same column, down one row to occupied square
                        return false; 	// pawns have to attack diagonally
                    }
                } else {
                    return false;	// after the first move, pawns can only move one row at a time
                }

            } else if (((dstCol - 1) == srcCol) || ((dstCol + 1) == srcCol)) {
                // black pawn moving to an adjacent column
                if ((srcRow + 1) == dstRow) {
                    // black pawn moving to an adjacent column, down one row 
                    if (chessBoard.getChessPiece(dstRow, dstCol) != null) {
                        // black pawn moving to an adjacent column, down one row, to an occupied square
                        if (chessBoard.getChessPiece(dstRow, dstCol).Color == Color.White) {
                            // black pawn moving to an adjacent column, down one row, to a square occupied by a white piece
                            chessBoard.move(srcRow, srcCol, dstRow, dstCol);
                            return true;
                        } else {
                            // black pawn moving to an adjacent column, down one row, to a square occupied by a black piece
                            return false;
                        }
                    } else {
                        // black pawn moving to an adjacent column, down one row, to an unoccupied square
                        return false;
                    }
                } else {
                    // black pawn moving to an adjacent column, zero rows, or more than one row
                    return false;	// unless it's the first move, pawns can only advance one row at a time
                }
            } else {
                // completely invalid move
                return false;
            }
        }

        public override string ToString()
        {
            return "p";
        }
    }
}
