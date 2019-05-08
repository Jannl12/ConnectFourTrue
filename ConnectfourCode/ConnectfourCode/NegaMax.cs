using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConnectfourCode
{
    public class Negamax : BitBoard
    {
        public int bestMove = 0;
        public int thisIsMaxDepth = 15;
        int[] turnArray = { 0, 1, 2, 3, 4, 5, 6 };
        Dictionary<ulong, int> TranspositionTable = new Dictionary<ulong, int>();
         

        public void ResetBestMove() 
        {
            bestMove = 0;
            TranspositionTable.Clear();
        }

        private const int height = 6, width = 7;       
        public int NegaMax(int alpha, int beta, int thisIsMaxDepth, int color)

            //TODO: Skal med i implementeringen
        {
            ulong lookuphashCode = this.GetBoardKey();
            int evalBuffer = 0;

            int[] test = { 1, 4, 9, 1000 };

            if ((evalBuffer = EvaluateBoardDLL()) >= 1000 || thisIsMaxDepth == 0)
            {
                return color * evalBuffer;
            }

            foreach(int i in possibleMoves())
            {
                int value = -NegaMax(-beta, -alpha, thisIsMaxDepth - 1, -color);

                if (value >= beta)
                {
                    UndoMove();
                    return value;
                }
                if (value > alpha)
                {
                    alpha = value;
                    if (this.thisIsMaxDepth == thisIsMaxDepth)
                        bestMove = i;
                }
                UndoMove();
            }
            return alpha;
        }        
    }
}
