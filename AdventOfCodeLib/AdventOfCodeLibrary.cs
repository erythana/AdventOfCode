using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AdventOfCodeLib.Attributes;
using AdventOfCodeLib.Core;
using AdventOfCodeLib.Extensions;
using AdventOfCodeLib.Interfaces;

namespace AdventOfCodeLib
{
    public static class AdventOfCodeLibrary
    {
        public static IEnumerable<string> ReadInputFromFile(FileInfo file)
        {
            return file.Exists ? File.ReadAllLines(file.FullName) : Enumerable.Empty<string>();
        }

        public static IEnumerable<Puzzle> EnumeratePuzzles()
        {
            var assembly = Assembly.Load("AdventOfCodePuzzles");
            return assembly.GetTypes()
                .Where(t => t.GetInterface(nameof(IPuzzleClass)) is not null && !t.IsAbstract) 
                .Select(x =>
                    new Puzzle(x, 
                        methods: x
                        .GetMethods()
                        .Where(m => m.GetCustomAttribute(typeof(PuzzleMethodAttribute), true) is not null)));
        }

        public static long ExtractNumber(this string line)
        {
            long tempResult = 0;
            for (var i = 0; i < line.Length; i++)
            {
                if (line[i].IsDigit())
                    tempResult = tempResult * 10 + line[i].ToInt();
            }

            return tempResult;
        }
    }
}