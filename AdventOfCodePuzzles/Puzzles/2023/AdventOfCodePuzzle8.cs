using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCodeLib.Attributes;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles.Puzzles._2023
{
    [PuzzleType(null, 2023, 8)]
    public class AdventOfCode2023Puzzle8 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            var modifiedInput = input.ToArray();
            
            var map = new Dictionary<string, string[]>();
            var movingInstructionsIndex = modifiedInput[0].Select(x => x == 'L'
                ? 0
                : 1).ToArray();
            
            foreach (var line in modifiedInput[2..])
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                
                var node = line.Split("=", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                var position = node[0];
                var directions = node[1][1..^1].Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                map.Add(position, directions);
            }

            var result = 0;
            var current = "AAA";
            while (current != "ZZZ")
            {
                var mapPosition = movingInstructionsIndex[result % movingInstructionsIndex.Length];//0 or 1
                current = map[current][mapPosition];
                result++;
            }
            
            return result;
        }

        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            var modifiedInput = input.ToArray();
            
            var map = new Dictionary<string, string[]>();
            var validStartingNodes = new HashSet<string>();
            var movingInstructionsIndex = modifiedInput[0].Select(x => x == 'L'
                ? 0
                : 1).ToArray();
            
            foreach (var line in modifiedInput[2..])
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                
                var node = line.Split("=", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                var position = node[0];
                var directions = node[1][1..^1].Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                map.Add(position, directions);
                if (position[^1] == 'A')
                    validStartingNodes.Add(position);
            }

            var result = 0L;

            var processing = validStartingNodes.ToArray();
            var allEndWithZ = false;
            while (true)
            {
                for (var i = 0; i < processing.Length; i++)
                {
                    var currentNode = processing[i];
                    if (currentNode[^1] != 'Z')
                        allEndWithZ = false;
                    
                    var mapPosition = movingInstructionsIndex[result % movingInstructionsIndex.Length];//0 or 1
                    var newNode = map[currentNode][mapPosition];
                    processing[i] = newNode;
                }
                
                if (allEndWithZ)
                    break;

                result++;
                allEndWithZ = true;
            }

            return result;
        }
    }
}