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

            int boardEvaluationBuffer, boardKeyBuffer = GetBoardKey();

            
            if(transpositionTabel.TryGetValue(boardKeyBuffer, out boardEvaluationBuffer))
            {
                if (boardEvaluationBuffer == 1000)
                {
                    return (GetPreviousPlayer() == 0 ? 1000 - moveCount : -2 * 1000 + moveCount) * color;
                }
                else if(depth == 0)
                {
                    return boardEvaluationBuffer;
                }
            } 
            else
            {
                if (IsDraw())
                {
                    return 0;
                }
                if (IsWin(out boardEvaluationBuffer))
                {
                    transpositionTabel.Add(boardKeyBuffer, boardEvaluationBuffer);
                    return (GetPreviousPlayer() == 0 ? 1000 - moveCount : -2 * 1000 + moveCount) * color;
                }
                else if (depth == 0)
                {
                    transpositionTabel.Add(boardKeyBuffer, boardEvaluationBuffer);
                    return boardEvaluationBuffer;
                }
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
