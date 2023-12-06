using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdventOfCodeLib;
using AdventOfCodeLib.Attributes;
using AdventOfCodeLib.Extensions;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles.Puzzles._2023
{
    [PuzzleType(null, 2023, 6)]
    public class AdventOfCode2023Puzzle6 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            var results = new List<int>();

            var modifiedInput = input.ToArray();

            var parsedDurations = modifiedInput[0][(modifiedInput[0].IndexOf(":") + 1)..].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
            var parsedDistances = modifiedInput[1][(modifiedInput[1].IndexOf(":") + 1)..].Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
            var durations = new List<int>(parsedDurations);
            var distances = new List<int>(parsedDistances);

            var numberOfRaces = durations.Count;
            for (var i = 0; i < numberOfRaces; i++)
            {
                var raceDuration = durations[i];
                var raceDistance = distances[i];

                var waysToWin = WaysToWin(raceDuration, raceDistance);

                if (waysToWin > 0)
                    results.Add(waysToWin);
            }

            return results.Aggregate((i, i1) => i * i1);
        }

        private static int WaysToWin(long raceDuration, long raceDistance)
        {
            var waysToWin = 0;
            for (var startingSpeed = 1; startingSpeed < raceDuration; startingSpeed++)
            {
                var remainingRaceTime = raceDuration - startingSpeed;

                var distance = startingSpeed * remainingRaceTime;
                if (distance > raceDistance)
                    waysToWin++;
            }

            return waysToWin;
        }

        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            var results = new List<int>();

            var modifiedInput = input.ToArray();

            var raceDuration = modifiedInput[0][(modifiedInput[0].IndexOf(":") + 1)..].ExtractNumber();
            var raceDistance = modifiedInput[1][(modifiedInput[1].IndexOf(":") + 1)..].ExtractNumber();
            
            var waysToWin = WaysToWin(raceDuration, raceDistance);

            if (waysToWin > 0)
                results.Add(waysToWin);

            return results.Aggregate((i, i1) => i * i1);
        }
    }
}