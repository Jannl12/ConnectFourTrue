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

        public int NegaMax(int depth, int color, bool firstCall)

        //TODO: Skal med i implementeringen
        {
            if (IsWin() || depth == 0 || IsDraw())
            {
                int evalBuffer = EvaluateBoard();
                return evalBuffer * color; 

            }
            value = int.MinValue;
            List<int> moves = PossibleMoves();
            foreach (int move in moves)
            {

                    MakeMove(move);
                    int bufferValue = value;
                    value = Math.Max(value,-NegaMax(depth - 1, -color, false));
                    if (bufferValue < value && firstCall)
                        bestMove = move;
                    UndoMove();

            }
            return value;
        }
    }
}
