using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectfourCode
{
    interface INegaMax
    {
        int NegaMax(int alpha, int beta, int maxDepth, int color, bool firstCall);
        int bestMove { get; set; }
    }
}
