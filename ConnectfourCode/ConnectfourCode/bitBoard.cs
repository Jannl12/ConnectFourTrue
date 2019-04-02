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
    public class BitBoard
        //TODO: Add moveCounter to BitBoard Class
    {
        public ulong[] bitGameBoard = { 0, 0 };
        int[] columnHeight = new int[7]; 
        List<int> moveHistory = new List<int>();
        int height = 6;
        int width = 7;
        int[] directions = { 1, 7, 6, 8 };

        public void MakeMove(int coloumnInput, int moveInput)
        {   
                ulong moveBuffer = 1UL << columnHeight[coloumnInput]++;
                bitGameBoard[(moveInput & 1)] ^= moveBuffer;
                moveHistory.Add(coloumnInput); // TODO: Find out how we are going to use movehistory
        }

        public void UndoMove(int coloumnInput, int moveInput)
        {
            ulong moveBuffer = 1UL << --columnHeight[coloumnInput];
            bitGameBoard[(moveInput & 1)] ^= moveBuffer;
        }

        public bool CanPlay( int coulumn)
        {
            ulong mask = 1;
            ulong boardstate = bitGameBoard[0] ^ bitGameBoard[1];
            if (((boardstate >> ((coulumn * width) + height)) & mask) == mask)
                return false;
            else return true;
        }

        public void ResetBitBoard ()     // TODO: Make constructor for bitboard HVID
        {
            bitGameBoard[0] = 0;
            bitGameBoard[1] = 0;
            for(int i = 0; i < columnHeight.Length; i++)
            {
                columnHeight[i] = i * width;
            }
        }

        public bool IsWin(int moves)
        { //TODO: Should be described in Implemention, use figur
            ulong bitboard = bitGameBoard[moves & 1];
            for (int i = 0; i < directions.Length; i++)
            {
                if ((bitboard & (bitboard >> directions[i]) & (bitboard >> (2 * directions[i])) & 
                        (bitboard >> (3 * directions[i]))) != 0)
                {
                    return true;
                }
            }
            return false;
        }

        public int EvaluateBoard(int moves) //TODO: Fix brikker uden kontinuerlig sammenhæng og lav de fire for løkker om til en løkke H&M
        {
            ulong emptySlotsBitBoard = ulong.MaxValue ^ (bitGameBoard[0] | bitGameBoard[1]);
            ulong bitboard = bitGameBoard[moves & 1];
            

            //Check for four connected.
            for (int i = 0; i < directions.Length; i++)
            {
                if ((bitboard & (bitboard >> directions[i]) & (bitboard >> (2 * directions[i])) &
                        (bitboard >> (3 * directions[i]))) != 0)
                {
                    return int.MaxValue;
                }
            }
            //Check for three connected and space for the possibility of adding a fourth.
            for (int i = 0; i < directions.Length; i++)
            {
                if ((bitboard & (bitboard >> directions[i]) & (bitboard >> (2 * directions[i])) &
                        (emptySlotsBitBoard >> (3 * directions[i]))) != 0)
                {
                    return 9;
                }
            }

            //Check for two connected and empty slots for adding the third and fourth.
            for (int i = 0; i < directions.Length; i++)
            {
                if ((bitboard & (bitboard >> directions[i]) & (emptySlotsBitBoard >> (2 * directions[i])) &
                        (emptySlotsBitBoard >> (3 * directions[i]))) != 0)
                {
                    return 4;
                }
            }
            return int.MinValue;
        }
    }
}
