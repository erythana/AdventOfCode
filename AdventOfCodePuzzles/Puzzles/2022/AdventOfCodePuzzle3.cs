using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles
{
    [Description("2022 - Puzzle 3")]
    public class AdventOfCode2022Puzzle3 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            var result = 0;
            
            foreach (var rucksack in input.Where(s => !string.IsNullOrWhiteSpace(s)))
            {
                var halfLength = rucksack.Length / 2;
                var leftSet = new HashSet<char>();
                var rightSet = new HashSet<char>();

                for (int i = 0; i < halfLength; i++)
                {
                    leftSet.Add(rucksack[i]);
                    rightSet.Add(rucksack[i+halfLength]);
                }

                var tmpChar = leftSet.Intersect(rightSet).First();
                result += pointsFrom(tmpChar);
            }
            return result;

            int pointsFrom(char character) => character >= 97 ? character - 96 : character - 38;
        }

        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            return 0;
        }
    }
}