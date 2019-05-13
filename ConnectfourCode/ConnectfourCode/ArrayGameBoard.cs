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
        public int[,] gameboard = new int[7, 6];
        int moveCount = 0;
        int[] columnHeight = { 0, 0, 0, 0, 0, 0, 0 };
        Stack<Tuple<int, int>> moveHistory = new Stack<Tuple<int, int>>();
        public List<List<Tuple<int, int>>> boardCheckLocations;

        Dictionary<int, int> knownScores;

        public ArrayGameBoard()
        {
            ResetGame();
            knownScores = ControlFile.ScoreCombinations.GetDictionaryOfCombinationsAndScoresOfMoreSpanSizes(
                new Dictionary<int, int> { { 0, 0 }, { 1, 1 }, { 2, 4 }, { 3, 9 }, { 4, 1000 } }, new int[] { 4, 5, 6, 7 }, 4, new int[] { 0, 1, 3 }, '0', '1');
            boardCheckLocations = getSearchCoordinates(Properties.Resources.gameboardDirectionConfig);
        }

        public int MoveCount()
        {
            return moveHistory.Count();

        }
        private Dictionary<int, int> openScoreFileAndAddEntriesToDictionary(string inputFile)
        {
            Dictionary<int, int> returnDictionary = new Dictionary<int, int>();
            foreach (string line in inputFile.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries))
            {
                string[] splitStringBuffer = line.Split(' ');
                returnDictionary.Add(int.Parse(splitStringBuffer[0]), int.Parse(splitStringBuffer[1]));
            }
            return returnDictionary;
        }

        private List<List<Tuple<int, int>>> getSearchCoordinates(string searchLocation)
        {
            List<List<Tuple<int, int>>> returnList = new List<List<Tuple<int, int>>>();
            foreach(string set in searchLocation.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries))
            {
                List<Tuple<int, int>> bufferDirection = new List<Tuple<int, int>>();
                foreach (string coordinatePair in set.Split(new string[] { " " }, System.StringSplitOptions.RemoveEmptyEntries))
                {
                    string[] stringBuffer = coordinatePair.Split(',');
                    bufferDirection.Add(new Tuple<int, int> ( Int32.Parse(stringBuffer[0]), Int32.Parse(stringBuffer[1]) ));
                }
                returnList.Add(bufferDirection);
            }

            return returnList;
        }

        public bool GetCurrentPlayer()
        {
            return moveCount % 2 == 0;
        }

        public void MakeMove(int coloumnInput)
        {
            Tuple<int, int> latestTuple = new Tuple<int, int>(coloumnInput, (columnHeight[coloumnInput]));
            moveHistory.Push(latestTuple);
            gameboard[latestTuple.Item1, latestTuple.Item2] = moveCount % 2;

            moveCount++; (columnHeight[latestTuple.Item1])++;
        }

        public void UndoMove()
        {
                Tuple<int, int> latestTuple = moveHistory.Pop();
                gameboard[latestTuple.Item1, latestTuple.Item2] = 0;
                moveCount--; (columnHeight[latestTuple.Item1])--;
        }

        protected List<int> possibleMoves()
        {
            List<int> returnList = new List<int>();
            for(int i = 0; i < 7; i++)
            {
                if(columnHeight[i] <= 5)
                {
                    returnList.Add(i);
                }
            }
            return returnList;
        }

        public void ResetGame()
        {
            for (int i = 0; i < gameboard.GetLength(0); i++)
            {
                for (int j = 0; j < gameboard.GetLength(1); j++)
                {
                    gameboard[i, j] = 0;
                }
            }
            moveHistory = new Stack<Tuple<int, int>>();
        }

        public int GetBoardKey()
        {
            return gameboard.GetHashCode();
        }

        override public int GetHashCode()
        {
            int buffer = 0;
            foreach (int i in gameboard)
            {
                buffer += i.GetHashCode();
            }
            return buffer;
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

        public bool IsWin()
        {
            int g = EvaluateBoard();
            return g >= 1000 || g <= -1000;
        }


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
            }
            return returnValue;
        }
        //public int EvaluateBoard()
        //{
        //    int evaluationBuffer = 0, dictionaryLookup = 0;
        //    bool wasFound = false;


        //    //scan horizontal
        //    for (int i = 0; i < 6; i++)
        //    {
        //        wasFound = knownScores.TryGetValue(30000000 + gameboard[0, i] * 1000000 + gameboard[1, i] * 100000 +
        //            gameboard[2, i] * 10000 + gameboard[3, i] * 1000 + gameboard[4, i] * 100 +
        //            gameboard[5, i] * 10 + gameboard[6, i] * 1, 
        //            out dictionaryLookup);
        //        evaluationBuffer += wasFound ? dictionaryLookup : 0;
        //    }

        //    //scan vertical
        //    for (int i = 0; i < 7; i++)
        //    {
        //        knownScores.TryGetValue(3000000 + gameboard[i, 0] * 100000 + gameboard[i, 1] * 10000 +
        //            gameboard[i, 2] * 1000 + gameboard[i, 3] * 100 + gameboard[i, 4] * 10 +
        //            gameboard[i, 5] * 1, 
        //            out dictionaryLookup);
        //        evaluationBuffer += wasFound? dictionaryLookup : 0;
        //    }
        //    //scan diagonal 1
        //    wasFound = knownScores.TryGetValue(
        //        30000 + gameboard[0, 3] * 1000 + gameboard[1, 2] * 100 + gameboard[2, 1] * 10 + gameboard[3, 0] * 1, 
        //        out dictionaryLookup);
        //    evaluationBuffer += wasFound ? dictionaryLookup : 0;

        //    wasFound = knownScores.TryGetValue(
        //        300000 + gameboard[0, 4] * 10000 + gameboard[1, 3] * 1000 + gameboard[2, 2] * 100 + gameboard[3, 1] * 10 + gameboard[4, 0] * 1, 
        //        out dictionaryLookup);
        //    evaluationBuffer += wasFound ? dictionaryLookup : 0;

        //    wasFound = knownScores.TryGetValue(
        //        3000000 + gameboard[0, 5] * 100000 + gameboard[1, 4] * 10000 + gameboard[2, 3] * 1000 + gameboard[3, 2] * 100 + gameboard[4, 1] * 10 + gameboard[5, 0] * 1, 
        //        out dictionaryLookup);
        //    evaluationBuffer += wasFound ? dictionaryLookup : 0;

        //    wasFound = knownScores.TryGetValue(
        //        3000000 + gameboard[1, 5] * 100000 + gameboard[2, 4] * 10000 + gameboard[3, 3] * 1000 + gameboard[4, 2] * 100 + gameboard[5, 1] * 10 + gameboard[6, 0] * 1, 
        //        out dictionaryLookup);
        //    evaluationBuffer += wasFound ? dictionaryLookup : 0;

        //    wasFound = knownScores.TryGetValue(
        //        300000 + gameboard[2, 5] * 10000 + gameboard[3, 4] * 1000 + gameboard[4, 3] * 100 + gameboard[5, 2] * 10 + gameboard[6, 1] * 1, 
        //        out dictionaryLookup);
        //    evaluationBuffer += wasFound ? dictionaryLookup : 0;

        //    wasFound = knownScores.TryGetValue(
        //        30000 + gameboard[3, 5] * 1000 + gameboard[4, 4] * 100 + gameboard[5, 3] * 10 + gameboard[6, 2] * 1,
        //        out dictionaryLookup);
        //    evaluationBuffer += wasFound ? dictionaryLookup : 0;

        //    //scan diagonal 2
        //    wasFound = knownScores.TryGetValue(
        //        30000 + gameboard[3, 5] * 1000 + gameboard[2, 4] * 100 + gameboard[1, 3] * 10 + gameboard[0, 2] * 1, 
        //        out dictionaryLookup);
        //    evaluationBuffer += wasFound ? dictionaryLookup : 0;

        //    wasFound = knownScores.TryGetValue(
        //        300000 + gameboard[4, 5] * 10000 + gameboard[3, 4] * 1000 + gameboard[2, 3] * 100 + gameboard[1, 2] * 10 +  gameboard[0, 1] * 1, 
        //        out dictionaryLookup);
        //    evaluationBuffer += wasFound ? dictionaryLookup : 0;

        //    wasFound = knownScores.TryGetValue(
        //        3000000 + gameboard[5, 5] * 100000 + gameboard[4, 4] * 10000 + gameboard[3, 3] * 1000 + gameboard[2, 2] * 100 + gameboard[1, 1] * 10 + gameboard[0, 0] * 1,
        //        out dictionaryLookup);
        //    evaluationBuffer += wasFound ? dictionaryLookup : 0;

        //    wasFound = knownScores.TryGetValue(
        //        3000000 + gameboard[6, 5] * 100000 + gameboard[5, 4] * 10000 + gameboard[4, 3] * 1000 + gameboard[3, 2] * 100 + gameboard[2, 1] * 10 + gameboard[1, 0] * 1, 
        //        out dictionaryLookup);
        //    evaluationBuffer += wasFound ? dictionaryLookup : 0;

        //    wasFound = knownScores.TryGetValue(
        //        300000 + gameboard[6, 4] * 10000 + gameboard[5, 3] * 1000 + gameboard[4, 2] * 100 + gameboard[3, 1] * 10 + gameboard[2, 0] * 1, 
        //        out dictionaryLookup);
        //    evaluationBuffer += wasFound ? dictionaryLookup : 0;

        //    wasFound = knownScores.TryGetValue(
        //        30000 + gameboard[6, 3] * 1000 + gameboard[5, 2] * 100 + gameboard[4, 1] * 10 + gameboard[3, 0] * 1, 
        //        out dictionaryLookup);
        //    evaluationBuffer += wasFound ? dictionaryLookup : 0;

        //    return evaluationBuffer;
        //}
    }
}
