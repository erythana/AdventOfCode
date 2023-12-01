using System.Collections.Generic;
using System.Linq;
using AdventOfCodeLib.Attributes;
using AdventOfCodeLib.Extensions;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles.Puzzles._2023
{
    [PuzzleType("Puzzle 1", 2023, 1)]
    public class AdventOfCode2023Puzzle1 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            var result = 0;
            
            foreach (var line in input)
            {
                var tempResult = 0;
                for (var i = 0; i < line.Length; i++)
                {
                    if (line[i].IsDigit())
                        tempResult = tempResult * 10 + line[i].ToInt();
                }

                result += GetCalibrationValue(tempResult);
            }

            return result;
        }
        
        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            var validNumbers = new Dictionary<string, int>
            {
                { "one", 1 },
                { "two", 2 },
                { "three", 3 },
                { "four", 4 },
                { "five", 5 },
                { "six", 6 },
                { "seven", 7 },
                { "eight", 8 },
                { "nine", 9 },
                { "zero", 0 },
            };
            
            var result = 0;
            foreach (var line in input)
            {
                var tempResult = 0;
                for (var i = 0; i < line.Length; i++)
                {
                    if (line[i].IsDigit())
                        tempResult = tempResult * 10 + line[i].ToInt();
                    else
                    {
                        for (var y = 3; y <= 5; y++)
                        {
                            var word = new string(line.Skip(i).Take(y).ToArray());
                            if (!validNumbers.ContainsKey(word)) continue;
                            
                            tempResult = tempResult * 10 + validNumbers[word];
                            break;
                        }
                    }
                }

                result += GetCalibrationValue(tempResult);
            }

            return result;
        }

        //HURR DURR FAST APPROACH
        private static int GetCalibrationValue(int parsedNumber)
        {
            var lastDigit = parsedNumber % 10;
            while (parsedNumber >= 10)
            {
                parsedNumber /= 10;
            }

            var firstDigit = parsedNumber % 10;
            return firstDigit * 10 + lastDigit;
        }
    }
}