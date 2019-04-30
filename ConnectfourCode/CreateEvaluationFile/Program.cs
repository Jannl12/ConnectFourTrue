using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFile
{
    class Program
    {
        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("Hello World!");
            CreateCombinationFile.writeToFile();



            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }

    }



    static class CreateCombinationFile
    {
        //https://stackoverflow.com/questions/25824376/combinations-with-repetitions-c-sharp
        static IEnumerable<String> CombinationsWithRepition(IEnumerable<int> playerValues, int permutationLength)
        {
            if (permutationLength <= 0)
                yield return "";
            else
            {
                foreach (var i in playerValues)
                    foreach (var c in CombinationsWithRepition(playerValues, permutationLength - 1))
                        yield return i.ToString() + c;
            }
        }

        static Dictionary<int, int> getScoreValues(IEnumerable<int> permutationValues, Dictionary<int, int> scorePerConnected, int spanSize, int combinationLength, char emptySlotValue, char playerValue)
        {
            Dictionary<int, int> returnDictionary = new Dictionary<int, int>();
            string[] lineBuffer = new string[(combinationLength - combinationLength % spanSize)];
            int currentPlayerSlotsFound = 0, bufferScore = 0, dictionaryLookup, count = 0;
            foreach (string combination in CombinationsWithRepition(permutationValues, spanSize))
            {
                for (int g = 0; g < (spanSize - (combinationLength - 1)); g++)
                {
                    for (int i = 0; i < combinationLength; i++)
                    {
                        if (combination[g + i] == '1')
                        {
                            currentPlayerSlotsFound += 1; count++;
                        }
                        else if (combination[g + i] == '2')
                        {
                            currentPlayerSlotsFound = 0;
                            break;
                        }

                    }
                    bufferScore += scorePerConnected.TryGetValue(currentPlayerSlotsFound, out dictionaryLookup) ? dictionaryLookup : 0;
                    currentPlayerSlotsFound = 0;

                    for (int h = 0; h < combinationLength; h++)
                    {
                        if (combination[g + h] == '2')
                        {
                            currentPlayerSlotsFound += 1; count++;
                        }
                        else if (combination[g + h] == '1')
                        {
                            currentPlayerSlotsFound = 0;
                            break;
                        }
                    }

                    bufferScore -= scorePerConnected.TryGetValue(currentPlayerSlotsFound, out dictionaryLookup) ? dictionaryLookup : 0;
                    currentPlayerSlotsFound = 0;
                }
                returnDictionary.Add(Convert.ToInt32(combination), bufferScore);
                bufferScore = 0;

            }
            return returnDictionary;
        }

        static public void writeToFile()
        {
            Dictionary<int, int> inputDictionary = new Dictionary<int, int> { { 0, 0 }, { 1, 1 }, { 2, 4 }, { 3, 9 }, { 4, 1000 } };
            using (System.IO.StreamWriter test = new System.IO.StreamWriter(@"C:\Users\ehvid\Desktop\7C4.txt"))
            {
                foreach (KeyValuePair<int, int> item in getScoreValues(new int[] { 0, 1, 2 }, inputDictionary, 7, 4, '0', '1'))
                {
                    string workstring = item.Key + " " + item.Value;
                    test.WriteLine(workstring);
                }
            }

            using (System.IO.StreamWriter test = new System.IO.StreamWriter(@"C:\Users\ehvid\Desktop\6C4.txt"))
            {
                foreach (KeyValuePair<int, int> item in getScoreValues(new int[] { 0, 1, 2 }, inputDictionary, 6, 4, '0', '1'))
                {
                    string workstring = item.Key + " " + item.Value;
                    test.WriteLine(workstring);
                }
            }

            using (System.IO.StreamWriter test = new System.IO.StreamWriter(@"C:\Users\ehvid\Desktop\5C4.txt"))
            {
                foreach (KeyValuePair<int, int> item in getScoreValues(new int[] { 0, 1, 2 }, inputDictionary, 5, 4, '0', '1'))
                {
                    string workstring = item.Key + " " + item.Value;
                    test.WriteLine(workstring);
                }
            }

            using (System.IO.StreamWriter test = new System.IO.StreamWriter(@"C:\Users\ehvid\Desktop\4C4.txt"))
            {
                foreach (KeyValuePair<int, int> item in getScoreValues(new int[] { 0, 1, 2 }, inputDictionary, 4, 4, '0', '1'))
                {
                    string workstring = item.Key + " " + item.Value;
                    test.WriteLine(workstring);
                }
            }
        }
    }
}
