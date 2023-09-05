using ChessChallenge.API;
using System;
using System.Drawing;

/*
 * 
 * Add Point Weighing
 * 
 * after poit implementation, add tree prunning
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
        ////// Initiators ////////
        int[] Points = [1, 3, 3, 4, 8 ];
        String[] Peices = ["Pawn", "Knight", "Bishop", "Rook", "Queen"];
        Move[] moves = board.GetLegalMoves();
        int[] depth =  new int[moves.Length];
        Alloc MyPoints = new Alloc(moves.Length);
        //////////////////////////////////////////////////////////

        ////// Recursion ////////
        void Recurse(int Depth, int Weight, int Ass)
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
                    for (int k = 0; k < Peices.Length; k++)
                    {
                        
                        if (moves[Ass].MovePieceType.ToString() == Peices[k])
                        {
                            TotalPoint += Points[k];
                        }
                    }
                    TotalPoint = TotalPoint*Weight;
                    MyPoints.AddPoint(TotalPoint, Ass);
                    Recurse(RecursionDepth - 1, Weight*-1, Ass);
                }
            }
        }
        ////////////////////////////////////////////////////////////


        ////// Start ////////
        for (int i = 0; i < moves.Length; i++)
        {
            ////Depth////////
            depth[i] = 4 ;
            ////Depth////////
            

            Recurse(depth[i], 1, i);
            Console.Write(moves[i].MovePieceType);
            Console.WriteLine(MyPoints.returnPoint(i));
        }

        ////// Decider ////////
        return moves[0];
    }
}