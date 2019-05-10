using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFile
{
        static class ScoreCombinations
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
            

        static Dictionary<string, int> getScoreValues(
            IEnumerable<int> permutationValues, Dictionary<int, int> scorePerConnected, int spanSize, int combinationLength, char emptySlotValue, char playerValue)
        {
            Dictionary<string, int> returnDictionary = new Dictionary<string, int>();
            string[] lineBuffer = new string[(combinationLength - combinationLength % spanSize)];
            int currentPlayerSlotsFound = 0, bufferScore = 0, dictionaryLookup, count = 0;
            foreach (string combination in CombinationsWithRepition(permutationValues, spanSize))
            {
                string bufferString = '3' + combination;
                for (int g = 0; g < (spanSize + 1) - (combinationLength - 1); g++)
                {
                    for (int i = 0; i < combinationLength; i++)
                    {
                        if (bufferString[g + i] == '1')
                        {
                            currentPlayerSlotsFound += 1; count++;
                        }
                        else if (bufferString[g + i] == '2')
                        {
                            currentPlayerSlotsFound = 0;
                            break;
                        }

                    }
                    bufferScore += scorePerConnected.TryGetValue(currentPlayerSlotsFound, out dictionaryLookup) ? dictionaryLookup : 0;
                    currentPlayerSlotsFound = 0;

                    for (int h = 0; h < combinationLength; h++)
                    {
                        if (bufferString[g + h] == '2')
                        {
                            currentPlayerSlotsFound += 1; count++;
                        }
                        else if (bufferString[g + h] == '1')
                        {
                            currentPlayerSlotsFound = 0;
                            break;
                        }
                    }

                    bufferScore -= scorePerConnected.TryGetValue(currentPlayerSlotsFound, out dictionaryLookup) ? dictionaryLookup : 0;
                    currentPlayerSlotsFound = 0;
                }
                returnDictionary.Add(bufferString, bufferScore);
                bufferScore = 0;

            }
            Console.WriteLine(returnDictionary.Count());
            return returnDictionary;
        }

        public static Dictionary<int, int> GetDictionaryOfCombinationsAndScoresOfMoreSpanSizes(
            Dictionary<int,int> inputScores, int[] spanSizes, int combinationsize, int[] playerValues, char emptySlotValue, char playerValue)
        {
            Dictionary<int, int> returnDictionary = new Dictionary<int, int>();
                
            foreach(int spanSize in spanSizes)
            {
                foreach(KeyValuePair<string, int> item in getScoreValues(playerValues, inputScores, spanSize, 4, emptySlotValue, playerValue))
                {
                    returnDictionary.Add(Int32.Parse(item.Key), item.Value);
                }
            }
            return returnDictionary;
        }
    }
}
