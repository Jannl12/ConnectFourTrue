using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConnectfourCode
{
    public class Negamax : BitBoard
    {
        public int bestMove = 3;
        public int thisIsMaxDepth = 9;
        int[] turnArray = { 3, 2, 4, 1, 5, 0, 6 };
        Dictionary<int, int> TranspositionTable = new Dictionary<int, int>();


        public void ResetBestMove() 
        {
            bestMove = 3;
            TranspositionTable.Clear();
        }

        private const int height = 6, width = 7;       
        public int NegaMax(int alpha, int beta, int maxDepth, int color)

            //TODO: Skal med i implementeringen
        {
            int lookuphashCode = bitGameBoard.GetHashCode();

            //TODO: Lav eventuelt nyt bitboard hver gang funktionen kaldes. JAN OG MAYOH
            if (IsWin() || maxDepth == 0)
            {
                    int evalBuffer = EvaluateBoard();
                    //TranspositionTable.Add(lookuphashCode, evalBuffer * color);
                    return evalBuffer * color; // TODO: Add heuristic score JAN OG MAYOH
               
            }

            foreach(int i in turnArray)
            {
                MakeMove(i);
                if (CanPlay(i))
                {
                    int value = -NegaMax(-beta, -alpha, maxDepth - 1, -color);

                    if (value >= beta)
                    {
                        UndoMove();
                        return value;
                    }
                    if (value > alpha)
                    {
                        alpha = value;
                        if (thisIsMaxDepth == maxDepth)
                            bestMove = i;

                    }
                }
                UndoMove();
            }
            return alpha;
        }        
    }
}
