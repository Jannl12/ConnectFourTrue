using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
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
        [DllImport("EvaluateBoardDLL.dll")]
        private static extern int EvaluateBoard(ulong board1, ulong board2, int[] inputvalues);

        public int EvaluateBoardDLL()
        {
            return EvaluateBoard(bitGameBoard[0], bitGameBoard[1], new int[] { 0, 1, 4, 9, 1000 });
        }

        public ulong[] bitGameBoard;
        public int[] columnHeight;

        protected List<int> moveHistory = new List<int>();
        int boardHeight = 6 - 1, boardWidth = 7, moveCount;

        public int MoveCount
        {
            get { return moveCount; }
        }
        private int Three1 = 4;//75;//4
        private int Two1 = 1;//15;//1
        private int One1 = 0;//8;//0

        int[] directions = { 1, 7, 6, 8 }; //Vertikal, Horizontal, V.Diagonal, H.Diagonal

        public BitBoard()
        {
            ResetGame();
        }
        public void MakeMove(int coloumnInput)
        {
            ulong moveBuffer = 1UL << columnHeight[coloumnInput]++;
            bitGameBoard[(moveCount++ & 1)] ^= moveBuffer;
            moveHistory.Add(coloumnInput);

        }


        public bool GetCurrentPlayer()
        {
            return MoveCount % 2 == 0;
        }

        public void UndoMove()
        {
            ulong moveBuffer = 1UL << --columnHeight[moveHistory.Last()];
            moveHistory.RemoveAt(moveHistory.Count - 1);
            bitGameBoard[(--moveCount & 1)] ^= moveBuffer;
        }

        public bool CanPlay(int column)
        {
            ulong mask = 1;
            ulong boardstate = bitGameBoard[0] ^ bitGameBoard[1];
            if (((boardstate >> ((column * boardWidth) + boardHeight)) & mask) == mask)
                return false;
            else return true;
        }

        public void ResetGame()
        {
            bitGameBoard = new ulong[2];
            bitGameBoard[0] = 0; bitGameBoard[1] = 0;

            columnHeight = new int[boardWidth];
            for (int i = 0; i < columnHeight.Length; i++)
            {
                columnHeight[i] = i * boardWidth;
            }
            moveHistory = new List<int>();
            moveCount = 0;
            moveHistory.Clear();
        }

        public bool IsWin()
        { //TODO: Should be described in Implemention, use figur
            ulong bitboard = bitGameBoard[(moveCount-1) % 2];
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


        protected List<int> possibleMoves()
        {
            List<int> returnList = new List<int>();
            int[] turnArray = { 3, 2, 4, 1, 5, 0, 6 };
            foreach (int i in turnArray)
            {
                if ((((bitGameBoard[0] ^ bitGameBoard[1]) >> ((i * boardWidth) + boardHeight)) & 1UL) != 1UL)
                {
                    returnList.Add(i);
                }
            }
            return returnList;
        }

        public ulong GetBoardKey()
        {
            ulong buffer = 0;
            foreach (ulong inputlong in this.bitGameBoard)
            {
                buffer ^= inputlong;
            }
            return buffer + bitGameBoard[moveCount & 1];
        }

        public bool IsDraw()
        {
            int[] frameRemover = { 6, 13, 20, 27, 34, 41, 48, 49, 50, 51, 52,
                                  53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64 };
            ulong outerFrameBuffer = 0;
            foreach (int frameIn in frameRemover)
            {
                outerFrameBuffer += (ulong)(Math.Pow(2, frameIn));
            }
            if ((bitGameBoard[0] | bitGameBoard[1]) == (ulong.MaxValue ^ outerFrameBuffer))
                return true;
            else
                return false;
        }




        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                return this.GetHashCode() == obj.GetHashCode();
            }
        }

        public int EvaluateBoard() //TODO: Fix brikker uden kontinuerlig sammenhæng og lav de fire for løkker om til en løkke H&M

        {
            // Frame: 6, 13, 20, 27, 34, 41, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64 };
            ulong outerFrameBuffer = 0xFFFF020408102040; //11111_1111111_1000000_1000000_1000000_1000000_1000000_1000000_1000000UL / 18446464815071240256

            ulong emptySlotsBitBoard = (ulong.MaxValue ^ (bitGameBoard[0] | bitGameBoard[1])) ^ outerFrameBuffer;
            ulong[] bitboard = bitGameBoard;
            int[] returnValue = { 0, 0 };

            //returnValue[(moveCount -1 ) & 1] = 10 - ((moveHistory[0] + 1) % 4)*2;


            int win = 10000;

            if (IsWin())
                return (MoveCount-1)%2 == 0 ?  win -MoveCount : -2*win - MoveCount;
            else if (IsDraw())
                return 0;
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    //1111
                    //returnValue[i] = Eval(bitboard[i], bitboard[i], bitboard[i], bitboard[i], win);
                    //111x
                    returnValue[i] += Eval(bitboard[i], bitboard[i], bitboard[i], emptySlotsBitBoard, Three1);

                    //11x1
                    returnValue[i] += Eval(bitboard[i], bitboard[i], emptySlotsBitBoard, bitboard[i], Three1);

                    //1x11
                    returnValue[i] += Eval(bitboard[i], emptySlotsBitBoard, bitboard[i], bitboard[i], Three1);

                    //x111
                    returnValue[i] += Eval(emptySlotsBitBoard, bitboard[i], bitboard[i], bitboard[i], Three1);

                    //11xx
                    returnValue[i] += Eval(bitboard[i], bitboard[i], emptySlotsBitBoard, emptySlotsBitBoard, Two1);

                    //1x1x
                    returnValue[i] += Eval(bitboard[i], emptySlotsBitBoard, bitboard[i], emptySlotsBitBoard, Two1);

                    //1xx1
                    returnValue[i] += Eval(bitboard[i], emptySlotsBitBoard, emptySlotsBitBoard, bitboard[i], Two1);

                    //x11x
                    returnValue[i] += Eval(emptySlotsBitBoard, bitboard[i], bitboard[i], emptySlotsBitBoard, Two1);

                    //x1x1
                    returnValue[i] += Eval(emptySlotsBitBoard, bitboard[i], emptySlotsBitBoard, bitboard[i], Two1);

                    //xx11
                    returnValue[i] += Eval(emptySlotsBitBoard, emptySlotsBitBoard, bitboard[i], bitboard[i], Two1);

                    //1xxx
                    returnValue[i] += Eval(bitboard[i], emptySlotsBitBoard, emptySlotsBitBoard, emptySlotsBitBoard, One1);

                    //x1xx
                    returnValue[i] += Eval(emptySlotsBitBoard, bitboard[i], emptySlotsBitBoard, emptySlotsBitBoard, One1);

                    //xx1x
                    returnValue[i] += Eval(emptySlotsBitBoard, emptySlotsBitBoard, bitboard[i], emptySlotsBitBoard, One1);

                    //xxx1
                    returnValue[i] += Eval(emptySlotsBitBoard, emptySlotsBitBoard, emptySlotsBitBoard, bitboard[i], One1);
                }
            }

            return returnValue[0] - returnValue[1];
        }
        private int Eval(ulong b1, ulong b2, ulong b3, ulong b4, int score)
        {
            int retval = 0;
            for (int i = 0; i < directions.Length; i++)
            {
                ulong andBitBoards = (b1 & (b2 >> directions[i]) & (b3 >> (2 * directions[i]))
                    & (b4 >> (3 * directions[i])));
                if (andBitBoards != 0)
                {
                    retval += score * CountSetBits(andBitBoards);
                }
            }
            return retval;
        }

        public int CountSetBits(ulong x)
        {
            int count = 0;
            while (x > 0)
            {
                if ((x & 1) == 1)
                    count++;
                x >>= 1;
            }
            return count;
        }
    }

}
