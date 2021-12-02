using System.Collections.Generic;
using System.Linq;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles
{
    public class AdventOfCodePuzzle1 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            var count = 0;
            int? previous = null;
            foreach (var number in input.Select(int.Parse))
            {
                if (number > previous)
                    count++;

                previous = number;
            }

            return count;
        }

        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            int? previous = null;
            var result = 0;
            var modifiedInput = input.Select(int.Parse).ToList();
            for (int i = 0; i + 3 <= modifiedInput.Count; i++)
            {
                var sum = 0;
                for (int x = 0; x < 3; x++)
                {
                    sum += modifiedInput[i + x];
                }

                if (sum > previous)
                    result++;
                previous = sum;
            }

            return result;
        }
    }
}