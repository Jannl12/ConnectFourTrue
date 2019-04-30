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
        int[,] gameboard = new int[7, 6];
        int moveCount = 0;
        int[] columnHeight = { 0, 0, 0, 0, 0, 0, 0 };
        Stack<Tuple<int, int>> moveHistory = new Stack<Tuple<int, int>>();

        Dictionary<int, int> sevenSlotsScores = new Dictionary<int, int>();
        Dictionary<int, int> sixSlotsScores = new Dictionary<int, int>();
        Dictionary<int, int> fiveSlotsScores = new Dictionary<int, int>();
        Dictionary<int, int> fourSlotsScores = new Dictionary<int, int>();

        public int MoveCount()
        {
            return moveHistory.Count();
        }

        public ArrayGameBoard()
        {
            ResetBitBoard();
            string[] splitStringBuffer;
            foreach (string line in File.ReadLines(@"..\..\..\ConnectfourCode\7C4.txt"))
            {
                splitStringBuffer = line.Split(' ');
                sevenSlotsScores.Add(int.Parse(splitStringBuffer[0]), int.Parse(splitStringBuffer[1]));
            }

            foreach (string line in File.ReadLines(@"..\..\..\ConnectfourCode\6C4.txt"))
            {
                splitStringBuffer = line.Split(' ');
                sixSlotsScores.Add(int.Parse(splitStringBuffer[0]), int.Parse(splitStringBuffer[1]));
            }

            foreach (string line in File.ReadLines(@"..\..\..\ConnectfourCode\5C4.txt"))
            {
                splitStringBuffer = line.Split(' ');
                fiveSlotsScores.Add(int.Parse(splitStringBuffer[0]), int.Parse(splitStringBuffer[1]));
            }
            foreach (string line in File.ReadLines(@"..\..\..\ConnectfourCode\4C4.txt"))
            {
                splitStringBuffer = line.Split(' ');
                fourSlotsScores.Add(int.Parse(splitStringBuffer[0]), int.Parse(splitStringBuffer[1]));
            }
        }

        public bool IsPlayerMove()
        {
            return moveCount % 2 == 0;
        }

        public void MakeMove(int coloumnInput)
        {
            if (CanPlay(coloumnInput) && columnHeight[coloumnInput] < 6)
            {
                Tuple<int, int> latestTuple = new Tuple<int, int>(coloumnInput, (columnHeight[coloumnInput]));
                moveHistory.Push(latestTuple);
                gameboard[latestTuple.Item1, latestTuple.Item2] = moveCount % 2;

                moveCount++; (columnHeight[coloumnInput])++;
            }
        }

        public void UndoMove()
        {
            if (moveHistory.Count > 0)
            {
                Tuple<int, int> latestTuple = moveHistory.Pop();
                gameboard[latestTuple.Item1, latestTuple.Item2] = 0;
                moveCount--; (columnHeight[moveHistory.Last().Item1])--;
            }
        }

        public bool CanPlay(int column)
        {
            if(gameboard[column, (gameboard.GetLength(1) - 1)] == 0)
            {
                return true;
            }
            return false;
        }

        public void ResetBitBoard()
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

        public ulong GetBoardKey()
        {
            ulong buffer = 0; 
            foreach(int i in gameboard)
            {
                buffer += Convert.ToUInt64(i.GetHashCode());
            }
            return buffer;
        }

        override public int GetHashCode()
        {
            int buffer = 0;
            foreach (int i in gameboard)
            {
                buffer = checked(buffer + i.GetHashCode());
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
            return EvaluateBoard() >= 1000 ? true : false;
        }

        public int EvaluateBoard()
        {
            int evaluationBuffer = 0, dictionaryLookup = 0;
            bool wasFound = false;

            //scan horizontal
            for (int i = 0; i < 6; i++)
            {
                wasFound = sixSlotsScores.TryGetValue((gameboard[0, i] * 1000000 + gameboard[1, i] * 100000 +
                    gameboard[2, i] * 10000 + gameboard[3, i] * 1000 + gameboard[4, i] * 100 +
                    gameboard[5, i] * 10 + gameboard[6, i] * 1).GetHashCode(), 
                    out dictionaryLookup);
                evaluationBuffer += wasFound ? dictionaryLookup : 0;
            }

            //scan vertical
            for (int i = 0; i < 7; i++)
            {
                sevenSlotsScores.TryGetValue((gameboard[i, 0] * 100000 + gameboard[i, 1] * 10000 +
                    gameboard[i, 2] * 1000 + gameboard[i, 3] * 100 + gameboard[i, 4] * 10 +
                    gameboard[i, 5] * 1).GetHashCode(), 
                    out dictionaryLookup);
                evaluationBuffer += wasFound? dictionaryLookup : 0;
            }
            //scan diagonal 1
            wasFound = fourSlotsScores.TryGetValue(
                (gameboard[0, 3] * 1000 + gameboard[1, 2] * 100 + gameboard[2, 1] * 10 + gameboard[3, 0] + 1).GetHashCode(), 
                out dictionaryLookup);
            evaluationBuffer += wasFound ? dictionaryLookup : 0;

            wasFound = fiveSlotsScores.TryGetValue(
                (gameboard[0, 4] * 10000 + gameboard[1, 3] * 1000 + gameboard[2, 2] * 100 + gameboard[3, 1] * 10 + gameboard[4, 0] + 1).GetHashCode(), 
                out dictionaryLookup);
            evaluationBuffer += wasFound ? dictionaryLookup : 0;

            wasFound = sixSlotsScores.TryGetValue(
                (gameboard[0, 5] * 100000 + gameboard[1, 4] * 10000 + gameboard[2, 3] * 1000 + gameboard[3, 2] + 100 + gameboard[4, 1] + 10 + gameboard[5, 0] + 1).GetHashCode(), 
                out dictionaryLookup);
            evaluationBuffer += wasFound ? dictionaryLookup : 0;

            wasFound = sixSlotsScores.TryGetValue(
                (gameboard[1, 5] * 100000 + gameboard[2, 4] * 10000 + gameboard[3, 3] * 1000 + gameboard[4, 2] + 100 + gameboard[5, 1] + 10 + gameboard[6, 0] + 1).GetHashCode(), 
                out dictionaryLookup);
            evaluationBuffer += wasFound ? dictionaryLookup : 0;

            wasFound = fiveSlotsScores.TryGetValue(
                (gameboard[2, 5] * 10000 + gameboard[3, 4] * 1000 + gameboard[4, 3] * 100 + gameboard[5, 2] * 10 + gameboard[6, 1] + 1).GetHashCode(), 
                out dictionaryLookup);
            evaluationBuffer += wasFound ? dictionaryLookup : 0;

            wasFound = fourSlotsScores.TryGetValue(
                (gameboard[3, 5] * 1000 + gameboard[4, 4] * 100 + gameboard[5, 3] * 10 + gameboard[6, 2] + 1).GetHashCode(),
                out dictionaryLookup);
            evaluationBuffer += wasFound ? dictionaryLookup : 0;

            //scan diagonal 2
            wasFound = fourSlotsScores.TryGetValue(
                (gameboard[3, 5] * 1000 + gameboard[2, 4] * 100 + gameboard[1, 3] * 10 + gameboard[0, 2] + 1).GetHashCode(), 
                out dictionaryLookup);
            evaluationBuffer += wasFound ? dictionaryLookup : 0;

            wasFound = fiveSlotsScores.TryGetValue(
                (gameboard[4, 5] * 10000 + gameboard[3, 4] * 1000 + gameboard[2, 3] * 100 + gameboard[1, 2] * 10 +  gameboard[0, 1] + 1).GetHashCode(), 
                out dictionaryLookup);
            evaluationBuffer += wasFound ? dictionaryLookup : 0;

            wasFound = sixSlotsScores.TryGetValue(
                (gameboard[5, 5] * 100000 + gameboard[4, 4] * 10000 + gameboard[3, 3] * 1000 + gameboard[2, 2] + 100 + gameboard[1, 1] + 10 + gameboard[0, 0] + 1).GetHashCode(),
                out dictionaryLookup);
            evaluationBuffer += wasFound ? dictionaryLookup : 0;

            wasFound = sixSlotsScores.TryGetValue(
                (gameboard[6, 5] * 100000 + gameboard[5, 4] * 10000 + gameboard[4, 3] * 1000 + gameboard[3, 2] + 100 + gameboard[2, 1] + 10 + gameboard[1, 0] + 1).GetHashCode(), 
                out dictionaryLookup);
            evaluationBuffer += wasFound ? dictionaryLookup : 0;

            wasFound = fiveSlotsScores.TryGetValue(
                (gameboard[6, 4] * 10000 + gameboard[5, 3] * 1000 + gameboard[4, 2] * 100 + gameboard[3, 1] * 10 + gameboard[2, 0] + 1).GetHashCode(), 
                out dictionaryLookup);
            evaluationBuffer += wasFound ? dictionaryLookup : 0;

            wasFound = fourSlotsScores.TryGetValue(
                (gameboard[6, 3] * 1000 + gameboard[5, 2] * 100 + gameboard[4, 1] * 10 + gameboard[3, 0] + 1).GetHashCode(), 
                out dictionaryLookup);
            evaluationBuffer += wasFound ? dictionaryLookup : 0;

            Debug.WriteLine(evaluationBuffer);
            return evaluationBuffer;
        }
    }
}
