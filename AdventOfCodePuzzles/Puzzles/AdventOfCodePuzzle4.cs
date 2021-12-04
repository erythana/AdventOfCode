using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles
{
    public class AdventOfCodePuzzle4 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            var gridLength = 5;

            var modifiedInput = input.ToArray();
            var bingoNumber = modifiedInput[0].Split(',');
            var grids = new Dictionary<string[,], bool[,]>();

            //Parse Grids
            for (var i = 1; i < modifiedInput.Length; i += gridLength) //Start with grids
            {
                grids.Add(ParseGrid(modifiedInput[i..(i + gridLength)], gridLength), new bool[5, 5]);
            }

            for (int i = 0; i < bingoNumber.Length; i++)
            {
                foreach (var grid in grids)
                {
                    MarkBingoField(grid, gridLength, bingoNumber[i]);
                    if (i >= gridLength && ReturnSumOnBingo(grid, gridLength, bingoNumber[i], out var bingoSum))
                        return bingoSum;
                }
            }

            return -1;
        }

        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            var winnerQueue = new Stack<int>();
            var gridLength = 5;

            var modifiedInput = input.ToArray();
            var bingoNumber = modifiedInput[0].Split(',');
            var grids = new Dictionary<string[,], bool[,]>();

            //Parse Grids
            for (var i = 1; i < modifiedInput.Length; i += gridLength)
            {
                grids.Add(ParseGrid(modifiedInput[i..(i + gridLength)], gridLength), new bool[5, 5]);
            }

            for (int i = 0; i < bingoNumber.Length; i++)
            {
                foreach (var grid in grids.ToList())
                {
                    MarkBingoField(grid, gridLength, bingoNumber[i]);
                    if (i >= gridLength && ReturnSumOnBingo(grid, gridLength, bingoNumber[i], out var bingoSum))
                    {
                        winnerQueue.Push(bingoSum);
                        grids.Remove(grid.Key);
                    }
                }
            }
            return winnerQueue.Pop();
        }

        private bool ReturnSumOnBingo(KeyValuePair<string[,], bool[,]> grid, int gridLength, string bingoNumber,
            out int bingoSum)
        {
            for (int x = 0; x < gridLength; x++)
            {
                var allTrueX = true;
                var allTrueY = true;
                for (int y = 0; y < gridLength; y++)
                {
                    allTrueX &= grid.Value[x, y];
                    allTrueY &= grid.Value[y, x];
                }
        
                if (!allTrueX && !allTrueY)
                    continue;
        
                var sum = ReturnSumOfGridByFunc(grid, gridLength, b => !b);
                bingoSum = int.Parse(bingoNumber) * sum;
                return true;
            }
        
            bingoSum = -1;
            return false;
        }
        
        private static void MarkBingoField(KeyValuePair<string[,], bool[,]> grid, int gridLength, string bingoNumber)
        {
            for (int x = 0; x < gridLength; x++)
            for (int y = 0; y < gridLength; y++)
                if (grid.Key[x, y] == bingoNumber)
                    grid.Value[x, y] = true;
        }

        private int ReturnSumOfGridByFunc(KeyValuePair<string[,], bool[,]> grid, int gridLength,
            Predicate<bool> matchFunc)
        {
            var sum = 0;
            for (int x = 0; x < gridLength; x++)
            for (int y = 0; y < gridLength; y++)
                if (matchFunc(grid.Value[x, y]))
                    sum += int.Parse(grid.Key[x, y]);

            return sum;
        }

        private string[,] ParseGrid(string[] modifiedInput, int gridLength)
        {
            var grid = new string[gridLength, gridLength];
            for (int i = 0; i < gridLength; i++)
            {
                var line = modifiedInput[i].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();
                for (int x = 0; x < gridLength; x++)
                    grid[i, x] = line[x];
            }

            return grid;
        }
    }
}