using System.Collections.Generic;
using System.Linq;
using AdventOfCodeLib.Attributes;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles.Puzzles._2022
{
    [PuzzleType("Puzzle 3", 2022, 3)]
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

                var commonChar = leftSet.Intersect(rightSet).First();
                result += returnPointsFrom(commonChar);
            }
            return result;

            int returnPointsFrom(char character) => character >= 97 ? character - 96 : character - 38;
        }

        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            var result = 0;
            var tmpInput = input.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
            var results = new List<char[]>();
           
            for (int i = 0; i < tmpInput.Length; i += 3)
            {
                results.Clear();
                for (int j = 0; j < 3; j++)
                {
                    var currentLineIndex = i + j;
                    var line = tmpInput[currentLineIndex];
                    
                    results.Add(line.ToCharArray());
                }

                var commonChar = results[0].Intersect(results[1]).Intersect(results[2]).First();
                result += returnPointsFrom(commonChar);
            }
            return result;
            
            int returnPointsFrom(char character) => character >= 97 ? character - 96 : character - 38;
        }
    }
}