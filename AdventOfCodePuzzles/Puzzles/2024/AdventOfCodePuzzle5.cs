using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AdventOfCodeLib;
using AdventOfCodeLib.Attributes;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles.Puzzles._2024
{
    [PuzzleType("Puzzle 5", 2024, 5)]
    public class AdventOfCode2024Puzzle5 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            var result = 0L;
            var inputText = string.Join(Environment.NewLine, input);
            var sections = inputText.Split(Environment.NewLine + Environment.NewLine);
            var tmpRules = new List<KeyValuePair<long, long>>();

            var orderingRulesRaw = sections[0]
                .Split(Environment.NewLine)
                .Select(l =>
                    l.Split("|", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries));

            foreach (var orderingRule in orderingRulesRaw)
            {
                var firstNumber = orderingRule[0].ExtractNumber();
                var secondNumber = orderingRule[1].ExtractNumber();

                tmpRules.Add(new KeyValuePair<long, long>(firstNumber, secondNumber));
            }


            var updates = sections[1].Split(Environment.NewLine)
                .Select(l =>
                    l.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                        .Select(long.Parse)
                        .ToList());

            foreach (var update in updates.Where(l => l.Count > 0))
            {
                var allBeforeInOrder = true;
                var allAfterInOrder = true;
                for (var i = 0; i < update.Count; i++)
                {
                    var currentPageNumber = update[i];
                    Debug.WriteLine("Current Number: " + currentPageNumber);
                    for (var j = 0; j < i; j++)
                    {
                        var allRulesComply = tmpRules //when we have a ruleset for both numbers
                            .Where(kvp => update.Contains(kvp.Value) && update.Contains(kvp.Key))
                            .Where(kvp => kvp.Key == currentPageNumber)
                            .All(k => update.IndexOf(k.Value) >= 0 &&
                                      update.IndexOf(k.Value) > update.IndexOf(currentPageNumber));

                        allBeforeInOrder &= j == update.Count - 1 || allRulesComply;
                    }

                    for (var j = i + 1; j < update.Count; j++)
                    {
                        var allRulesComply = tmpRules //when we have a ruleset for both numbers
                            .Where(kvp => update.Contains(kvp.Value) && update.Contains(kvp.Key))
                            .Where(kvp => kvp.Key == update[j])
                            .All(k => update.IndexOf(currentPageNumber) >= 0 &&
                                      update.IndexOf(currentPageNumber) < update.IndexOf(k.Value));

                        allAfterInOrder &= j == update.Count - 1 || allRulesComply;
                    }
                }

                if (allBeforeInOrder && allAfterInOrder)
                    result += GetMiddleNumber(update);
            }

            return result;
        }

        private long GetMiddleNumber(IList<long> numbers)
        {
            if (numbers.Count % 2 == 0)
                throw new ArgumentException("Can't get middle number from these numbers");

            return numbers[numbers.Count / 2]; //because its zero based we get the middle this way
        }

        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            var result = 0L;

            return result;
        }
    }
}