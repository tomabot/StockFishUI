using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessParser {
    public class WhitePawn : Pawn {
        public WhitePawn() : base(Color.White) { }

        public override Boolean LegalMove(int srcRow, int srcCol, int dstRow, int dstCol, ChessBoard chessBoard) {
            if (srcRow == 6)
                return checkWhitePawnFirstMove(srcRow, srcCol, dstRow, dstCol, chessBoard);

            return checkWhitePawnNotFirstMove(srcRow, srcCol, dstRow, dstCol, chessBoard);
        }

        private bool checkWhitePawnFirstMove(int srcRow, int srcCol, int dstRow, int dstCol, ChessBoard chessBoard) {
            if (srcCol == dstCol) {
                // white pawn moving along same column
                if ((dstRow == 4) || (dstRow == 5)) {
                    // white pawn moving along same column to row 4 or 5
                    if( chessBoard.getChessPiece(dstRow, dstCol) == null)  {
                        // white pawn moving to row 4 or 5, along same column to unoccupied square
                        chessBoard.move(srcRow, srcCol, dstRow, dstCol);
                        return true;

                    } else {
                        // white pawn moving to row 4 or 5, along same column to square occupied by a black piece
                        return false;	// pawns have to attack diagonally
                    }
                } else {
                    // first move for white pawn trying to row other than 4 or 5, along same column
                    return false;
                }

            } else if (((dstCol + 1) == srcCol) || ((dstCol - 1) == srcCol)) {
                // white pawn moving to adjacent column
                if (dstRow == 5) {
                    // white pawn moving to adjacent column, up one row
                    if (chessBoard.getChessPiece(dstRow, dstCol) != null) {
                        // white pawn moving to adjacent column, up one row, to an occupied square
                        if( chessBoard.getChessPiece(dstRow, dstCol).Color == Color.Black) {
                            // white pawn moving to adjacent column, up one row, to a square occupied by a black piece
                            chessBoard.move(srcRow, srcCol, dstRow, dstCol);
                            return true;
                        } else {
                            //white pawn moving to adjacent column, up one row, to a square occupied by a white piece
                            return false;
                        }

                    } else {
                        // white pawn moving to an adjacent column, up one row, to an unoccupied square
                        return false;
                    }
                } else {
                    // white pawn moving to an adjacent column, to a row other than row 2
                    return false;
                }
            } else {
                // no idea what this move was supposed to be doing
                return false;
            }
        }

        private bool checkWhitePawnNotFirstMove(int srcRow, int srcCol, int dstRow, int dstCol, ChessBoard chessBoard) {
            if (srcCol == dstCol) {
                // white pawn moving along same column
                if ((srcRow - 1) == dstRow) {
                    // white pawn moving along the same column, up one row
                    if (chessBoard.getChessPiece(dstRow, dstCol) == null) {
                        // white pawn moving along the same column, up one row to an unoccupied square	
                        chessBoard.move(srcRow, srcCol, dstRow, dstCol);
                        return true;
                    } else {
                        // white pawn moving along the same column, up one row to occupied square
                        return false; 	// pawns have to attack diagonally
                    }
                } else {
                    return false;	// after the first move, pawns can only move up one row at a time
                }

            } else if (((dstCol - 1) == srcCol) || ((dstCol + 1) == srcCol)) {
                // white pawn moving to an adjacent column
                if ((srcRow + 1) == dstRow) {
                    // white pawn moving to an adjacent column, up one row 
                    if (chessBoard.getChessPiece(dstRow, dstCol) != null) {
                        // white pawn moving to an adjacent column, up one row, to an occupied square
                        chessBoard.move(srcRow, srcCol, dstRow, dstCol);
                        return true;
                    } else {
                        // white pawn moving to an adjacent column, up one row, to an unoccupied square
                        return false;	// when not attacking, pawns can only move straight ahead
                    }
                } else {
                    return false;	// unless it's the first move, pawns can only advance one row at a time
                }
            } else {
                // completely invalid move
                return false;
            }
        
        }
        public override string ToString() {
            return "P";
        }
    }
}
