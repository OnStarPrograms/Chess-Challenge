using ChessChallenge.API;
using System;

public class MyBot : IChessBot
{
    public Move Think(Board board, Timer timer)
    {
        Move[] moves = board.GetLegalMoves();
        for (int i=0; i<moves.Length;i++){
            Console.WriteLine(moves);
        }
        return moves[0];
    }
}