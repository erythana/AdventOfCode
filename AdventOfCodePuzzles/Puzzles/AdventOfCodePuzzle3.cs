using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles
{
    public class AdventOfCodePuzzle3 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            var inputCount = 0;
            var length = input.FirstOrDefault().Length;
            var intFounds = new int[length];
            
            foreach (var line in input)
            {
                inputCount++;
                for (int i = 0; i < length; i++)
                {
                    var currentNumber = line[i] - '0';
                    if (currentNumber == 1)
                        intFounds[i] += currentNumber;
                }
            }
            
            var binaryRepresentationGamma = string.Empty;
            for (int i = 0; i < intFounds.Length; i++)
            {
                binaryRepresentationGamma += (inputCount / 2 < intFounds[i]) ? 1 : 0;
            }
            var maxInt = Math.Pow(2, length) - 1;
            var gamma = Convert.ToInt32(binaryRepresentationGamma, 2);
            var epsilon = maxInt - gamma;
            
            return gamma*epsilon;
        }

        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            void RemoveEntriesFromResultset(int currentIndexPosition, ICollection<string> list, Func<int, int, bool> matchesNumberFunc)
            {
                var oneFounds = OneFounds(list, currentIndexPosition);
                var moreOrEqual = list.Count / 2.0 <= oneFounds;
                var target = moreOrEqual ? 1 : 0;
                foreach (var oxygenItem in list.ToList())
                {
                    if (list.Count > 1 && !matchesNumberFunc(oxygenItem[currentIndexPosition] - '0', target))
                        list.Remove(oxygenItem);
                }
            }

            var modifiedInput = input.ToArray();
            var length = modifiedInput.FirstOrDefault().Length;

            var oxygenResult = new List<string>(modifiedInput);
            var scrubberResult = new List<string>(modifiedInput);
            
            Func<int, int, bool> oxygenScrubberCriteria = (i, t) => i == t;

            for (int i = 0; i < length; i++)
            {
                var oneFounds = OneFounds(oxygenResult, i);

                var moreOrEqual = oxygenResult.Count / 2.0 <= oneFounds;
                foreach (var oxygenItem in oxygenResult.ToList())
                {
                    var target = moreOrEqual ? 1 : 0;
                    if (oxygenResult.Count > 1 && !oxygenScrubberCriteria(oxygenItem[i] - '0', target))
                    {
                        oxygenResult.Remove(oxygenItem);
                    }
                }
                
                oneFounds = OneFounds(scrubberResult, i);
                
                var lessOrEqual = scrubberResult.Count / 2.0 >= scrubberResult.Count - oneFounds;
                foreach (var scrubberItem in scrubberResult.ToList())
                {
                    var target = lessOrEqual ? 0 : 1;
                    if (scrubberResult.Count > 1 && !oxygenScrubberCriteria(scrubberItem[i] - '0', target))
                    {
                        scrubberResult.Remove(scrubberItem);
                    }
                }
            }
            return Convert.ToInt32(oxygenResult[0],2) * Convert.ToInt32(scrubberResult[0], 2);

            int OneFounds(IEnumerable<string> inputEnumerable, int i)
            {
                var oneFounds = 0;
                foreach (var line in inputEnumerable)
                {
                    var currentNumber = line[i] - '0';
                    if (currentNumber == 1)
                        oneFounds++;
                }

                return oneFounds;
            }
        }
    }
}