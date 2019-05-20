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

        /** <summary><c>GetScoreValues</c> is a static medthod, which is used to give a </summary>
         */


        static Dictionary<string, int> GetScoreValuesForCombinations(
            char[] playerChars, Dictionary<int, int> pointsPerDiskWithinCombinationLength,
            int spanSize, int combinationLength, char emptySlotValue, char mainPlayerChar)
        {
            Dictionary<string, int> returnDictionary = new Dictionary<string, int>();

            int currentPlayerSlotsFound = 0, bufferScore = 0, dictionaryLookup;

            foreach (string combination in CombinationsWithRepition(playerChars, spanSize))
            {
                string bufferString = '3' + combination; //Add 3 to front of string, as an identififer to which spanSize. 
                foreach (char playerChar in playerChars.Except(new char[] { emptySlotValue }))
                {
                    for (int g = 1; g < (spanSize + 1) - (combinationLength - 1); g++)
                        //Run from the start of combination (account for the added char), and till 
                        //the span covers the entire combination.
                    {
                        for (int f = 0; f < combinationLength; f++)
                        {
                            if (bufferString[g + f] == playerChar)
                            {
                                currentPlayerSlotsFound++;
                            }
                            else if (bufferString[g + f] == emptySlotValue)
                            {
                                //Do nothing - Keep running. 
                            }
                            else if ((bufferString[g + f] != playerChar) || (bufferString[g + f] != emptySlotValue))
                            {
                                currentPlayerSlotsFound = 0; 
                                break;
                            }
                        }
                        bufferScore += pointsPerDiskWithinCombinationLength.TryGetValue(currentPlayerSlotsFound, out dictionaryLookup)
                            ? (playerChar == mainPlayerChar ? dictionaryLookup : -dictionaryLookup) : 0;
                        currentPlayerSlotsFound = 0;
                    }
                    
                }
                if (bufferScore != 0) //Only add boards that make difference to the score.
                {
                    if (bufferScore > pointsPerDiskWithinCombinationLength.Values.Max())
                    {
                        returnDictionary.Add(bufferString, pointsPerDiskWithinCombinationLength.Values.Max());
                    }
                    else if (bufferScore < -pointsPerDiskWithinCombinationLength.Values.Max())
                    {
                        returnDictionary.Add(bufferString, -pointsPerDiskWithinCombinationLength.Values.Max());
                    }
                    else
                    {
                        returnDictionary.Add(bufferString, bufferScore);
                    }
                    bufferScore = 0;
                }
            }
            return returnDictionary;
        }

        public static Dictionary<int, int> GetDictionaryOfCombinationsAndScoresOfMoreSpanSizes(
            Dictionary<int, int> pointsPerDiskWithinCombinationLength, int[] spanSizes, 
            int combinatioLength, char[] playerValues, char emptySlotValue, char mainPlayerValue)
        {
            Dictionary<int, int> returnDictionary = new Dictionary<int, int>();

            foreach (int spanSize in spanSizes)
            {
                foreach (KeyValuePair<string, int> item in GetScoreValuesForCombinations(
                    playerValues, pointsPerDiskWithinCombinationLength, spanSize, 4, emptySlotValue, mainPlayerValue))
                {
                    returnDictionary.Add(Int32.Parse(item.Key), item.Value);
                }
            }
            return returnDictionary;
        }
    }
}
