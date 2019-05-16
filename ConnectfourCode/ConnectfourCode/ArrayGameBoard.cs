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
        private int[,] gameboard = new int[7, 6];
        int[] columnHeight = { 0, 0, 0, 0, 0, 0, 0 };
        Stack<Tuple<int, int>> moveHistory = new Stack<Tuple<int, int>>();
        public List<List<Tuple<int, int>>> boardCheckLocations;

        Dictionary<int, int> knownScores;

        protected int moveCount
        {
            get { return moveHistory.Count(); }
        }

        public ArrayGameBoard()
        {
            ResetGame();
            knownScores = ControlFile.ScoreCombinations.GetDictionaryOfCombinationsAndScoresOfMoreSpanSizes(
                new Dictionary<int, int>() { { 0, 0 }, { 1, 1 }, { 2, 4 }, { 3, 9 }, { 4, 1000 } }, 
                new int[] { 4, 5, 6, 7 }, 4, new int[] { 0, 1, 2 }, '0', '1');

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

            (columnHeight[latestTuple.Item1])++;
        }

        public void UndoMove()
        {
                Tuple<int, int> latestTuple = moveHistory.Pop();
                gameboard[latestTuple.Item1, latestTuple.Item2] = 0;
                (columnHeight[latestTuple.Item1])--;
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
    }
}
