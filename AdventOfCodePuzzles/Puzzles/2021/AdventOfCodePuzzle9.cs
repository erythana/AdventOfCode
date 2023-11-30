using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AdventOfCodeLib.Attributes;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles.Puzzles._2021
{
    [PuzzleType("Puzzle 9", 2021, 9)]
    public class AdventOfCodePuzzle9 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            var grid = input.Select(x => x.ToArray().Select(x => x - '0').ToList()).ToList();
            var results = new List<int>();

            for (int y = 0; y < grid.Count; y++)
            {
                for (int x = 0; x < grid[y].Count; x++)
                {
                    var currentDigit = grid[y][x];
                    //If any of the following positions is false then we don't have a low point!
                    var smallerThanAdjacent = true;
                    smallerThanAdjacent &= IsSmallerThanTarget(currentDigit, new Point(x - 1, y));
                    smallerThanAdjacent &= IsSmallerThanTarget(currentDigit, new Point(x + 1, y));
                    smallerThanAdjacent &= IsSmallerThanTarget(currentDigit, new Point(x, y - 1));
                    smallerThanAdjacent &= IsSmallerThanTarget(currentDigit, new Point(x, y + 1));
                    
                    if (smallerThanAdjacent)
                        results.Add(currentDigit + 1);
                }
            }
            return results.Sum();
            
            bool IsSmallerThanTarget(int currentDigit, Point target)
            {
                if (target.Y < 0 || target.Y >= grid.Count || target.X < 0 || target.X >= grid[target.Y].Count)
                    return true; //If we are out of bounds we return true because this is a valid outcome

                return currentDigit < grid[target.Y][target.X];
            }
        }

        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            var grid = input.Select(x => x.ToArray().Select(x => x - '0').ToList()).ToList();
            var basinResults = new HashSet<(Point source, Point current, int number)>();
            var lowPerRun = 0;
            var highPerRun = 0;
            long result = 1;

            for (int y = 0; y < grid.Count; y++)
            {
                for (int x = 0; x < grid[y].Count; x++)
                {
                    var currentDigit = grid[y][x];

                    //If any of the following positions is false then we don't have a low point!
                    var smallerThanAdjacent = true;
                    smallerThanAdjacent &= IsSmallerThanTarget(currentDigit, new Point(x - 1, y));
                    smallerThanAdjacent &= IsSmallerThanTarget(currentDigit, new Point(x + 1, y));
                    smallerThanAdjacent &= IsSmallerThanTarget(currentDigit, new Point(x, y - 1));
                    smallerThanAdjacent &= IsSmallerThanTarget(currentDigit, new Point(x, y + 1));

                    if (smallerThanAdjacent)
                    {
                        lowPerRun = currentDigit;
                        var lowPoint = new Point(x, y);
                        AddAndCheckBasin(lowPoint, lowPoint, currentDigit);
                    }
                }
            }

            var groupedBySource = basinResults.GroupBy(x => x.source)
                .OrderByDescending(x => x.Count());

            var maxThreeGroupCount = groupedBySource.Take(3).Select(x => x.Count()).ToList();
            foreach (var groupCount in maxThreeGroupCount)
            {
                result *= groupCount;
            }

            return result;

            void CheckAdjacentBasin(Point source, Point target, int currentValue)
            {
                var targetValues = new List<int>(); 
                for (int i = lowPerRun -1; i <= highPerRun +1; i++)
                    targetValues.Add(i);
                
                if (IsPointValidBasin(new Point(target.X - 1, target.Y), targetValues, out var basinTarget))
                    AddAndCheckBasin(source, basinTarget, grid[basinTarget.Y][basinTarget.X]);

                if (IsPointValidBasin(new Point(target.X + 1, target.Y), targetValues, out basinTarget))
                    AddAndCheckBasin(source, basinTarget, grid[basinTarget.Y][basinTarget.X]);

                if (IsPointValidBasin(new Point(target.X, target.Y - 1), targetValues, out basinTarget))
                    AddAndCheckBasin(source, basinTarget, grid[basinTarget.Y][basinTarget.X]);

                if (IsPointValidBasin(new Point(target.X, target.Y + 1), targetValues, out basinTarget))
                    AddAndCheckBasin(source, basinTarget, grid[basinTarget.Y][basinTarget.X]);
            }

            void AddAndCheckBasin(Point source, Point target, int currentValue)
            {
                if (currentValue > highPerRun)
                    highPerRun = currentValue;
                
                if (basinResults.Add((source, target,
                    currentValue))) //basinResult is a Hashset, therefor only unique values as required
                    CheckAdjacentBasin(source, target,
                        currentValue); //Check all adjacent basins from the currently added one
            }

            bool IsSmallerThanTarget(int currentDigit, Point target)
            {
                if (target.Y < 0 || target.Y >= grid.Count || target.X < 0 || target.X >= grid[target.Y].Count)
                    return true; //If we are out of bounds we return true because this is a valid outcome

                return currentDigit < grid[target.Y][target.X];
            }

            bool IsPointValidBasin(Point target, List<int> targetValues, out Point basin)
            {
                basin = new Point(target.X, target.Y);
                foreach (var targetVal in targetValues)
                {
                    if (target.Y < 0 || target.Y >= grid.Count || target.X < 0 || target.X >= grid[target.Y].Count ||
                        targetVal is < 0 or 9)
                        continue;
                    
                    if (grid[target.Y][target.X] == targetVal)
                        return true;
                }

                return false;

            }
        }
    }
}