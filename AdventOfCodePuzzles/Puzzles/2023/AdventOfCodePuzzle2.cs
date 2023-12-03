using System;
using System.Collections.Generic;
using AdventOfCodeLib.Attributes;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles.Puzzles._2023
{
    [PuzzleType("Puzzle 2", 2023, 2)]
    public class AdventOfCode2023Puzzle2 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            var result = 0;

            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var gameSeparatorIndex = line.IndexOf(":");

                var id = int.Parse(line.Substring(5, gameSeparatorIndex - 5));
                if (IsGamePossible(line[(gameSeparatorIndex + 2)..]))
                    result += id;
            }

            return result;
        }

        private static bool IsGamePossible(string game)
        {
            var sets = game.Split(";", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            foreach (var set in sets)
            {
                var colorCount = new Dictionary<string, int>
                {
                    {
                        "red", 0
                    },
                    {
                        "green", 0
                    },
                    {
                        "blue", 0
                    }
                };

                var cubes = set.Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                foreach (var cube in cubes)
                {
                    var roll = cube.Split(" ");
                    var number = int.Parse(roll[0]);
                    var color = roll[1];
                    colorCount[color] += number;
                }

                if (colorCount["red"] > 12 || colorCount["green"] > 13 || colorCount["blue"] > 14)
                    return false;
            }

            return true;
        }

        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            var result = 0;

            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;
                
                var gameSeparatorIndex = line.IndexOf(":");
                var fewestRequired = GetFewestRequiredCubes(line[(gameSeparatorIndex + 2)..]);
                var powerOfGame = fewestRequired.Red * fewestRequired.Green * fewestRequired.Blue;
                result += powerOfGame;
            }

            return result;
        }

        private (int Red, int Green, int Blue) GetFewestRequiredCubes(string game)
        {
            var colorCount = new Dictionary<string, int>
            {
                {
                    "red", 0
                },
                {
                    "green", 0
                },
                {
                    "blue", 0
                }
            };
            var sets = game.Split(";", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            foreach (var set in sets)
            {
                var cubes = set.Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                foreach (var cube in cubes)
                {
                    var roll = cube.Split(" ");
                    var number = int.Parse(roll[0]);
                    var color = roll[1];
                    if (colorCount[color] < number)
                        colorCount[color] = number;
                }
            }

            return (colorCount["red"], colorCount["green"], colorCount["blue"]);
        }
    }
}