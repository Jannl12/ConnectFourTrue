using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectfourCode
{
    public class Position
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
    }
}
