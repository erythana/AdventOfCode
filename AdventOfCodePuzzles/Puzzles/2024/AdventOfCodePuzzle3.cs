using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCodeLib;
using AdventOfCodeLib.Attributes;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles.Puzzles._2024
{
    [PuzzleType("Puzzle 3", 2024, 3)]
    public class AdventOfCode2024Puzzle3 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            var result = 0L;

            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var regex = new Regex(@"mul\(\d+,\d+\)");
                var matches = regex.Matches(line);
                foreach (Match match in matches)
                {
                    var matchValue = match.Value;
                    var startIndex = matchValue.IndexOf('(');
                    var endIndex = matchValue.IndexOf(')');
                    var splitIndex = matchValue.IndexOf(',');

                    result += matchValue[startIndex..splitIndex].ExtractNumber() *
                              matchValue[splitIndex..endIndex].ExtractNumber();
                }
            }
            return result;
        }

        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            var result = 0L;

            var line = string.Join("", input); // eric, you nearly got me

            var matches = new Regex(@"mul\(\d+,\d+\)").Matches(line);
            var dos = new Regex(@"do\(\)").Matches(line).Select(m => m.Index).ToList();
            var donts = new Regex(@"don't\(\)").Matches(line).Select(m => m.Index).ToList();

            foreach (Match instructionMatch in matches)
            {
                var indexDo = dos.LastOrDefault(m => m < instructionMatch.Index);
                var indexDont = donts.LastOrDefault(m => m < instructionMatch.Index);

                var isEnabled = indexDo >=
                                indexDont;

                if (!isEnabled)
                    continue;

                var matchValue = instructionMatch.Value;
                var startIndex = matchValue.IndexOf('(');
                var endIndex = matchValue.IndexOf(')');
                var splitIndex = matchValue.IndexOf(',');

                result += matchValue[startIndex..splitIndex].ExtractNumber() *
                          matchValue[splitIndex..endIndex].ExtractNumber();
            }
            
            return result;
        }
    }
}