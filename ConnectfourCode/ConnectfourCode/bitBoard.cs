﻿using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/* The following, is based on information obtained through:
 * https://github.com/denkspuren/BitboardC4/blob/master/BitboardDesign.md
 */


namespace ConnectfourCode
{

    public class BitBoard
    {

        public ulong[] bitGameBoard;
        protected int[] columnHeight;


        public Stack<int> moveHistory = new Stack<int>();
        private int boardHeight = 6 - 1, boardWidth = 7;
        private int[] boardScores = { 0, 1, 4};
        private int[] directions = { 1, 7, 6, 8 };
  



        public int moveCount
        {
            get { return moveHistory.Count(); }
        }
        
        /**<summary><c>BitBoard</c> Constructor for the BitBoard class that calls the <paramref name="ResetGame"/> method.</summary>
        */
        public BitBoard()
        {
            ResetGame();
        }
        /**<summary><c>MakeMove</c> Makes a move for the given player based on <paramref name="columnInput"/> and saves the value in <paramref name="moveHistory"/>.</summary>
        */
        public void MakeMove(int columnInput)
        {
            ulong moveBuffer = 1UL << columnHeight[columnInput]++;
            bitGameBoard[GetCurrentPlayer()] ^= moveBuffer;
            moveHistory.Push(columnInput);
        }

        /**<summary><c>GetCurrentPlayer</c> Finds out which player is performing the current move</summary>
         */
        public int GetCurrentPlayer()
        {
            return moveCount % 2;
        }
        /**<summary><c>GetPrevoiusPlayer</c> Finds out which player is performing the current move</summary>
          */
        public int GetPreviousPlayer()
        {
            return (moveCount + 1) % 2;
        }

        /**<summary><c>UndoMove</c> undoes the move that the player did based on <paramref name="movehistory"/>.</summary>
         */
        public void UndoMove()
        {
            if (moveHistory.Count != 0)
            {
                ulong moveBuffer = 1UL << --columnHeight[moveHistory.Pop()];
                bitGameBoard[GetCurrentPlayer()] ^= moveBuffer;
            }
        }


        /**<summary><c>ResetGame</c>Resets the state of the board by:
         * <list type="bullet">
         *  <item>Setting values of <paramref name="bitGameBoard"/> to 0</item>
         *  <item>Setting the values of <paramref name="columnHeight"/> to values equal of the coloumn bottom integers of the ulongs.</item>
         *  <item>Clearing the values of <paramref name="moveHistory"/> .</list>
         * </list>
         * </summary>
         */
        public void ResetGame()
        {
            bitGameBoard = new ulong[2];
            bitGameBoard[0] = 0; bitGameBoard[1] = 0;

            columnHeight = new int[boardWidth];
            for (int i = 0; i < columnHeight.Length; i++)
            {
                columnHeight[i] = i * boardWidth;
            }
            moveHistory.Clear();
        }

