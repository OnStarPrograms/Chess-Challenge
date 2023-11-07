using ChessChallenge.API;
using System;
using System.Linq;

/*
 * 
 * Add Point Weighing
 * 
 * after poit implementation, add tree prunning
 * 
 */
class Alloc(int Moves)
{
    public int[] MyPointts = new int[Moves];
    public bool ch = false;

    public void AddPoint(int P, int Accesor)
    {
        MyPointts[Accesor] = MyPointts[Accesor] + P;
    }
    public int ReturnPoint(int Accessor)
    {
        return MyPointts[Accessor];
    }
    public void ClearPoints()
    {
        for (int i = 0; i < MyPointts.Length; i++)
        {
            MyPointts[i] = 0;
        }
    }
}

public class MyBot : IChessBot
{
    public Move Think(Board board, Timer timer)
    {
        ////// Initiators ////////
        int[] Peices = [1, 2, 3, 4, 5, 6];
        Move[] moves = board.GetLegalMoves();
        Alloc MyPoints = new(moves.Length);
        //Depth//
        int depth = 3;
        //Depth//
        int[] Points;
        
        //////////////////////////////////////////////////////////

        ////// Recursion ////////
        void Recurse(int Depth, int Weight, int Ass)
        {
            Move[] moves2 = board.GetLegalMoves();
            int RecursionDepth = Depth;
            for (int j = 0 ; j < moves2.Length; j++)
            {
                if (Depth <= 0 || timer.MillisecondsRemaining < 10000) //
                {
                    break;
                }
                else
                {
                    int TotalPoint = 0;
                    //Console.WriteLine(Depth);
                    //Console.WriteLine("move choice" + j.ToString());
                    Piece capturedPiece = board.GetPiece(moves2[j].TargetSquare);

                    if (board.IsInCheckmate())
                    {
                        TotalPoint -= 10000000;
                    }
                    if (board.IsInCheckmate())
                    {
                        TotalPoint -= 1000;
                    }

                    if (Weight == -1)
                    {
                        Points = [0, 100 * Depth, 300 * Depth, 400 * Depth, 500 * Depth, 800 * Depth * Depth, 100000];
                        
                    }
                    else
                    {
                        Points = [0, 10 * Depth, 30 * Depth, 40 * Depth, 50 * Depth, 8 * Depth, 100];
                    }
                    TotalPoint += Points[(int)capturedPiece.PieceType];
                    switch ((int)moves2[j].CapturePieceType)
                    {
                        case 1:
                            TotalPoint += Points[0];
                            break;
                        case 2:
                            TotalPoint += Points[1];
                            break;
                        case 3:
                            TotalPoint += Points[2];
                            break;
                        case 4:
                            TotalPoint += Points[3];
                            break;
                        case 5:
                            TotalPoint += Points[4];
                            break;
                        case 6:
                            TotalPoint += Points[6];
                            break;
                    }
                    TotalPoint *= Weight;
                    MyPoints.AddPoint(TotalPoint, Ass);
                    //Console.WriteLine(MyPoints.returnPoint(Ass));
                    board.MakeMove(moves2[j]);
                    Recurse(RecursionDepth - 1, Weight * -1, Ass);
                    board.UndoMove(moves2[j]);

                }
            }
        }
        ////////////////////////////////////////////////////////////

        int Timer = timer.MillisecondsRemaining;
        ////// Start ////////
        
        for (int i = 0; i < moves.Length; i++)
        {
            ////Depth////////
            ////Depth////////

            board.MakeMove(moves[i]);
            /*if (Timer - timer.MillisecondsRemaining < 1000)
            {
                depth += 1;
            }
            else
            {
                depth -= 1;
            }*/

            Timer = timer.MillisecondsRemaining;
            if (board.IsInCheckmate())
            {
                return moves[i];
            }
            if (!board.IsRepeatedPosition()) 
            {
                Recurse(depth, -1, i);
            }
            else
            {
                MyPoints.AddPoint(-1000000000, i);
            }
            board.UndoMove(moves[i]);
            Console.WriteLine(moves[i].MovePieceType.ToString());
            Console.WriteLine((int)moves[i].CapturePieceType);
        }
        ////// Decider ////////
        int Highest = 0;
        for (int i = 0; i < moves.Length; i++)
        {
            Console.WriteLine(MyPoints.ReturnPoint(i));
            if (MyPoints.ReturnPoint(i) > MyPoints.ReturnPoint(Highest))
            {
                Highest = i;
            }

        }
        
        MyPoints.ClearPoints();
        return moves[Highest];
    }
}