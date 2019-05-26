using System.Collections.Generic;


namespace ConnectfourCode
{
    public class NegaTrans : BitBoard
    {

        public int bestMove { get; set; }

        public int PlyDepth;
        private Dictionary<ulong, int> TranspositionTable = new Dictionary<ulong, int>();

        /**<summary><c>ResetTranspositionTable</c> resets the transposition table used by the negamax method.</summary>
        */
        public void ResetTranspositionTable()
        {
            TranspositionTable.Clear();
        }

        /**<summary><c>NegaTrans</c> Constructor for the funktion that that sest the ply depth of the negamax method.</summary>
        */
        public NegaTrans(int plyDepth)
        {
            this.PlyDepth = plyDepth;
        }

        /**<summary><c>GetBestMove</c> a method that runs the negamax funktion.</summary>
        * <returns>The best move found by the negamax function.</returns>
       */
        public int GetBestMove()
        {
            int color = GetCurrentPlayer() == 0 ? 1 : -1;
            NegaMax(int.MinValue + 1, int.MaxValue, PlyDepth, color, true);
            return bestMove;
        }

        /**<summary><c>NegaMax</c>Finds the best value of each branch in a gametree.</summary>
         * <returns>The value of the best branch as an integer.</returns>
        */
        public int NegaMax(int alpha, int beta, int depth, int color, bool rootNode)
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
            foreach (int move in PossibleMoves())
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
