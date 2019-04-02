using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectfourCode
{
    public class Negamax : bitBoard
    {
        
        const int height = 6, width = 7;

       
        public int NegaMax(bitBoard node, int alpha, int beta, int moves)
        {
            if (node.isWin(moves))
                return 22 - moves; // TODO: Add heuristic score
            moves++;
            for(int i = 0; i < height; i++)
            {
                node.makeMove(i, moves);
                if (node.CanPlay(i))
                {
                    int value = -NegaMax(node, -beta, -alpha, moves);

                    if (value >= beta)
                        return value;
                    if (value >= alpha)
                        alpha = value;
                }
                else {
                    node.UndoMove(i, moves);
                     }
            }
            return alpha;
        }        
    }
}
