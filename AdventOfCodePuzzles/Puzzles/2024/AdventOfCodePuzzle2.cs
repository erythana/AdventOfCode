using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCodeLib;
using AdventOfCodeLib.Attributes;
using AdventOfCodeLib.Extensions;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles.Puzzles._2024
{
    [PuzzleType("Puzzle 2", 2024, 2)]
    public class AdventOfCode2024Puzzle2 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            var result = 0;
            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                if (AreAllAscendingOrDescending(line.Split(' ').Select(int.Parse).ToList()))
                    result++;
            }

            return result;
        }

        private static bool AreAllAscendingOrDescending(List<int> values)
        {
            var allAscOrDesc = true;
            var allAsc = true;
            var allDesc = true;

            for (var i = 1; i < values.Count; i++)
            {
                var previousDigit = values[i - 1];
                var currentDigit = values[i];

                var numberDiff = Math.Abs(previousDigit - currentDigit);
                if (numberDiff is 0 or > 3)
                {
                    allAscOrDesc = false;
                    break;
                }

                var isAscending =
                    previousDigit <
                    currentDigit; // either ascending or descending because they can't be of same value (diff within bounds)

                allAsc = allAsc && isAscending;
                allDesc = allDesc && !isAscending;

                allAscOrDesc = allAscOrDesc && (allAsc || allDesc);
                if (!allAscOrDesc)
                    break;
            }

            return allAscOrDesc;
        }

        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            var result = 0;
            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;
            
                var allNumbers = line.Split(' ').Select(int.Parse).ToList();
                if (AreAllAscendingOrDescending(allNumbers))
                    result++;
                else
                {
                   
                    for (var i = 0; i < allNumbers.Count; i++)
                    {
                        var copy = allNumbers.ToList();
                        copy.RemoveAt(i);
                        if (!AreAllAscendingOrDescending(copy)) continue;
                        
                        result++;
                        break;
                    }
                }
            }
            return result;
        }
    }
}