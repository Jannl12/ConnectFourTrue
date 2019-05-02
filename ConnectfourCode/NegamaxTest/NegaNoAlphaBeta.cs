using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConnectfourCode;

namespace NegamaxTest
{
    public class NegaNoAlphaBeta : BitBoard
    {

        public int bestMove { get; set; }
        int[] turnArray = { 3, 2, 4, 1, 5, 0, 6 };
        int value = 0;


        public void ResetBestMove()
        {
            bestMove = 3;
        }

        private const int height = 6, width = 7;
        public int NegaMax(int maxDepth, int color, bool firstCall)

        //TODO: Skal med i implementeringen
        {
            if (IsWin() || maxDepth == 0 || IsDraw())
            {
                int evalBuffer = EvaluateBoard();
                return evalBuffer * color; 

            }
            value = int.MinValue;

            foreach (int i in turnArray)
            {
                MakeMove(i);
                if (CanPlay(i))
                {
                    int bufvalue = value;
                    value = Math.Max(value,-NegaMax(maxDepth - 1, -color, false));
                    if (bufvalue != value && firstCall)
                        bestMove = i;
                }
                UndoMove();
            }
            return value;
        }
    }
}
