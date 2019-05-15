using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectfourCode
{
    public class NegaTrans : BitBoard
    {
        public int bestMove { get; set; }
        Dictionary<ulong, int> TranspositionTable = new Dictionary<ulong, int>();

        public void ResetTranspositionTable()
        {
            TranspositionTable.Clear();
        }


        public int NegaMax(int alpha, int beta, int depth, int color, bool firstCall)

        //TODO: Skal med i implementeringen
        {
            ulong lookUpBoardKey = GetBoardKey();
            int evalBuffer = 0;
            if (IsWin() || depth == 0 || IsDraw())
            {
                if (TranspositionTable.TryGetValue(lookUpBoardKey, out evalBuffer))
                    return evalBuffer * color;
                else
                {
                    evalBuffer = EvaluateBoard();
                    TranspositionTable[lookUpBoardKey] = evalBuffer;
                    return evalBuffer * color;
                }
            }
            //int value = int.MinValue;
            List<int> moves = possibleMoves();

            foreach (int move in moves)
            {
                MakeMove(move);
                int value = -NegaMax(-beta, -alpha, depth - 1, -color, false);

                if (value >= beta)
                {
                    UndoMove();
                    return value;
                }

                if (value > alpha)
                {
                    alpha = value;
                    if (firstCall)
                        bestMove = move;
                }
                UndoMove();
            }
            return alpha;
        }
    }
}
