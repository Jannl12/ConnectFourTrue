using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectfourCode
{
    public class Negamax : BitBoard
    {

        public int bestMove = 0;

        private const int height = 6, width = 7;       
        public int NegaMax(BitBoard node, int alpha, int beta, int maxDepth)
            //TODO: Skal med i implementeringen
        {
            //TODO: Lav eventuelt nyt bitboard hver gang funktionen kaldes. JAN OG MAYOH
            if (node.IsWin())
                return 44 - node.MoveCount; // TODO: Add heuristic score JAN OG MAYOH
            else if (maxDepth == 0)
                return 0;

            for(int i = 0; i < width; i++)
            {
                node.MakeMove(i);
                if (node.CanPlay(i))
                {
                    int value = -NegaMax(node, -beta, -alpha, maxDepth - 1);

                    if (value >= beta)
                        return value;
                    if (value >= alpha)
                    {
                        alpha = value;
                        bestMove = i;
                    }
                }                
                node.UndoMove();
            }
            return alpha;
        }        
    }
}
