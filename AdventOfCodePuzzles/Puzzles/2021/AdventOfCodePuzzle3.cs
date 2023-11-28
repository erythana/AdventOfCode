using AdventOfCodeLib.Attributes;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles.Puzzles._2021
{
    [PuzzleType("Puzzle 3", 2021, 3)]
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

            return gamma * epsilon;
        }

        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            var modifiedInput = input.ToArray();
            var length = modifiedInput.FirstOrDefault().Length;

            var oxygenResult = new List<string>(modifiedInput);
            var scrubberResult = new List<string>(modifiedInput);
            
            Func<bool, int> targetSelector = b => b ? 1 : 0;
            Func<int, int, bool> oxygenMatch = (listSize, ones) => listSize / 2.0 <= ones;
            Func<int, int, bool> scrubberMatch = (listSize, zeros) => listSize / 2.0 >= zeros;

            for (int i = 0; i < length; i++)
            {
                var oxygenSize = oxygenResult.Count;
                var oneFounds = OneFounds(oxygenResult, i);
                RemoveEntriesFromResultSet(oxygenResult, i,
                    num => num == targetSelector(oxygenMatch(oxygenSize, oneFounds)));

                var scrubberSize = scrubberResult.Count;
                var zeroFounds = scrubberResult.Count - OneFounds(scrubberResult, i);
                RemoveEntriesFromResultSet(scrubberResult, i,
                    num => num == targetSelector(!scrubberMatch(scrubberSize, zeroFounds)));
            }

            return Convert.ToInt32(oxygenResult[0], 2) * Convert.ToInt32(scrubberResult[0], 2);

            int OneFounds(IEnumerable<string> inputEnumerable, int i)
                => inputEnumerable.Select(line => line[i] - '0').Count(currentNumber => currentNumber == 1);

            void RemoveEntriesFromResultSet(ICollection<string> list, int currentIndexPosition, Func<int, bool> matchesNumberFunc)
            {
                foreach (var oxygenItem in list.ToList())
                {
                    if (list.Count > 1 && !matchesNumberFunc(oxygenItem[currentIndexPosition] - '0'))
                        list.Remove(oxygenItem);
                }
            }
        }
    }
}