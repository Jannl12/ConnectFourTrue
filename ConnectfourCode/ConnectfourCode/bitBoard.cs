﻿using System;
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
        ulong[] BitGameBoard = { 0, 0 };
        int[] columnHeight = new int[7];
        List<int> moveHistory = new List<int>();

        public void makeMove(int coloumnInput, int moveInput)
        {   
                ulong moveBuffer = 1UL << columnHeight[coloumnInput]++;
                Console.WriteLine("here");
                BitGameBoard[(moveInput & 1)] ^= moveBuffer;
                moveHistory.Add(coloumnInput);
        }

        public void resetBitBoard ()       
        {
            BitGameBoard[0] = 0;
            BitGameBoard[1] = 0;
            for(int i = 0; i < columnHeight.Length; i++)
            {
                columnHeight[i] = i * 7;
            }
        }

        public bool isWin(int moves)
        {
            ulong bitboard = BitGameBoard[moves & 1];
            int[] directions = { 1, 7, 6, 8 };
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

        public int evaluateBoard(int moves)
        {
            ulong emptySlotsBitBoard = ulong.MaxValue ^ (BitGameBoard[0] | BitGameBoard[1]);
            ulong bitboard = BitGameBoard[moves & 1];
            int[] directions = { 1, 7, 6, 8 };
            for (int i = 0; i < directions.Length; i++)
            {
                if ((bitboard & (bitboard >> directions[i]) & (bitboard >> (2 * directions[i])) &
                        (bitboard >> (3 * directions[i]))) != 0)
                {
                    return int.MaxValue;
                }
            }

            for (int i = 0; i < directions.Length; i++)
            {
                if ((bitboard & (bitboard >> directions[i]) & (bitboard >> (2 * directions[i])) &
                        (emptySlotsBitBoard >> (3 * directions[i]))) != 0)
                {
                    return 9;
                }
            }
            for (int i = 0; i < directions.Length; i++)
            {
                if ((bitboard & (bitboard >> directions[i]) & (emptySlotsBitBoard >> (2 * directions[i])) &
                        (emptySlotsBitBoard >> (3 * directions[i]))) != 0)
                {
                    return 4;
                }
            }
            for (int i = 0; i < directions.Length; i++)
            {
                if ((bitboard & (emptySlotsBitBoard >> directions[i]) & (emptySlotsBitBoard >> (2 * directions[i])) &
                        (emptySlotsBitBoard >> (3 * directions[i]))) != 0)
                {
                    return 1;
                }
            }
            return int.MinValue;
        }
    }
}