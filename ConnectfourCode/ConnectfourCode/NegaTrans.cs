using System.Collections.Generic;
using System.Linq;

namespace ConnectfourCode
{
    public class NegaTrans : BitBoard
    {

        public int bestMove { get; set; }
        Dictionary<int, int> bestMoves = new Dictionary<int, int>();
        public int PlyDepth;
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
            KeyValuePair<int, int> tester = new KeyValuePair<int, int>();
            foreach ( KeyValuePair<int, int> move in bestMoves)
            {
                if(move.Value > tester.Value)
                {
                    tester = move;
                }
            } 
            bestMoves = new Dictionary<int, int>();
            return tester.Key;
        }

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
            List<int> moves = PossibleMoves();
            foreach (int move in moves)
            {

                MakeMove(move);
                int value = -NegaMax(-beta, -alpha, depth - 1, -color, false);

                if (value > alpha)
                {
                    alpha = value;

                }
                if (alpha >= beta)
                {
                    if (value > alpha)
                    {
                        alpha = value;

                        if (rootNode)
                            bestMove = move;
                    }
                    UndoMove();
                    return value;
                }
                
                if(rootNode)
                {
                    bestMoves.Add(move, value);
                }
                UndoMove();
            }
            return alpha;
        }
    }
}
