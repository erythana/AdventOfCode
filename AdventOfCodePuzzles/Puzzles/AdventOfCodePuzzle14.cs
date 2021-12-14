using System.Text;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles;

public class AdventOfCodePuzzle14 : PuzzleBase
{
    public override object SolvePuzzle1(IEnumerable<string> input)
    {
        var steps = 10;
        var resultDict = new Dictionary<char, long>();
        var polymerTemplate = input.First();
        var pairInsertionRules = input
            .Skip(1)
            .Select(line => line.Split("->", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            .ToDictionary(instruction => instruction[0], instruction => instruction[1]);

        for (int step = 0; step < steps; step++)
        {
            var insertionList = new List<(int, string)>();
            for (int i = 0; i < polymerTemplate.Length - 1; i++)
            {
                var lookup = polymerTemplate[i..(i + 2)];
                if (pairInsertionRules.TryGetValue(lookup, out var polymer))
                {
                    insertionList.Add((i + 1, polymer));
                }
            }

            var sb = new StringBuilder(polymerTemplate);
            for (int i = 0; i < insertionList.Count; i++)
            {
                var index = insertionList[i].Item1;
                var value = insertionList[i].Item2;
                sb.Insert(index + i, value);
            }

            polymerTemplate = sb.ToString();
        }

        foreach (var polymer in polymerTemplate)
            AddAndCountDictionary(polymer);
        
        void AddAndCountDictionary(char lookup)
        {
            if (!resultDict.TryGetValue(lookup, out var value))
                resultDict.Add(lookup, 1);
            resultDict[lookup] = ++value;
        }

        var max = resultDict.Max(x => x.Value);
        var min = resultDict.Min(x => x.Value);
        
        
        return max-min;
    }

    public override object SolvePuzzle2(IEnumerable<string> input)
    {
        var steps = 40;
        var resultDict = new Dictionary<char, long>();
        var polymerTemplate = input.First();
        var pairInsertionRules = input
            .Skip(1)
            .Select(line => line.Split("->", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            .ToDictionary(instruction => instruction[0], instruction => instruction[1]);

        for (int step = 0; step < steps; step++)
        {
            var insertionList = new List<(int Index, string Value)>();
            for (int i = 0; i < polymerTemplate.Length - 1; i++)
            {
                var lookup = polymerTemplate[i..(i + 2)];
                if (pairInsertionRules.TryGetValue(lookup, out var polymer))
                {
                    insertionList.Add((i + 1, polymer));
                }
            }

            var sb = new StringBuilder(polymerTemplate);
            for (int i = 0; i < insertionList.Count; i++)
            {
                var index = insertionList[i].Index;
                var value = insertionList[i].Value;
                sb.Insert(index + i, value);
            }

            polymerTemplate = sb.ToString();
        }

        foreach (var polymer in polymerTemplate)
            AddAndCountDictionary(polymer);
        
        void AddAndCountDictionary(char lookup)
        {
            if (!resultDict.TryGetValue(lookup, out var value))
                resultDict.Add(lookup, 1);
            resultDict[lookup] = ++value;
        }

        var max = resultDict.Max(x => x.Value);
        var min = resultDict.Min(x => x.Value);
        
        
        return max-min;
    }
}