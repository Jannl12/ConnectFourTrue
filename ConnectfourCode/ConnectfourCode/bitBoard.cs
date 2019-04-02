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
        ulong[] BitGameBoard = { 0, 0 };
        int[] columnHeight = new int[7];
        List<int> moveHistory = new List<int>();
        ulong[,] zArray = new ulong[49, 2];
        ulong zobristKey = 0;

        public void randomZobristNumbers(ulong[,] zArray)
        {
            for (int i = 0; i < 49; i++)
            {
                zArray[i, 0] = Get64BitRandom(ulong.MinValue, ulong.MaxValue);
                zArray[i, 1] = Get64BitRandom(ulong.MinValue, ulong.MaxValue);
            }
        }

        private readonly Random rnd = new Random(); // create it just once and reuse

        private ulong Get64BitRandom(ulong minValue, ulong maxValue)
        {
            // Get a random array of 8 bytes. 
            // As an option, you could also use the cryptography namespace stuff to generate a random byte[8]
            byte[] buffer = new byte[sizeof(ulong)];
            rnd.NextBytes(buffer);
            return BitConverter.ToUInt64(buffer, 0) % (maxValue - minValue + 1) + minValue;
        }
        /// https://social.msdn.microsoft.com/Forums/vstudio/en-US/cb9c7f4d-5f1e-4900-87d8-013205f27587/64-bit-strong-random-function?forum=csharpgeneral
        /// burde måske omskrives til noget der ligner det her
        /// https://stackoverflow.com/questions/677373/generate-random-values-in-c-sharp

        public ulong GetZobristHash(ulong [,] zArray, ulong[] bitGameBoard)
        {
            ulong zkey = 0; /// hash return value

            /// for each position in the bitboard, if there is a 1 present, 
            /// the corresponding random value (generated in method randomZobristNumbers)
            /// will be XOR'ed and assigned to hash return value zkey
            for (int position = 0; position < 49; position++)
            {
                if (((bitGameBoard[0] >> position) & 1) == 1)
                {
                    zkey ^= zArray[position, 0];
                }
                else if((((bitGameBoard[1] >> position) & 1) == 1))
                {
                    zkey ^= zArray[position, 1];
                }                
            }

            return zkey;
        }

        public void makeMove(int coloumnInput, int moveInput)
        {   
            ulong moveBuffer = 1UL << columnHeight[coloumnInput]++;
            Console.WriteLine("here");
            BitGameBoard[(moveInput & 1)] ^= moveBuffer;
            moveHistory.Add(coloumnInput);
            this.zobristKey = GetZobristHash(this.zArray, this.BitGameBoard);
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
