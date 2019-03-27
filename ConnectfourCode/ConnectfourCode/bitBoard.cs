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
        int[] columnHeight = { 0, 0, 0, 0, 0, 0, 0 };
        List<int> moveHistory = new List<int>();

        public void makeMove(int coloumnInput, int moveInput)
        {
            long moveBuffer = 1L << columnHeight[coloumnInput]++;
            BitGameBoard[moveInput % 2] ^= moveBuffer;
            moveHistory.Add(coloumnInput);
        }

        public bool isWin(int moves)
        {
            long bitboard = BitGameBoard[moves % 2];
            if ((bitboard & (bitboard >> 6) & (bitboard >> 12) & (bitboard >> 18)) != 0) return true; // diagonal \
            if ((bitboard & (bitboard >> 8) & (bitboard >> 16) & (bitboard >> 24)) != 0) return true; // diagonal /
            if ((bitboard & (bitboard >> 7) & (bitboard >> 14) & (bitboard >> 21)) != 0) return true; // horizontal
            if ((bitboard & (bitboard >> 1) & (bitboard >> 2) & (bitboard >> 3)) != 0) return true; // vertical
            return false;
        }
    }
}
