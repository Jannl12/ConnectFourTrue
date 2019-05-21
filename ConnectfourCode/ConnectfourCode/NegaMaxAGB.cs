using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectfourCode
{
    class NegaMaxAGB : ArrayGameBoard
    {
        private Dictionary<int, int> rootMoves = new Dictionary<int, int>();
        private Dictionary<ulong, int> transpositionTabel = new Dictionary<ulong, int>();
        private int plyDepth, value = int.MinValue;

        public NegaMaxAGB(int plyDepth)
        {
            this.plyDepth = plyDepth;
        }
        public int GetBestMove(int playerColor)
        {
            Negamax(int.MinValue + 1, int.MaxValue, plyDepth, playerColor, true);
            KeyValuePair<int, int> bestMoveBuffer = new KeyValuePair<int, int> ( 7, int.MinValue );
            foreach (KeyValuePair<int, int> move in rootMoves)
            {
                //if (bestMoveBuffer.Equals(default))
                //{
                //    bestMoveBuffer = move;
                //}
                //else 
                if (move.Value > bestMoveBuffer.Value)
                {
                    bestMoveBuffer = move;
                }
            }
            transpositionTabel = new Dictionary<ulong, int>();
            rootMoves = new Dictionary<int, int>();
            value = int.MinValue;
            return bestMoveBuffer.Key;
        }
        private int Negamax(int alpha, int beta, int depth, int color, bool rootNode)
        {
            int boardEvaluationBuffer;
            if(depth == 0)
            {
                ulong boardKeyBuffer = GetBoardKey();
                if (transpositionTabel.TryGetValue(boardKeyBuffer, out boardEvaluationBuffer))
                {
                    return boardEvaluationBuffer * color;
                }
                else
                {
                    boardEvaluationBuffer = EvaluateBoard();
                    transpositionTabel.Add(boardKeyBuffer, boardEvaluationBuffer);
                    return boardEvaluationBuffer * color;
                }
            } else if(IsDraw())
            {
                return 0;
            }
            else if (IsWin())
            {
                return 1000 * color;
            }

            foreach (int move in PossibleMoves())
            {
                MakeMove(move);
                int valueBuffer = -Negamax(-beta, -alpha, depth - 1, -color, false);
                value = valueBuffer > value ? valueBuffer : value;
                if (rootNode)
                {
                    rootMoves.Add(move, valueBuffer);
                }

                alpha = value > alpha ? value : alpha;

                if(alpha >= beta)
                {
                    UndoMove();
                    break;
                }
                UndoMove();
            }
            return value;
        }
    }
}
