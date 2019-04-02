using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectfourCode
{
    class TranspositionTable
    {
        private int max = 10000;
        private int key = 0;

        ///make 2d array of random numbers.
        int[] yBitBoard = new int[42];
        int[] rBitBoard = new int[42];

        public void randomZobristNumbers(int[] yBitBoard, int[] rBitBoard)
        {
            Random randNum = new Random();

            for (int i = 0; i < 42; i++)
            {
                yBitBoard[i] = randNum.Next(max);
                rBitBoard[i] = randNum.Next(max);
            }
        }

        public int GenerateZobristKey(ulong[] gameBitBoard)
        {
            for (int i = 0; i < 42; i++)
            {
                

            }


            return key;
        }
        
    }
}
