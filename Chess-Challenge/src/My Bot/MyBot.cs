using ChessChallenge.API;
using System;

public class MyBot : IChessBot
{
    public Move Think(Board board, Timer timer)
    {
        Move[] moves = board.GetLegalMoves();
        Console.WriteLine(timer.MillisecondsRemaining);
        for (int i=0; i<moves.Length;i++){
            Console.WriteLine(moves[i]);
            Console.WriteLine(moves[i].MovePieceType);
        }
        return moves[0];
    }
}