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
    {
        public ulong[] bitGameBoard;
        int[] columnHeight;
        List<int> moveHistory = new List<int>();
        int boardHeight = 6, boardWidth = 7, moveCount;
        public int MoveCount {
            get { return moveCount; }
            set { moveCount = value; }
        }

        int[] directions = { 1, 7, 6, 8 };

        public BitBoard()
        {
            ResetBitBoard();
        }
        public void MakeMove(int coloumnInput)
        {   
                ulong moveBuffer = 1UL << columnHeight[coloumnInput]++;
                bitGameBoard[(moveCount++ & 1)] ^= moveBuffer;
                moveHistory.Add(coloumnInput);

        }

        public void UndoMove()
        {
            ulong moveBuffer = 1UL << --columnHeight[moveHistory.Last()];
            moveHistory.RemoveAt(moveHistory.Count - 1);
            bitGameBoard[(moveCount-- & 1)] ^= moveBuffer;
        }

        public bool CanPlay( int coulumn)
        {
            ulong mask = 1;
            ulong boardstate = bitGameBoard[0] ^ bitGameBoard[1];
            if (((boardstate >> ((coulumn * boardWidth) + boardHeight)) & mask) == mask)
                return false;
            else return true;
        }

        public void ResetBitBoard()   
        {
            bitGameBoard = new ulong[2];
            bitGameBoard[0] = 0; bitGameBoard[1] = 0;

            columnHeight = new int[boardWidth];
            for(int i = 0; i < columnHeight.Length; i++)
            {
                columnHeight[i] = i * boardWidth;
            }
            moveCount = 0;
        }

        public bool IsWin()
        { //TODO: Should be described in Implemention, use figur
            ulong bitboard = bitGameBoard[moveCount - 1 & 1];
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

        public int EvaluateBoard() //TODO: Fix brikker uden kontinuerlig sammenhæng og lav de fire for løkker om til en løkke H&M
        {
            int[] frameRemover = { 6, 13, 20, 27, 34, 41, 48, 49, 50, 51, 52,
                                  53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64 };
            ulong outerFrameBuffer = 0;
            foreach(int frameIn in frameRemover)
            {
                outerFrameBuffer += (ulong)(Math.Pow(2, frameIn));
            }

            ulong emptySlotsBitBoard = (ulong.MaxValue ^ (bitGameBoard[0] | bitGameBoard[1])) ^ outerFrameBuffer;
            ulong bitboard = bitGameBoard[0];
            int returnValue = 0;
            ulong mask = 1;

            if (IsWin())
            {
                return int.MaxValue;
            }
                //Check for four connected.
            for (int i = 0; i < directions.Length; i++)
            {   //111x
                if ((bitboard & (bitboard >> directions[i]) & (bitboard >> (2 * directions[i])) &
                        (emptySlotsBitBoard >> (3 * directions[i]))) != 0)
                {
                    returnValue = Three1(returnValue);
                } //11x1
                if ((bitboard & (bitboard >> directions[i]) & (emptySlotsBitBoard >> (2 * directions[i])) &
                        (bitboard >> (3 * directions[i]))) != 0)
                {
                    returnValue = Three1(returnValue);
                } //11xx
                if ((bitboard & (bitboard >> directions[i]) & (emptySlotsBitBoard >> (2 * directions[i])) &
                        (emptySlotsBitBoard >> (3 * directions[i]))) != 0)
                {
                    returnValue = Two1(returnValue);

                } //1x1x
                if ((bitboard & (emptySlotsBitBoard >> directions[i]) & (bitboard >> (2 * directions[i])) &
                        (emptySlotsBitBoard >> (3 * directions[i]))) != 0)
                {
                    returnValue = Two1(returnValue);
                } //1xx1
                if ((bitboard & (emptySlotsBitBoard >> directions[i]) & (emptySlotsBitBoard >> (2 * directions[i])) &
                        (bitboard >> (3 * directions[i]))) != 0)
                {
                    returnValue = Two1(returnValue);
                } //x11x
                if ((emptySlotsBitBoard & (bitboard >> directions[i]) & (bitboard >> (2 * directions[i])) &
                        (emptySlotsBitBoard >> (3 * directions[i]))) != 0)
                {
                    returnValue = Two1(returnValue);
                } //1xxx
                if ((bitboard & (emptySlotsBitBoard >> directions[i]) & (emptySlotsBitBoard >> (2 * directions[i])) & (emptySlotsBitBoard >> (3 * directions[i]))) != 0)
                {
                    returnValue = One1(returnValue);
                } //x1xx
                if ((emptySlotsBitBoard & (bitboard >> directions[i]) & (emptySlotsBitBoard >> (2 * directions[i])) &
                        (emptySlotsBitBoard >> (3 * directions[i]))) != 0)
                {
                    returnValue = One1(returnValue);
                } 
            }
            return returnValue;
        }
        private int Three1(int val)
        {
            return val + 9;
        }
        private int Two1(int val)
        {
            return val + 4;
        }
        private int One1(int val)
        {
            return val + 1;
        }
            }
    
}
