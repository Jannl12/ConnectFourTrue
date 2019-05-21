using System.Collections.Generic;


namespace ConnectfourCode
{
    public class NegamaxArray : ArrayGameBoard
    {

        public int bestMove { get; set; }

        public int PlyDepth;
        Dictionary<ulong, int> TranspositionTable = new Dictionary<ulong, int>();

        public void ResetTranspositionTable()
        {
            TranspositionTable.Clear();
        }

        public NegamaxArray(int plyDepth)
        {
            this.PlyDepth = plyDepth;
        }

        public int GetBestMove(int player)
        {
            NegaMax(int.MinValue + 1, int.MaxValue, PlyDepth, player, true);
            int bufferBestMove = bestMove;
            ResetGame();
            return bufferBestMove;
        }

        public int NegaMax(int alpha, int beta, int depth, int color, bool rootNode)

        {
            ulong lookUpBoardKey = GetBoardKey();
            int evalBuffer = 0;
            if (IsWin()) {
                IsWin();
                return (GetPreviousPlayer() == 0 ? 1000 - moveCount : -2 * 1000 + moveCount) * color;
            }
        
            else if (IsDraw()) {
                IsDraw();
                return 0;
            }
            else if (depth == 0)
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
                if (rootNode)
                    System.Console.WriteLine("Current move is {0}, with a score of {1}", move, value);
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
