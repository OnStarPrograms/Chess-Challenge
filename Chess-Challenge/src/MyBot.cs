using ChessChallenge.API;
using System;

namespace ChessChallenge.Example
{
    // A simple bot that can spot mate in one, and always captures the most valuable piece it can.
    // Plays randomly otherwise.
    public class MyBot : IChessBot
    {
        // Piece values: null, pawn, knight, bishop, rook, queen, king
        int[] pieceValues = { 0, 100, 300, 300, 500, 900, 10000 };

        public Move Think(Board board, Timer timer)
        {
            Move[] allMoves = board.GetLegalMoves();

            // Pick a random move to play if nothing better is found
            Move moveToPlay = allMoves[0];
            int highestValueCapture = 0;

            foreach (Move move in allMoves)
            {
                // Always play checkmate in one

                

                // Find highest value capture
                Piece capturedPiece = board.GetPiece(move.TargetSquare);
                int capturedPieceValue = pieceValues[(int)capturedPiece.PieceType];

                
                if (isMoveSafe(board, move))
                {
                    if (capturedPieceValue > highestValueCapture)
                    {
                        moveToPlay = move;
                        highestValueCapture = capturedPieceValue;
                    }
                    //moveToPlay = move;
                    //break;
                }
            }

            return moveToPlay;
        }

        // Test if this move gives checkmate
        bool isMoveSafe(Board board, Move move)
        {
            board.MakeMove(move);
            Move[] EnemyMove = board.GetLegalMoves();
            bool isMate = false;
            bool isDager = false;
            bool isDager2 = false;
            foreach (Move mMove in EnemyMove)
            {
                board.MakeMove(mMove);
                Square StrtS = mMove.TargetSquare;
                if (!isMate)
                {
                    isMate = board.IsInCheck();
                }
                if (!isDager)
                {
                    isDager = board.SquareIsAttackedByOpponent(mMove.TargetSquare);
                    isDager2 = board.SquareIsAttackedByOpponent(mMove.StartSquare);
                }
                board.UndoMove(mMove);
                
            }
            board.UndoMove(move);
            Console.WriteLine(!isDager2);
            return (!isMate || !isDager || !isDager2);
        }
    }
}