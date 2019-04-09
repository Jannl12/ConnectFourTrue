using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectfourCode
{
    public class Negamax : BitBoard
    {

        const int height = 6, width = 7, depth = 7;
        public int NegaMax(BitBoard node, int alpha, int beta, int moveCounter)
            //TODO: Skal med i implementeringen
        {
            //TODO: Lav eventuelt nyt bitboard hver gang funktionen kaldes. JAN OG MAYOH
            if (node.IsWin())
                return 22 - moveCounter; // TODO: Add heuristic score JAN OG MAYOH
            moveCounter++;
            for(int i = 0; i < width; i++)
            {
                node.MakeMove(i);
                if (node.CanPlay(i))
                {
                    int value = -NegaMax(node, -beta, -alpha, moveCounter);

                    if (value >= beta)
                        return value;
                    if (value >= alpha)
                        alpha = value;
                }                
                node.UndoMove();
            }
            return alpha;
        }        
    }
}
