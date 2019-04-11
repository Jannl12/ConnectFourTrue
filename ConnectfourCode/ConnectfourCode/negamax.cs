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
        public int thisIsMaxDepth = 9;
        int[] turnArray = { 3, 2, 4, 1, 5, 0, 6 };
        

        private const int height = 6, width = 7;       
        public int NegaMax(BitBoard node, int alpha, int beta, int maxDepth, int color)

            //TODO: Skal med i implementeringen
        {
            //TODO: Lav eventuelt nyt bitboard hver gang funktionen kaldes. JAN OG MAYOH
            if (node.IsWin() || maxDepth == 0)
            {
                //ConsoleManager.Show();
                //Console.WriteLine("test");
                int test =node.EvaluateBoard() * color;
                /*using (System.IO.StreamWriter file =
                    new System.IO.StreamWriter(@"C:\Users\jan_n\Desktop\GameTreeData.txt", true))
                    
                {
                    foreach (int i in moveHistory)
                        file.Write(i + ",");
                    file.WriteLine(test);
                    //file.AutoFlush = true;
                }*/
                    return node.EvaluateBoard() * color; // TODO: Add heuristic score JAN OG MAYOH
            }
            //for(int i = 6; i >= 0; i--)
            //foreach(int i in turnArray)
            for(int i = 0; i < 7; i++)
            {
                node.MakeMove(i);
                if (node.CanPlay(i))
                {

                    int value = -NegaMax(node, -beta, -alpha, maxDepth - 1, -color);

                    if (value >= beta)
                    {
                        node.UndoMove();
                        return value;
                    }
                    if (value > alpha)
                    {
                        alpha = value;
                        if(thisIsMaxDepth == maxDepth)
                            bestMove = i;
                    }
                }                
                node.UndoMove();
            }
            return alpha;
        }        
    }
}
