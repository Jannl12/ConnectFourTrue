using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* The following, is based on information we learned through:
 * https://github.com/denkspuren/BitboardC4/blob/master/BitboardDesign.md
 */
namespace ConnectfourCode
{
    class bitBoard
    {
        long[] BitGameBoard = { 0, 0 };
        int[] columnHeight = { 0, 7, 14, 21, 28, 35, 42 };
        List<int> moveHistory = new List<int>();

        public void makeMove(int coloumnInput, int moveInput)
        {
            
            long moveBuffer = 1L << columnHeight[coloumnInput]++;
            BitGameBoard[moveInput & 1] ^= moveBuffer;
            moveHistory.Add(coloumnInput);
        }

        public void resetBoard ()       
        {
            BitGameBoard[0] = 0;
            BitGameBoard[1] = 0;
        }

        public bool isWin(int moves)
        {
            long bitboard = BitGameBoard[moves & 1];
            int[] directions = { 1, 7, 6, 8 };
            for (int i = 0; i < directions.Length; i++)
            {
                if ((bitboard & (bitboard >> directions[i]) & (bitboard >> (2 * directions[i])) & (bitboard >> (3 * directions[i]))) != 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
