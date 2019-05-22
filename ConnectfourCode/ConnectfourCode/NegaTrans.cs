﻿using System.Collections.Generic;


namespace ConnectfourCode
{
    public class NegaTrans : ArrayGameBoard//BitBoard
    {

        public int bestMove { get; set; }

        public int PlyDepth;
        Dictionary<int, int> TranspositionTable = new Dictionary<int, int>();

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
            ResetGame();
            return bufferBestMove;
        }

        public int NegaMax(int alpha, int beta, int depth, int color, bool rootNode)

        {
            int lookUpBoardKey = GetBoardKey();
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
