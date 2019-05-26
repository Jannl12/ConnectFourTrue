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
        ulong hashCode = 0;

        Dictionary<int, int> knownScores;

        public int moveCount
        {
            get { return moveHistory.Count(); }
        }

        public ArrayGameBoard()
        {
            ResetGame();
            knownScores = ControlFile.ScoreCombinations.GetDictionaryOfCombinationsAndScoresOfMoreSpanSizes(
                new Dictionary<int, int> { { 0, 0 }, { 1, 0 }, { 2, 1 }, { 3, 4 }, { 4, 1000 } },
                new int[] { 4, 5, 6, 7 }, 4, new char[] { '0', '1', '2' }, '0', '1');

            boardCheckLocations = GetSearchCoordinates(Properties.Resources.gameboardDirectionConfig);
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

        public int GetCurrentPlayer()
        {
            return moveCount % 2;
        }

        public int GetPreviousPlayer()
        {
            return (moveCount + 1) % 2;
        }

        public void MakeMove(int coloumnInput)
        {
            Tuple<int, int> latestTuple = new Tuple<int, int>((columnHeight[coloumnInput]), coloumnInput);
            gameboard[latestTuple.Item1, latestTuple.Item2] = GetCurrentPlayer() + 1;
            moveHistory.Push(latestTuple);    
            columnHeight[latestTuple.Item2]++;

        }

        public void UndoMove()
        {
            if (moveHistory.Count() != 0)
            {
                Tuple<int, int> latestTuple = moveHistory.Pop();
                gameboard[latestTuple.Item1, latestTuple.Item2] = 0;
                columnHeight[latestTuple.Item2]--;
            }
        }

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

        public void ResetGame()
        {
            gameboard = new int[6, 7];
            moveHistory = new Stack<Tuple<int, int>>();
            columnHeight = new int[7];
        }

        public int GetBoardKey()
        {
            int hashCode = gameboard.Length;
            
            foreach(int i in gameboard)
            {
                hashCode = unchecked(hashCode * 17 + i);
            }
            return hashCode;
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

        protected bool IsWin(out int returnBoardEvaluation)
        {
            returnBoardEvaluation = EvaluateBoard();
            return returnBoardEvaluation >= 1000;
        }

        public bool IsWin()
        {
            return EvaluateBoard() == 1000;
        }

        public bool IsDraw()
        {
            return columnHeight.Sum() == 42;
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
                if (lookupValueBuffer == 1000 || lookupValueBuffer == -1000)
                    return 1000; //TODO: Make "Global" win value
            }
            return GetCurrentPlayer() == 0 ?  returnValue : returnValue * -1;
        }
    }
}
