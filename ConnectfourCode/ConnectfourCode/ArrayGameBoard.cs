using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConnectfourCode
{

    public class ArrayGameBoard
    {
        public int[,] gameboard = new int[6, 7];
        int[] columnHeight = new int[7];
        protected int initalPlayer = 0;

        Stack<Tuple<int, int>> moveHistory = new Stack<Tuple<int, int>>();
        public List<List<Tuple<int, int>>> boardCheckLocations;
        protected int winValue = 10000;
        Dictionary<int, int> knownScores;

        public int moveCount
        {
            get { return moveHistory.Count(); }
        }

        /**<summary><c>ArrayGameBoard</c> Constructor for the ArrayGameBoard class that calls the <paramref name="ResetGame"/> method. Furtmore it 
         * contains the variable <paramref name="knownScores"/> that is a dict of board values for different possible boardstates. Finally
         * the variable <paramref name="boardCheckLocations"/> is set, 
         * so it containes alle neassecary search coordinates.</summary>
    */
        public ArrayGameBoard()
        {
            ResetGame();
            knownScores = ControlFile.ScoreCombinations.GetDictionaryOfCombinationsAndScoresOfMoreSpanSizes(
                new Dictionary<int, int> { { 0, 0 }, { 1, 0 }, { 2, 1 }, { 3, 4 }, { 4, winValue } },
                4, new int[] { 4, 5, 6, 7 }, new char[] { '0', '1', '2' }, '0', '1');

            boardCheckLocations = GetSearchCoordinates(Properties.Resources.gameboardDirectionConfig);
        }

        /**<summary><c>GetSearchCoordinates</c> Takes the string <paramref name="searchLocation"/> as an input and creates a list of lists of tuples
         * where each tuple is a coordinate in the array, each list of tuples is the direction search the array, and each list of lists of tuples
         * are the row and directions to search .</summary>
        */
        private List<List<Tuple<int, int>>> GetSearchCoordinates(string searchLocation)
        {
            List<List<Tuple<int, int>>> returnList = new List<List<Tuple<int, int>>>();
            foreach(string coordinates in searchLocation.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                List<Tuple<int, int>> bufferDirection = new List<Tuple<int, int>>();
                foreach (string coordinatePair in coordinates.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries))
                {
                    string[] stringBuffer = coordinatePair.Split(',');
                    bufferDirection.Add(new Tuple<int, int> ( int.Parse(stringBuffer[0]), int.Parse(stringBuffer[1]) ));
                }
                returnList.Add(bufferDirection);
            }

            return returnList;
        }
        /**<summary><c>GetCurrentPlayer</c> Finds out which player is performing the current move</summary>
         */        public int GetCurrentPlayer()

        {
            return moveCount % 2;
        }
        /**<summary><c>GetPreviousPlayerol</c> Finds out which player is performing the current move</summary>
          */
        public int GetPreviousPlayer()
        {
            return (moveCount + 1) % 2;
        }
        /**<summary><c>MakeMove</c> Makes a move for the given player based on <paramref name="columnInput"/> and saves the value in <paramref name="moveHistory"/>.</summary>
        */
        public void MakeMove(int coloumnInput)
        {
            Tuple<int, int> latestTuple = new Tuple<int, int>((columnHeight[coloumnInput]++), coloumnInput);
            gameboard[latestTuple.Item1, latestTuple.Item2] = GetCurrentPlayer() + 1;
            moveHistory.Push(latestTuple);    
        }

        /**<summary><c>UndoMove</c> undoes the move that the player did based on <paramref name="movehistory"/>.</summary>
         */
        public void UndoMove()
        {
            if (moveHistory.Count() != 0)
            {
                Tuple<int, int> latestTuple = moveHistory.Pop();
                gameboard[latestTuple.Item1, latestTuple.Item2] = 0;
                columnHeight[latestTuple.Item2]--;
            }
        }
        /**<summary><c>possibleMoves</c> creates a list of possible moves, based on which columns in <paramref name="gameboard"> that are not full.</paramref></summary>
         * <returns><c>List<int></c>A list of possible moves in the current state of the game.</returns>
         */
        protected List<int> PossibleMoves()
        {
            List<int> returnList = new List<int>();
            int[] turnArray = { 3, 2, 4, 1, 5, 0, 6 };
            foreach (int i in turnArray)
            {
                if (gameboard[5, i] == 0)
                {
                    returnList.Add(i);
                }
            }
            return returnList;
        }
        /**<summary><c>ResetGame</c>Resets the state of the board by:
         * <list type="bullet">
         *  <item>Creating a new instance of <paramref name="gameboard"/> where all values are zero</item>
         *  <item>Setting the values of <paramref name="columnHeight"/> to vzero.</item>
         *  <item>Clearing the values of <paramref name="moveHistory"/> .</list>
         * </list>
         * </summary>
         */
        public void ResetGame()
        {
            gameboard = new int[6, 7];
            columnHeight = new int[7];
            moveHistory = new Stack<Tuple<int, int>>();
 
        }
        /**<summary>Creates a unique key based on the current state of the array in <paramref name="gameboard"/>.</summary>
         * <returns>A hashcode based the board state</returns>
         */
        public int GetBoardKey()
        {
            int hashCode = gameboard.Length;
            
            foreach(int i in gameboard)
            {
                hashCode = unchecked(hashCode * 17 + i);
            }
            return hashCode;
        }
        /**<summary><c>IsWin</c> checks if the previuos player won the game.</summary>
         * <overloads><see cref="IsWin(out int)"/></overloads>
         * <returns>A bool showing if rhe previuos player one.</returns>
         */
        public bool IsWin()
        {
            return EvaluateBoard() == winValue;
        }

        /**<summary><c>IsWin(int)</c> checks if the previuos player won the game.</summary>
         * <returns>A bool showing if rhe previuos player one, and the evaluation of the board in the argument <paramref name="returnBoardEvaluation"/>.</returns>
         */
        protected bool IsWin(out int returnBoardEvaluation)
        {
            returnBoardEvaluation = EvaluateBoard();
            return returnBoardEvaluation == winValue;
        }

        /**<summary><c>IsDraw</c> tests if the gameboard is full, by testing if the sum of the columns is 42 (6*7).</summary>
         * <retuns>A bool which describes if the board is full, and the game therefor is a draw.</retuns>
         */
        public bool IsDraw()
        {
            return columnHeight.Sum() == 42;
        }
        /** <summary> Function <c>int EvaluateBoard() </c> takes the current state of the board, and creates 
         * an value which represents a score of the board, for the computerplayer. </summary>
         * <returns> Returns the score of the given board as integer value. </returns>
         */
        public int EvaluateBoard()
        {
            int returnValue = 0, lookupValueBuffer;
            foreach(List<Tuple<int, int>> checkLocation in boardCheckLocations)
            {
                int i = checkLocation.Count();
                double lookupKeyBuffer = 3 * Math.Pow(10, i--);
                foreach(Tuple<int, int> coordinate in checkLocation)
                {
                    lookupKeyBuffer += gameboard[coordinate.Item1, coordinate.Item2] * Math.Pow(10 , i--);
                }
                returnValue += knownScores.TryGetValue(Convert.ToInt32(lookupKeyBuffer), out lookupValueBuffer) ? lookupValueBuffer : 0;
                if (lookupValueBuffer == winValue || lookupValueBuffer == -winValue)
                    return winValue; 
            }
            return GetCurrentPlayer() == 0 ?  returnValue : returnValue * -1;
        }
    }
}
