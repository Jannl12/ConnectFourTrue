using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectfourCode
{
    //https://github.com/PascalPons/connect4/blob/master/Solver.cpp
    class NegaMaxAGB : ArrayGameBoard
    {
        private Dictionary<int, int> rootMoves = new Dictionary<int, int>();
        private Dictionary<int, int> transpositionTabel = new Dictionary<int, int>();
        private int plyDepth;

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
            transpositionTabel = new Dictionary<int, int>();
            rootMoves = new Dictionary<int, int>();

            return bestMoveBuffer.Key;
        }



        private int Negamax(int alpha, int beta, int depth, int color, bool rootNode)
        {
            

            if (depth == 0)
            {
                int boardEvaluationBuffer;
                int boardKeyBuffer = GetBoardKey();
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
            }
            else if (IsDraw())
            {
                return 0;
            }
            else if (IsWin())
            {
                return 1000 * color;
            }

            int value = int.MinValue + 1;
            foreach (int move in PossibleMoves())
            {
                MakeMove(move);
                int valueBuffer = -Negamax(-beta, -alpha, depth - 1, -color, false);
                value = value > valueBuffer ? value : valueBuffer;

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
