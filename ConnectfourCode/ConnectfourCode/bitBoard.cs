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
    public delegate int Score(int x);
    public class BitBoard
    {
        public ulong[] bitGameBoard;
        public int[] columnHeight;
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
            bitGameBoard[(--moveCount & 1)] ^= moveBuffer;
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
            foreach (int frameIn in frameRemover)
            {
                outerFrameBuffer += (ulong)(Math.Pow(2, frameIn));
            }

            ulong emptySlotsBitBoard = (ulong.MaxValue ^ (bitGameBoard[0] | bitGameBoard[1])) ^ outerFrameBuffer;
            ulong[] bitboard = bitGameBoard;//[0];
            int[] returnValue = { 0, 0};

            Score Three1 = v => v + 9;
            Score Two1 = v => v + 4;
            Score One1 = v => v + 1;

            if (IsWin()) //Check for four connected.
            {
                return 10000;
            }
            else
            {
                for(int i = 0; i < 2; i++)
                {
                    //111x
                    returnValue[i]+= Eval(bitboard[i], bitboard[i], bitboard[i], emptySlotsBitBoard, Three1);

                    //11x1
                    returnValue[i] += Eval(bitboard[i], bitboard[i], emptySlotsBitBoard, bitboard[i], Three1);

                    //11xx
                    returnValue[i] += Eval(bitboard[i], bitboard[i], emptySlotsBitBoard, emptySlotsBitBoard, Two1);

                    //1x1x
                    returnValue[i] += Eval(bitboard[i], emptySlotsBitBoard, bitboard[i], emptySlotsBitBoard, Two1);

                    //1xx1
                    returnValue[i] += Eval(bitboard[i], emptySlotsBitBoard, emptySlotsBitBoard, bitboard[i], Two1);

                    //x11x
                    returnValue[i] += Eval(emptySlotsBitBoard, bitboard[i], bitboard[i], emptySlotsBitBoard, Two1);

                    //1xxx
                    returnValue[i] += Eval(bitboard[i], emptySlotsBitBoard, emptySlotsBitBoard, emptySlotsBitBoard, One1);

                    //x1xx
                    returnValue[i] += Eval(emptySlotsBitBoard, bitboard[i], emptySlotsBitBoard, emptySlotsBitBoard, One1);
                }
            }
            return returnValue[1] - returnValue[0];
        }
        private int Eval(ulong b1, ulong b2, ulong b3, ulong b4, Score score)
        {
            int retval = 0;
            for (int i = 0; i < directions.Length; i++)
            {
                if ((b1 & (b2 >> directions[i]) & (b3 >> (2 * directions[i])) &
                        (b4 >> (3 * directions[i]))) != 0)
                {
                    retval = score(retval); 
                }
            }
            return retval;
        }
    }   
}