using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConnectfourCode
{
    public class NegaTrans : ArrayGameBoard
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
                    //if (evalBuffer == 1)
                    //   Thread.Sleep(1);
                    TranspositionTable[lookUpBoardKey] = evalBuffer;
                    return evalBuffer * color;
                }
            }
            //int value = int.MinValue;

            foreach (int move in PossibleMoves())
            {

                MakeMove(move);
                int value = -NegaMax(-beta, -alpha, depth - 1, -color, false);

                if (value >= beta)
                {
                    UndoMove();
                    return value;
                }
                //if(rootNode)
                //    Console.WriteLine("move is {0}, with a score of {1}", move,value);
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
