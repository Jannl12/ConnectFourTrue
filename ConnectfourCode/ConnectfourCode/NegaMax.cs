using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConnectfourCode
{
    public class Negamax : BitBoard
    {
        public int bestMove = 3; // :)
        public int thisIsMaxDepth = 9;
        int[] turnArray = { 3, 2, 4, 1, 5, 0, 6 };
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

            if ((evalBuffer = EvaluateBoard()) >= 1000 || maxDepth == 0)
            {
                return color * evalBuffer;
            }

            foreach(int i in possibleMoves())
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
                UndoMove();
            }
            return alpha;
        }        
    }
}
