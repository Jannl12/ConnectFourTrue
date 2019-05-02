using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;


namespace ConnectfourCode
{
    public class Negamax : BitBoard
    {

        public int bestMove { get; set; }
        int[] turnArray =  { 3, 2, 4, 1, 5, 0, 6 };
        public int thisIsMaxDepth;
        Dictionary<int, int> TranspositionTable = new Dictionary<int, int>();



        public void ResetBestMove() 
        {
            bestMove = 3;
            //TranspositionTable.Clear();
        }

        private const int height = 6, width = 7;       
        public int NegaMax(int alpha, int beta, int maxDepth, int color, bool firstCall)

            //TODO: Skal med i implementeringen
        {

            if (IsWin() || maxDepth == 0 || IsDraw())
            {
                int evalBuffer = EvaluateBoard();
                return evalBuffer * color;
               
            }

            foreach(int i in turnArray)
            {
                MakeMove(i);
                if (CanPlay(i))
                {
                    int value = -NegaMax(-beta, -alpha, maxDepth - 1, -color, false);

                    if (value >= beta)
                    {
                        UndoMove();
                        return value;
                    }
                    if (value > alpha)
                    {
                        alpha = value;
                        if (firstCall)
                            bestMove = i;

                    }
                }
                UndoMove();
            }
            return alpha;
        }        
    }
}
