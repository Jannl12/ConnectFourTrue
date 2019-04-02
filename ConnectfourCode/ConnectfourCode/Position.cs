using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectfourCode
{
    public class Position : bitBoard
    {
        
        const int height = 6, width = 7;
        public static bool CanPlay(ulong[] bitmap, int coulumn)
        {
            ulong mask = 1;
            ulong boardstate = bitmap[0] ^ bitmap[1];
            if (((boardstate >> ((coulumn * width) + height)) & mask) == mask)
                return false;
            else return true;
        }
        
        public int NegaMax(bitBoard node, int alpha, int beta, int moves)
        {
            if (node.isWin(moves))
                return 22 - moves;
            moves++;
            for(int i = 0; i < height; i++)
            {
                node.makeMove(i, moves);
                if (CanPlay(node.BitGameBoard, i))
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
