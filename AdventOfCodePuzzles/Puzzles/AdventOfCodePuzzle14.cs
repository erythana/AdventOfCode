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
        var steps = 2;
        var resultDict = new Dictionary<char, long>();
        var polymerTemplate = input.First();
        var pairInsertionRules = input
            .Skip(1)
            .Select(line => line.Split("->", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            .ToDictionary(instruction => instruction[0], instruction => char.Parse(instruction[1]));

        
        
        var stepDict = new Dictionary<int, List<char>>
        {
            [-1] = polymerTemplate.ToList(),
            [0] = polymerTemplate[1..].ToList(),
        };
        var length = polymerTemplate.Length;
        
        for (int step = 1; step <= steps; step++)
        {
            stepDict.Add(step, new List<char>());
            for (int i = 0; i < stepDict[step-1].Count; i++)
            {
                var lookup = new string( new []{stepDict[step-2][i], stepDict[step - 1][i]}) ;
                if (pairInsertionRules.TryGetValue(lookup, out var polymer))
                {
                    stepDict[step].Add(polymer);
                    length++;
                }
            }
        }

        var resultSB = new StringBuilder();
        for (int i = 0; i < polymerTemplate.Length - 1; i++)
        {
            for (int j = 0; j < stepDict.Values.Count -1; j++)
            {
                resultSB.Append(stepDict[j-1][i]);
                resultSB.Append(stepDict[j][i]);//as we start at -1 we can access this
            }
        }

        resultSB.Append(polymerTemplate[^1]);
        

        foreach (var polymer in resultSB.ToString())
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