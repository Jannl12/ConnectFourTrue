using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConnectfourCode
{
    public class Negamax : ArrayGameBoard
    {
        public int bestMove = 0, plyDepth;
        public int thisIsMaxDepth = 15;
        int[] turnArray = { 0, 1, 2, 3, 4, 5, 6 };
        Dictionary<int, int> TranspositionTable = new Dictionary<int, int>();
         

        //public void ResetBestMove() 
        //{
        //    bestMove = 3;
        //    //TranspositionTable.Clear();
        //}

        private const int height = 6, width = 7;
        
        public int GetBestMove(int plyDepth)
        {
            this.plyDepth = plyDepth;
            this.NegaMax(int.MinValue + 1, int.MaxValue, this.plyDepth, 1);
            return bestMove;
        }

        public int NegaMax(int alpha, int beta, int depth, int color)

            //TODO: Skal med i implementeringen
        {
            if (depth == 0) {
                int evalBuffer = 0, boardHashCode = gameboard.GetHashCode();
                if (TranspositionTable.TryGetValue(boardHashCode, out evalBuffer))
                {
                    return evalBuffer * color;
                }
                else
                {
                    evalBuffer = EvaluateBoard();
                    TranspositionTable.Add(boardHashCode, evalBuffer);
                    return evalBuffer * color;
                }
            }

            foreach(int move in possibleMoves())
            {
                MakeMove(move);
                int value = -NegaMax(-beta, -alpha, depth - 1, -color);

                if (value >= beta)
                {
                    UndoMove();
                    return value;
                }
                if (value > alpha)
                {
                    alpha = value;
                    if (plyDepth == depth)
                        bestMove = move;
                }
                UndoMove();
            }
            return alpha;
        }        
    }
}
