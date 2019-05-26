using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlFile
{

    
    /**<summary>The class use din ArrayGameBoard, for the evaulation of the board.</summary>
     */
    static class ScoreCombinations
    {
        /**<summary><c>CombinationsWithRepition</c> is function, which makes a list of all combinations of a given length
         * based on a given list of chars. It uses recursion to get all these values. The amount of combinations, can be calculated by
         * combinationLength ^ playerValues.Count()</summary>
         * <param name="permutationLength"> Indicates which length the final return strings are desired to be.</param>
         * <param name="playerValues"> Is a array of all the chars which are to be made combinations of. </param>
         * <returns> An IEnumerable<String>, which consits of all the combinations. </returns>
         * <see cref="https://stackoverflow.com/questions/25824376/combinations-with-repetitions-c-sharp"/>
        */
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

        /** <summary><c>GetScoreValues</c> is a static method, which is used to give a Dictionary of all combinations 
         * and scores.</summary>
         * <param name="combinationLength">Is the length of the strings which are to be made for keys.</param>
         * <param name="emptySlotValue">The char-value which indicates an empty slots in the board. </param>
         * <param name="mainPlayerChar">The char-value which is the mainplayer, from which the points should be indicating preferences from.</param>
         * <param name="playerChars">Is all the chars, which can be expected to be found on the board, including empty-slots.</param>
         * <param name="pointsPerDiskWithinCombinationLength">Is the scores, which are given for connected tiles in span.  </param>
         * <param name="spanSize">Is the size, of what is the maximum connected slots, which it is needed to win. In connect four,
         * this is always 4.</param>
         * <returns>A dictionary of combinations, of which the key is a combination of playerChars if the input length of <paramref name="combinationLength"/>.
         * The value of each of the key, is based on the amount of disks found in alle the spans in the combinations and the score, which is inputed 
         * with <paramref name="pointsPerDiskWithinCombinationLength"/>.</returns>
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
                                currentPlayerSlotsFound = 0; //If there is found a disk of an opponont if the currentPlayer, the cannot be given 
                                break;                       //points for that span, since it is not possible for any player to achieve four disks.
                            }
                        }
                        //When the amount of disks of an span, it is calculated how many scores this results in.
                        bufferScore += pointsPerDiskWithinCombinationLength.TryGetValue(currentPlayerSlotsFound, out dictionaryLookup)
                            ? (playerChar == mainPlayerChar ? dictionaryLookup : -dictionaryLookup) : 0;
                        currentPlayerSlotsFound = 0;
                    }
                    
                }
                if (bufferScore != 0) //Only add boards that make difference to the score.
                {
                    //Also make sure, that no score is above the maximum winning value of connected-four. 
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

        /** <summary><c>GetDictionaryOfCombinationsAndScoresOfMoreSpanSizes</c> is used, if it is desired to use 
         * a dictionary of different lengths, and since there are combinations of 4, 5, 6 and 7, this method is useful.</summary>'
         * <param name="spanSizes">The span from which all slots need to be filled with disks, if the game is to be won. Usually just 4.</param>
         * <param name="combinationLengths">Is the width of all the keys, which should be made.</param>
         * <param name="spanSize">The width if the required amount if empty tiles for a win. Usually 4.</param>
         * <param name="pointsPerDiskWithinCombinationLength">The points given for the different amount of filed tiles in a span.</param>
         * <param name="emptySlotValue">The value of an emplty slot.</param>
         * <param name="mainPlayerValue">The value of the main player, from whose from perspective the scores are made.</param>
         * <param name="playerValues">All the values which can be found in the board, including the emptyslots.</param>
         * <returns>A Dictionary of the lengths, which are possible to be found in the board. All keys of the Dictionary are the possible combinations, and the values
         * are the scores, which describe how well the mainPlayer is set in the given combination.</returns>
         */
        public static Dictionary<int, int> GetDictionaryOfCombinationsAndScoresOfMoreSpanSizes(

            Dictionary<int, int> pointsPerDiskWithinCombinationLength, int spanSize, 
            int[] combinationLengths, char[] playerValues, char emptySlotValue, char mainPlayerValue)

        {
            Dictionary<int, int> returnDictionary = new Dictionary<int, int>();

            foreach (int combinationLength in combinationLengths)
            {
                foreach (KeyValuePair<string, int> item in GetScoreValuesForCombinations(
                    playerValues, pointsPerDiskWithinCombinationLength, combinationLength, spanSize, emptySlotValue, mainPlayerValue))

                {
                    returnDictionary.Add(int.Parse(item.Key), item.Value);
                }
            }
            return returnDictionary;
        }
    }
}
