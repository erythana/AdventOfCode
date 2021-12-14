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
        
        
        return max - min;
    }

    public override object SolvePuzzle2(IEnumerable<string> input)
    {
        var resultDict = new Dictionary<char, long>();
        var steps = 40;
        var polymerTemplate = input.First();
        var pairInsertionRules = input
            .Skip(1)
            .Select(line => line.Split("->", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            .ToDictionary(instruction => instruction[0], instruction => char.Parse(instruction[1]));


        var stepDictionary = new Dictionary<int, List<Func<int, char>>>();
        var startFuncs = new Func<int, char>(x => polymerTemplate[x]);
        stepDictionary.Add(0, new List<Func<int, char>>());
        for (int i = 0; i < polymerTemplate.Length; i++)
        {
            var capturedIndex = i;
            stepDictionary[0].Add((_ => startFuncs(capturedIndex)));
        }
        
        for (int i = 1; i <= steps; i++)
        {
            var inserts = 0;
            stepDictionary.Add(i, new List<Func<int, char>>());
            for (int x = 0; x < stepDictionary[i-1].Count - 1; x++)
            {
                var capturedStepIndex = i;
                var capturedIndex = x;
                stepDictionary[i].Insert(x+inserts, _ => stepDictionary[capturedStepIndex-1][capturedIndex](_));
                var previousCharLeft = stepDictionary[i - 1][x](capturedIndex);
                var previousCharRight = stepDictionary[i - 1][x+1](capturedIndex+1);
                
                var lookup = new string(new[] {previousCharLeft, previousCharRight });
                if (pairInsertionRules.TryGetValue(lookup, out var polymer))
                {
                    inserts++;
                    stepDictionary[i].Insert(x+inserts, _ => polymer);
                }
            }
            var prev = stepDictionary[i - 1];
            stepDictionary[i].Add(_ => prev[^1](_));
        }


        var resultSB = new StringBuilder();
        foreach (var funcs in stepDictionary[steps])
        {
            resultSB.Append(funcs(58));
        }
        
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
        return max - min;
    }
}