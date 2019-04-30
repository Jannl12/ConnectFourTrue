using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConnectfourCode
{
    public class Negamax : ArrayGameBoard
    {
        public int bestMove = 3; // :)
        public int thisIsMaxDepth = 9;
        int[] turnArray = { 0, 1, 2, 3, 4, 5, 6 };
        Dictionary<ulong, int> TranspositionTable = new Dictionary<ulong, int>();
         

        public void ResetBestMove() 
        {
            bestMove = 3;
            TranspositionTable.Clear();
        }

        private const int height = 6, width = 7;       
        public int NegaMax(int alpha, int beta, int maxDepth, int color)

            //TODO: Skal med i implementeringen
        {
            ulong lookuphashCode = this.GetBoardKey();
            int evalBuffer = 0;

            //TODO: Lav eventuelt nyt bitboard hver gang funktionen kaldes. JAN OG MAYOH
            if (IsWin() || maxDepth == 0)
            {
                if(TranspositionTable.TryGetValue(lookuphashCode, out evalBuffer))
                {
                    return evalBuffer * color; // TODO: Add heuristic score JAN OG MAYOH
                }
                else
                {
                    evalBuffer = EvaluateBoard();
                    TranspositionTable[lookuphashCode] = evalBuffer;
                    return evalBuffer * color;
                }


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
