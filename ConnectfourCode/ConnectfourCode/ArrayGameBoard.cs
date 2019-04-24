using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectfourCode
{
    class ArrayGameBoard
    {
        int[,] gameboard = new int[7, 6];
        List<int> moveHistory = new List<int>();
        Dictionary<int, int> sevenSlotsScores = new Dictionary<int, int>();
        Dictionary<int, int> sixSlotsScores = new Dictionary<int, int>();
        Dictionary<int, int> fiveSlotsScores = new Dictionary<int, int>();

        public ArrayGameBoard()
        {
            ResetBitBoard();
            string[] splitStringBuffer;
            foreach (string line in File.ReadLines(@"C:\Users\skive\Source\Repos\Jannl12\ConnectFourTrue\ConnectfourCode\ConnectfourCode\7C4.txt"))
            {
                splitStringBuffer = line.Split(' ');
                sevenSlotsScores.Add(int.Parse(splitStringBuffer[0]), int.Parse(splitStringBuffer[1]));
            }

            foreach (string line in File.ReadLines(@"C:\Users\skive\Source\Repos\Jannl12\ConnectFourTrue\ConnectfourCode\ConnectfourCode\6C4.txt"))
            {
                splitStringBuffer = line.Split(' ');
                sixSlotsScores.Add(int.Parse(splitStringBuffer[0]), int.Parse(splitStringBuffer[1]));
            }

            foreach (string line in File.ReadLines(@"C:\Users\skive\Source\Repos\Jannl12\ConnectFourTrue\ConnectfourCode\ConnectfourCode\5C4.txt"))
            {
                splitStringBuffer = line.Split(' ');
                fiveSlotsScores.Add(int.Parse(splitStringBuffer[0]), int.Parse(splitStringBuffer[1]));
            }
        }

        public void MakeMove(int coloumnInput)
        {
            for(int i = gameboard.GetLength(1); i > 0; i--)
            {
                gameboard[coloumnInput, i] = gameboard[coloumnInput, i] == 0 ? moveHistory.Count() % 2 : 0; 
            }
            moveHistory.Add(coloumnInput);
        }

        public void UndoMove()
        {
            for (int i = 0; i > gameboard.GetLength(1); i++)
            {
                if(gameboard[moveHistory.Last(), i] != 0)
                {
                    gameboard[moveHistory.Last(), i] = 0;
                }
            }
            moveHistory.RemoveAt(moveHistory.Count - 1);
        }

        public bool CanPlay(int column)
        {
            if(gameboard[column, gameboard.GetLength(1)] == 0)
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
            foreach(int move in moveHistory)
            {
                moveHistory.Remove(move);
            }
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

        public int EvaluateBoard()
        {
            int evaluationBuffer = 0, dictionaryLookup = 0;

            //scan horizontal
            for (int i = 0; i < 6; i++)
            {
                sixSlotsScores.TryGetValue((gameboard[0, i] * 1000000 + gameboard[1, i] * 100000 +
                    gameboard[2, i] * 10000 + gameboard[3, i] * 1000 + gameboard[4, i] * 100 +
                    gameboard[5, i] * 10 + gameboard[6, i] * 1), out dictionaryLookup);
                evaluationBuffer += dictionaryLookup;
            }

            //scan vertical
            for (int i = 0; i < 7; i++)
            {
                sevenSlotsScores.TryGetValue((gameboard[i, 0] * 1000000 + gameboard[i, 1] * 100000 +
                    gameboard[i, 2] * 10000 + gameboard[i, 3] * 1000 + gameboard[i, 4] * 100 +
                    gameboard[i, 5] * 10 + gameboard[i, 6] * 1), out dictionaryLookup);
                evaluationBuffer += dictionaryLookup;
            }

            //scan diagonal 1
            
            return evaluationBuffer;
        }
    }
}
