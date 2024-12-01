using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCodeLib;
using AdventOfCodeLib.Attributes;
using AdventOfCodeLib.Extensions;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles.Puzzles._2024
{
    [PuzzleType("Puzzle 1", 2024, 1)]
    public class AdventOfCode2024Puzzle1 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            var result = 0L;
            var leftNumbers = new List<long>();
            var rightNumbers = new List<long>();
            var indexFirstSpace = input.First().IndexOf(' ');
            var indexLastSpace = input.First().LastIndexOf(' ');
            
            foreach (var line in input)
            {
                if(string.IsNullOrWhiteSpace(line))
                    continue;
                
                leftNumbers.Add(line[..indexFirstSpace].ExtractNumber());
                rightNumbers.Add(line[indexLastSpace..].ExtractNumber());
            }

            var orderedLeft = leftNumbers.Order().ToList();
            var orderedRight = rightNumbers.Order().ToList();
            
            for (var i = 0; i < leftNumbers.Count; i++)
            {
                var difference = Math.Abs(orderedLeft[i] - orderedRight[i]);
                result += difference;
            }
            

            return result;
        }
        
        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            var result = 0L;
            var leftNumbers = new List<long>();
            var rightNumberCounter = new Dictionary<long, int>();
            var indexFirstSpace = input.First().IndexOf(' ');
            var indexLastSpace = input.First().LastIndexOf(' ');
            
            foreach (var line in input)
            {
                if(string.IsNullOrWhiteSpace(line))
                    continue;

                var leftNumber = line[..indexFirstSpace].ExtractNumber();
                var rightNumber = line[indexLastSpace..].ExtractNumber();
                
                leftNumbers.Add(leftNumber);
                if (!rightNumberCounter.TryAdd(rightNumber, 1))
                    rightNumberCounter[rightNumber]++;
            }
            foreach (var leftNumber in leftNumbers)
                result += rightNumberCounter.TryGetValue(leftNumber, out var value) ? value * leftNumber : 0;

            return result;
        }
    }
}