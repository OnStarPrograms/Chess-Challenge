using ChessChallenge.API;
using System;

public class MyBot : IChessBot
{
    public Move Think(Board board, Timer timer)
    {
        int[] Points = [1, 3, 3, 4, 8 ];
        String[] Peices = ["Pawn", "Knight", "Bishop", "Rook", "Queen"];
        Move[] moves = board.GetLegalMoves();
        Console.WriteLine(timer.MillisecondsRemaining);
        for (int i = 0; i < moves.Length; i++)
        {
            Console.WriteLine(moves[i]);
            Console.WriteLine(moves[i].MovePieceType);
        }


        int Recurse(int Depth, int Points)
        {
            Move[] moves = board.GetLegalMoves();
            for (int j = 0 ; j < moves.Length; j++)
            {
                if (Depth <= 0)
                {
                    break;
                }
                else
                {   

                    Console.WriteLine(Depth);
                    return Recurse(Depth - 1, Points*-1);
                }
            }
            return Math.Abs(Points);
        }

        int Rec = Recurse(3, 3);
        Console.WriteLine(Rec);
        return moves[Rec];
    }
}