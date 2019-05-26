using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectfourCode
{
    //https://github.com/PascalPons/connect4/blob/master/Solver.cpp
    public class NegaMaxAGB : ArrayGameBoard
    {
        private Dictionary<int, int> rootMoves = new Dictionary<int, int>();
        private Dictionary<int, int> transpositionTabel = new Dictionary<int, int>();
        private int plyDepth;
        public int bestMove;

        public NegaMaxAGB(int plyDepth)
        {
            this.plyDepth = plyDepth;
        }

        /**<summary><c>ResetTranspositionTable</c> resets the transposition table used by the negamax method.</summary>
        */
        public void ResetTranspositionTable()
        {
            transpositionTabel.Clear();
        }
        /**<summary><c>GetBestMove</c> a method that runs the negamax funktion.</summary>
        * <returns>The best move found by the negamax function.</returns>
       */
        public int GetBestMove()
        {
            NegaMax(int.MinValue + 1, int.MaxValue, plyDepth,  true);
            return bestMove;
        }


        /**<summary><c>NegaMax</c>Finds the best value of each branch in a gametree.</summary>
         * <returns>The value of the best branch as an integer.</returns>
        */
        public int NegaMax(int alpha, int beta, int depth, bool rootNode)
        {

            int boardEvaluationBuffer, boardKeyBuffer = GetBoardKey();

            
            if(transpositionTabel.TryGetValue(boardKeyBuffer, out boardEvaluationBuffer))
            {
                if (boardEvaluationBuffer == 1000)
                {
                    return -10000 + moveCount;
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
                    return -10000 + moveCount;
                }
                else if (depth == 0)
                {
                    transpositionTabel.Add(boardKeyBuffer, boardEvaluationBuffer);
                    return boardEvaluationBuffer;
                }
            }

            foreach (int move in PossibleMoves())
            {
                MakeMove(move);
                int value = -NegaMax(-beta, -alpha, depth - 1, false);

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
