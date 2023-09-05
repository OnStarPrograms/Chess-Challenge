using ChessChallenge.API;
using System;
using System.Drawing;

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
class Alloc
{
    public int[] MyPointts;
    public Alloc(int Moves)
    {
        MyPointts = new int[Moves];
    }

    public void AddPoint(int P, int Accesor)
    {
        MyPointts[Accesor] += P;
    }
    public int returnPoint(int Accessor)
    {
        return MyPointts[Accessor];
    }
}

public class MyBot : IChessBot
{
    public Move Think(Board board, Timer timer)
    {
        int[] Points = [1, 3, 3, 4, 8 ];
        String[] Peices = ["Pawn", "Knight", "Bishop", "Rook", "Queen"];
        Move[] moves = board.GetLegalMoves();
        int[] depth =  new int[moves.Length];
        Alloc MyPoints = new Alloc(moves.Length);
        //////////////////////////////////////////////////////////

        for (int i = 0; i < moves.Length; i++)
        {
            //Deptch
            depth[i] = 3;
            //
        }
        /////////////////////////////////////////////////////

        void Recurse(int Depth, int Points, int Ass)
        {
            Move[] moves2 = board.GetLegalMoves();
            int RecursionDepth = Depth;
            for (int j = 0 ; j < moves2.Length; j++)
            {
                if (Depth <= 0 || timer.MillisecondsRemaining<10000)
                {
                    break;
                }
                else
                {
                    //Console.WriteLine(Depth);
                    //Console.WriteLine("move choice" + j.ToString());
                    int TotalPoint = 1;
                    MyPoints.AddPoint(TotalPoint, Ass);
                    Recurse(RecursionDepth - 1, Points*-1, Ass);
                }
            }
        }
        /////////////////////////////////////////////////////////
        for (int i = 0; i < moves.Length; i++)
        {
            Recurse(depth[i], 1, i);
            Console.WriteLine(MyPoints.returnPoint(i));
            Console.Write(moves[i].MovePieceType);
        }
        return moves[0];
    }
}