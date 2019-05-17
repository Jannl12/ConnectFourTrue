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
        static IEnumerable<String> CombinationsWithRepition(char[] playerValues, int permutationLength)
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
            char[] permutationValues, Dictionary<int, int> scorePerConnected, int spanSize, int combinationLength, char emptySlotValue, char playerValue)
        {
            Dictionary<string, int> returnDictionary = new Dictionary<string, int>();
            string[] lineBuffer = new string[(combinationLength - combinationLength % spanSize)];
            int currentPlayerSlotsFound = 0, bufferScore = 0, dictionaryLookup;
            foreach (string combination in CombinationsWithRepition(permutationValues, spanSize))
            {
                string bufferString = '3' + combination;
                foreach (char i in permutationValues)
                {
                    for (int g = 1; g < (spanSize + 1) - (combinationLength - 1); g++)
                    {
                        for (int f = 0; f < combinationLength; f++)
                        {
                            if (bufferString[g + f] == i)
                            {
                                currentPlayerSlotsFound++;
                            }
                            else if (bufferString[g + f] == emptySlotValue)
                            {
                            }
                            else if (bufferString[g + f] != i || bufferString[g + f] != emptySlotValue)
                            {
                                currentPlayerSlotsFound = 0;
                                break;
                            }
                        }
                        bufferScore += scorePerConnected.TryGetValue(currentPlayerSlotsFound, out dictionaryLookup)
                            ? (i == playerValue ? dictionaryLookup : -dictionaryLookup) : 0;
                        currentPlayerSlotsFound = 0;
                    }
                }
                returnDictionary.Add(bufferString, bufferScore);
                bufferScore = 0;

            }
            return returnDictionary;
        }

        public static Dictionary<int, int> GetDictionaryOfCombinationsAndScoresOfMoreSpanSizes(
            Dictionary<int, int> inputScores, int[] spanSizes, int combinationsize, char[] playerValues, char emptySlotValue, char playerValue)
        {
            Dictionary<int, int> returnDictionary = new Dictionary<int, int>();

            foreach (int spanSize in spanSizes)
            {
                foreach (KeyValuePair<string, int> item in getScoreValues(playerValues, inputScores, spanSize, 4, emptySlotValue, playerValue))
                {
                    returnDictionary.Add(int.Parse(item.Key), item.Value);
                }
            }
            return returnDictionary;
        }
    }
}
