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

        public int PlyDepth;
        int[] turnArray = { 3, 2, 4, 1, 5, 0, 6 };
        Dictionary<ulong, int> TranspositionTable = new Dictionary<ulong, int>();

        public void ResetTranspositionTable()
        {
            TranspositionTable.Clear();
        }

        public NegaTrans(int plyDepth)
        {
            this.PlyDepth = plyDepth;
        }

        public int GetBestMove(int player)
        {
            NegaMax(int.MinValue + 1, int.MaxValue, PlyDepth, player, true);
            int bufferBestMove = bestMove;
            bestMove = 3;
            ResetGame();
            return bufferBestMove;
        }

        public int NegaMax(int alpha, int beta, int depth, int color, bool rootNode)

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
            List<int> moves = PossibleMoves();

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

                    if (rootNode)
                        bestMove = move;
                }
                UndoMove();
            }
            return alpha;
        }
    }
}
