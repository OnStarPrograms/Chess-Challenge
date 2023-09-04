using ChessChallenge.API;
using System;

/*
 * Time Tracker that will stop the recursion if run out of time
 * 
 * implement points
 * 
 * after poit implementation, add tree prunning
 * 
 * 
 * 
 */
public class MyBot : IChessBot
{
    public Move Think(Board board, Timer timer)
    {
        int[] Points = [1, 3, 3, 4, 8 ];
        String[] Peices = ["Pawn", "Knight", "Bishop", "Rook", "Queen"];
        Move[] moves = board.GetLegalMoves();
        int[] depth =  new int[moves.Length];
        //////////////////////////////////////////////////////////

        for (int i = 0; i < moves.Length; i++)
        {
            //Deptch
            depth[i] = 3;
            //
        }
        /////////////////////////////////////////////////////

        int Recurse(int Depth, int Points)
        {
            Move[] moves2 = board.GetLegalMoves();
            int RecursionDepth = Depth;
            for (int j = 0 ; j < moves2.Length; j++)
            {
                if (Depth <= 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine(Depth);
                    Console.WriteLine("move choice" + j.ToString());
                    
                    Points += Recurse(RecursionDepth - 1, Points*-1);
                }
            }
            return Points;
        }
        /////////////////////////////////////////////////////////
        
        for (int i = 0; i < moves.Length; i++)
        {
            int Rec = Recurse(depth[i], 3);
            Console.WriteLine(Math.Abs(Rec));
        }
        return moves[0];
    }
}