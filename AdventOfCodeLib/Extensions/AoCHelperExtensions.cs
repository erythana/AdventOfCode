using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCodeLib.Extensions
{
    public static class AoCHelperExtensions
    {
        public static void ApplyActionToGrid<T>(this List<List<T>> grid, Action<T> action)
        {
            for (int y = 0; y < grid.Count; y++)
            for (int x = 0; x < grid[y].Count; x++)
                action(grid[y][x]);
        }

        public static List<List<T>> CreateGrid2D<T>(this IEnumerable<string> input, Func<char, T> converter)
        {
            return input.Select(x => x.Select(converter).ToList()).ToList();
        }

        public static bool IsDigit(this char character) => character >= 48 && character <= 57;
        
        public static int ToInt(this char character) => character - '0';
    }
}