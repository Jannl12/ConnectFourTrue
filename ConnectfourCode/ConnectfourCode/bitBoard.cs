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

        public ulong createHashCode()
        {
            ulong bufferLong = 0; 
            foreach(ulong inputLong in bitGameBoard)
            {
                bufferLong ^= inputLong;
            }
            return bufferLong + bitGameBoard[moveCount & 1];
        }

        public int EvaluateBoard(int player) //TODO: Fix brikker uden kontinuerlig sammenhæng og lav de fire for løkker om til en løkke H&M
        {
            ulong emptySlotsBitBoard = ulong.MaxValue ^ (bitGameBoard[0] | bitGameBoard[1]);
            ulong bitboard = bitGameBoard[0];
            int returnValue = int.MinValue;

            //Check for four connected.
            for (int i = 0; i < directions.Length; i++)
            {
                if ((bitboard & (bitboard >> directions[i]) & (bitboard >> (2 * directions[i])) &
                        (bitboard >> (3 * directions[i]))) != 0)
                {
                     returnValue = int.MaxValue;
                }
                 else if ((bitboard & (bitboard >> directions[i]) & (bitboard >> (2 * directions[i])) &
                        (emptySlotsBitBoard >> (3 * directions[i]))) != 0)
                {
                    returnValue = returnValue > 9 ? returnValue : 9;
                }
                else if ((bitboard & (bitboard >> directions[i]) & (emptySlotsBitBoard >> (2 * directions[i])) &
                        (emptySlotsBitBoard >> (3 * directions[i]))) != 0)
                {
                    returnValue = returnValue > 4 ? returnValue : 4; ;
                }
                else if ((bitboard & (emptySlotsBitBoard >> directions[i]) & (emptySlotsBitBoard >> (2 * directions[i])) &
                        (emptySlotsBitBoard >> (3 * directions[i]))) != 0)
                {
                    returnValue = returnValue > 1 ? returnValue : 1; ;
                }
            }
            //Check for three connected and space for the possibility of adding a fourth.
            return returnValue;
        }
    }
}
