using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessParser {
    public class King : ChessPiece {

        public Boolean HasMoved { get; private set; }

        public King(Color c) : base(c)
        {
            HasMoved = false;
        }

        public override string ToString() {
            return (this.Color == Color.Black) ? "k" : "K";
        }

        public override Boolean LegalMove( int srcRow, int srcCol, int dstRow, int dstCol, ChessBoard chessBoard )
        {
            // first see if this is a queen side castle
            if( legalQueenSideCastle(srcRow, srcCol, dstRow, dstCol, chessBoard) == true )
            {
                return true;
            }

            // first see if this is a king side castle
            if( legalKingSideCastle(srcRow, srcCol, dstRow, dstCol, chessBoard) == true )
            {
                return true;
            }

            // if the destination square is vacant, or has a piece 
            // of the opposite color, so far so good
            if (DestinationChecker(dstRow, dstCol, chessBoard) == true)
            {
                // can only move to an adjacent square
                if ((Math.Abs(srcCol - dstCol) == 1) || (Math.Abs(srcRow - dstRow) == 1))
                {
                    // just a non-castling king move
                    HasMoved = true;
                    chessBoard.move(srcRow, srcCol, dstRow, dstCol);
                    return true;
                }
            }
            return false;
        }

        private Boolean legalQueenSideCastle( int srcRow, int srcCol, int dstRow, int dstCol, ChessBoard chessBoard ) { 
            // see if the src square and dst square on along row 0 or row 7
            if(( srcRow == dstRow ) && (( srcRow == 0 ) || ( srcRow == 7)))
            {
                // see if the move looks like a queen side castle (e8c8, or e1c1)
                if ((srcCol == 4) && (dstCol == 2))
                {
                    // see if the piece on col 0 is a rook 
                    Rook rook = chessBoard.getChessPiece(dstRow, 0) as Rook;
                    if (rook != null) {
                        // it's a rook, see if see it's the same color as the king 
                        if(this.Color == rook.Color)
                        {
                            // the rook and the king are the same color...
                            // see if there are any pieces blocking the path between the rook and the king
                            if ((chessBoard.getChessPiece(srcRow, 1) == null)
                            && (chessBoard.getChessPiece(srcRow, 2) == null)
                            && (chessBoard.getChessPiece(srcRow, 3) == null))
                            {
                                // the piece on col 0 of row 0 or 7 is a rook of the same color as the king...
                                // there are no blocking pieces...
                                if (this.HasMoved == true || rook.HasMoved == true)
                                {
                                    return false;
                                }

                                // neither the king nor the rook have moved yet...
                                // the queen-side castle is good
                                chessBoard.move(srcRow, srcCol, dstRow, dstCol);    // move the king
                                this.HasMoved = true;

                                chessBoard.move(srcRow, 0, dstRow, 3);              // move the rook
                                rook.HasMoved = true;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private Boolean legalKingSideCastle(int srcRow, int srcCol, int dstRow, int dstCol, ChessBoard chessBoard)
        {
            // see if the src square and dst square on along row 0 or row 7
            if ((srcRow == dstRow) && ((srcRow == 0) || (srcRow == 7)))
            {
                // see if the move looks like a queen side castle (e8g8, or e1g1)
                if ((srcCol == 4) && (dstCol == 6))
                {
                    // see if the piece on col 7 is a rook 
                    Rook rook = chessBoard.getChessPiece(dstRow, 7) as Rook;
                    if (rook != null)
                    {
                        // it's a rook, see if see it's the same color as the king 
                        if (this.Color == rook.Color)
                        {
                            // the rook and the king are the same color...
                            // see if there are any pieces blocking the path between the rook and the king
                            if ((chessBoard.getChessPiece(srcRow, 5) == null)
                            && (chessBoard.getChessPiece(srcRow, 6) == null))
                            {
                                // the piece on col 7 of row 0 or 7 is a rook of the same color as the king...
                                // there are no blocking pieces... 
                                if (this.HasMoved == true || rook.HasMoved == true)
                                {
                                    return false;
                                }

                                // neither the king nor the rook have moved yet...
                                // the king-side castle is good
                                chessBoard.move(srcRow, srcCol, dstRow, dstCol);    // move the king
                                this.HasMoved = true;

                                chessBoard.move(srcRow, 7, dstRow, 5);
                                rook.HasMoved = true;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}
