using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles
{
    [Description("2021 - Puzzle 5")]
    public class AdventOfCodePuzzle5 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            var dict = Parse(input);
            var resultDict = new Dictionary<(int x, int y), int>();

            foreach (var kvp in dict)
            {
                foreach (var target in kvp.Value)
                {
                    var isVertical = kvp.Key.x == target.x;
                    var isHorizontal = kvp.Key.y == target.y;
                    if (!isVertical && !isHorizontal) continue;
                    if (isVertical)
                    {
                        var from = kvp.Key.y <= target.y ? kvp.Key.y : target.y;
                        var to = kvp.Key.y <= target.y ? target.y : kvp.Key.y;
                        for (int yn = from; yn <= to; yn++)
                        {
                            var contains = resultDict.TryGetValue((target.x, yn), out var currentCount);
                            resultDict[(target.x, yn)] = ++currentCount;
                        }
                    }
                    else
                    {
                        var from = kvp.Key.x <= target.x ? kvp.Key.x : target.x;
                        var to = kvp.Key.x <= target.x ? target.x : kvp.Key.x;
                        for (int xn = from; xn <= to; xn++)
                        {
                            var contains = resultDict.TryGetValue((xn, target.y), out var currentCount);
                            resultDict[(xn, target.y)] = ++currentCount;
                        }
                    }
                }
            }

            return resultDict.Count(x => x.Value >= 2);
        }


        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            var dict = Parse(input);
            var resultDict = new Dictionary<(int x, int y), int>();

            foreach (var kvp in dict)
            {
                foreach (var target in kvp.Value)
                {
                    var isVertical = kvp.Key.x == target.x;
                    var isHorizontal = kvp.Key.y == target.y;
                    if (isVertical)
                    {
                        var from = kvp.Key.y >= target.y ? target.y : kvp.Key.y;
                        var to = kvp.Key.y >= target.y ? kvp.Key.y : target.y;
                        for (int yn = from; yn <= to; yn++)
                        {
                            resultDict.TryGetValue((target.x, yn), out var currentCount);
                            resultDict[(target.x, yn)] = ++currentCount;
                        }
                    }
                    else if (isHorizontal)
                    {
                        var from = kvp.Key.x >= target.x ? target.x : kvp.Key.x;
                        var to = kvp.Key.x >= target.x ? kvp.Key.x : target.x;
                        for (int xn = from; xn <= to; xn++)
                        {
                            resultDict.TryGetValue((xn, target.y), out var currentCount);
                            resultDict[(xn, target.y)] = ++currentCount;
                        }
                    }
                    else
                    {
                        if ((kvp.Key.x + target.x + 1) % 2 != (kvp.Key.y + target.y + 1) % 2)
                            continue; //Check if really diagonal

                        var xSourceHigher = kvp.Key.x >= target.x;
                        var ySourceHigher = kvp.Key.y >= target.y;
                        var xStep = xSourceHigher ? -1 : 1;
                        var yStep = ySourceHigher ? -1 : 1;

                        for (int x = kvp.Key.x, y = kvp.Key.y; x != target.x + xStep; x += xStep, y += yStep)
                        {
                            resultDict.TryGetValue((x, y), out var currentCount);
                            resultDict[(x, y)] = ++currentCount;
                        }
                    }
                }
            }

            return resultDict.Count(x => x.Value >= 2);
        }

        private static Dictionary<(int x, int y), List<(int x, int y)>> Parse(IEnumerable<string> input)
        {
            Dictionary<(int x, int y), List<(int x, int y)>> dict = new();
            foreach (var line in input)
            {
                var splittedLine = line
                    .Split("->", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                    .SelectMany(x =>
                        x.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
                    .Select(int.Parse).ToArray();

                (int x, int y) leftPart = (splittedLine[0], splittedLine[1]);
                (int x, int y) rightPart = (splittedLine[2], splittedLine[3]);
                if (dict.TryGetValue(leftPart, out var list))
                    list.Add(rightPart);
                else
                    dict.Add(leftPart, new List<(int x, int y)> {rightPart});
            }

            return dict;
        }
    }
}