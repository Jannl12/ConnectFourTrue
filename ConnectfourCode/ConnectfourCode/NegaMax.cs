using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;


namespace ConnectfourCode
{
    public class Negamax : BitBoard
    {
        public int bestMove { get; set; }
        public int PlyDepth { get; }




        public Negamax (int inputPlyDepth)
        {
            PlyDepth = inputPlyDepth;   
        }



        public int NegaMax(int alpha, int beta, int depth, int color, bool firstCall)

        //TODO: Skal med i implementeringen
        {

            if (IsWin() || depth == 0 || IsDraw())
            {
                int evalBuffer = EvaluateBoard();
                return evalBuffer * color;
            }
            int value = int.MinValue;
            List<int> moves = possibleMoves();
            foreach (int move in moves)
            {
                MakeMove(move);

                value = Math.Max(value,-NegaMax(-beta, -alpha, depth - 1, -color, false));


                if (value >= beta)
                {
                    UndoMove();
                    return value;
                }
                if (value > alpha)
                {
                    alpha = value;
                    if (firstCall)
                        bestMove = move;
                }
                UndoMove();
            }
            return alpha;
        }
    }
}
