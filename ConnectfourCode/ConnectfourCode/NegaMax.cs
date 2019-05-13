using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;


namespace ConnectfourCode
{
    public class Negamax : ArrayGameBoard
    {


        public int bestMove { get; set; }
        int[] turnArray =  { 3, 2, 4, 1, 5, 0, 6 };

 
        public int NegaMax(int alpha, int beta, int depth, int color, bool firstCall)

            //TODO: Skal med i implementeringen
        {

            if (IsWin() || depth == 0 || IsDraw())
            {
                int evalBuffer = EvaluateBoard();
                return evalBuffer*color
            }
            int value = int.MinValue;

            foreach(int move in possibleMoves())
            {

                
                if (CanPlay(i))
                {
                    MakeMove(i);
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
                            bestMove = i;
                    }
                    UndoMove();
                }
                
            }
            return alpha;
        }        
    }
}