        /**<summary><c>IsWin</c> checks if the previuos player won the game.</summary>
         * <returns>A bool showing if rhe previuos player one.</returns>
         */
        public bool IsWin()
        { 
            ulong bitboard = bitGameBoard[GetPreviousPlayer()]; 


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


        /**<summary><c>possibleMoves</c> creates a list of possible moves, based on which bits are set in
         * the ulongs of the <paramref name="bitGameBoard">.</paramref></summary>
         * <returns><c>List<int></c>A list of possible moves in the current state of the game.</returns>
         */
        protected List<int> PossibleMoves()
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


        /**<summary>Creates a unique key based on the current state of the ulongs in <paramref name="bitGameBoard"/>.</summary>
         * <returns>A hashcode based on the two hashcodes</returns>
         */
        public ulong GetBoardKey()
        {
            ulong buffer = 0;
            foreach (ulong inputlong in bitGameBoard)
            {
                buffer ^= inputlong;
            }
            return buffer + bitGameBoard[moveCount & 1];
        }

        /**<summary><c>IsDraw</c> tests if the gameboard is full, by comparing with a template(ulong) of a full board.</summary>
         * <retuns>A bool which describes if the board is full, and the game therefor is a draw.</retuns>
         */
        public bool IsDraw()
        {
            ulong outerFrameMask = 0xFFFF020408102040;// 1111111111111111000000100000010000001000000100000010000001000000
            //outerFrameMask contains a ulong with all bits, which are not in used flipped to 0.

            return ((bitGameBoard[0] | bitGameBoard[1]) == (ulong.MaxValue ^ outerFrameMask));
        }


        /** <summary> Function <c>int EvaluateBoard() </c> takes the current state of the board, and creates 
         * an value which represents a score of the board, for the computerplayer. </summary>
         * <returns> Returns the score of the given board as integer value. </returns>
         */
        public int EvaluateBoard()
        {
            ulong outerFrameBuffer = 0xFFFF020408102040;
            ulong emptySlotsBitBoard = (ulong.MaxValue ^ (bitGameBoard[0] | bitGameBoard[1])) ^ outerFrameBuffer;


            int win = 10000;

            if (IsWin())
            {
                return GetPreviousPlayer() == 0 ? win - moveCount : -win + moveCount;
            }
            else if (IsDraw())
            {
                return 0;
            }
            else
            {
                int returnValue = 0;
                for (int playerIterator = 0; playerIterator < bitGameBoard.Count(); playerIterator++)
                {
                    //Shows all combinations of 2 or 3 discs in a posible four in a row
                    ulong[,] allCombinations = {
				                            { bitGameBoard[playerIterator], bitGameBoard[playerIterator],   bitGameBoard[playerIterator],   emptySlotsBitBoard},            //111x
                                            { bitGameBoard[playerIterator], bitGameBoard[playerIterator],   emptySlotsBitBoard,             bitGameBoard[playerIterator]},  //11x1
                                            { bitGameBoard[playerIterator], emptySlotsBitBoard,             bitGameBoard[playerIterator],   bitGameBoard[playerIterator]},  //1x11
                                            { emptySlotsBitBoard,           bitGameBoard[playerIterator],   bitGameBoard[playerIterator],   bitGameBoard[playerIterator]},  //x111
							                { bitGameBoard[playerIterator], bitGameBoard[playerIterator],   emptySlotsBitBoard,             emptySlotsBitBoard},            //11xx
                                            { bitGameBoard[playerIterator], emptySlotsBitBoard,             bitGameBoard[playerIterator],   emptySlotsBitBoard},            //1x1x
                                            { bitGameBoard[playerIterator], emptySlotsBitBoard,             emptySlotsBitBoard,             bitGameBoard[playerIterator]},  //1xx1
                                            { emptySlotsBitBoard,           bitGameBoard[playerIterator],   bitGameBoard[playerIterator],   emptySlotsBitBoard},            //x11x 
                                            { emptySlotsBitBoard,           bitGameBoard[playerIterator],   emptySlotsBitBoard,             bitGameBoard[playerIterator]},  //x1x1
                                            { emptySlotsBitBoard,           emptySlotsBitBoard,             bitGameBoard[playerIterator],   bitGameBoard[playerIterator]} };//xx11
							          
                    int[] numberOfboardsInSpan = { 2, 2, 2, 2, 1, 1, 1, 1, 1, 1 };
                    for (int combination = 0; combination < allCombinations.GetLength(0); combination++)
                    {
                        int evaluationBuffer = FindCombinationAndGiveScore(allCombinations[combination, 0],
                                                            allCombinations[combination, 1],
                                                            allCombinations[combination, 2],
                                                            allCombinations[combination, 3],
                                                            boardScores[numberOfboardsInSpan[combination]]);
                        returnValue += playerIterator == 0 ? evaluationBuffer : -evaluationBuffer;
                    }
                }
                return returnValue;
            }
        }

        /** <summary><c>FindCombinationAndGiveScore</c> takes in four boards(ulongs) and calculates how many cases of the given
         * combination that exists. </summary>
         * <retuns>The score of the board for that given combination, based on a given score and found combinations.</retuns>
         */
        private int FindCombinationAndGiveScore(ulong firstBoard, ulong secondBoard, ulong thirdBoard, ulong fourthBoard, int score)
        {
            int returnValue = 0;
            for (int i = 0; i < directions.Count(); i++)
            {
                ulong boardShiftAndAdditionBuffer =
                    (firstBoard) &
                    (secondBoard >> (directions[i])) &
                    (thirdBoard >> (2 * directions[i])) &
                    (fourthBoard >> (3 * directions[i]));
                if (boardShiftAndAdditionBuffer != 0)
                {
                    returnValue += score * countSetBitsInUlong(boardShiftAndAdditionBuffer);
                }
            }
            return returnValue;
        }

        /**<summary>Takes in a <paramref name="inputValue"/> and counts the set bits in the bitstring.</summary>
         * <param name="inputValue">The ulong which bits will be counted.</param>
         * <returns>The number of bits in the input parameter.</returns>
         */
        private int countSetBitsInUlong(ulong inputValue)
        {
            int returnCount = 0;
            while (inputValue > 0)
            {
                if ((inputValue & 1) == 1)
                {
                    returnCount++;
                }
                inputValue >>= 1;
            }
            return returnCount;
        }
    }
}
