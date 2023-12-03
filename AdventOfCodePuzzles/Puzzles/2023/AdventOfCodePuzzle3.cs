using System.Collections.Generic;
using System.Linq;
using AdventOfCodeLib.Attributes;
using AdventOfCodeLib.Extensions;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles.Puzzles._2023
{
    [PuzzleType(null, 2023, 3)]
    public class AdventOfCode2023Puzzle3 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            var result = 0;
            var grid = input.CreateGrid2D(x => x);

            for (var y = 0; y < grid.Count; y++)
            {
                for (var x = 0; x < grid[y].Count; x++)
                {
                    var character = grid[y][x];
                    if (character.IsDigit() || character == '.')
                        continue;
                    
                    var adjacentParts = GetAdjacentPartNumbers(grid, x, y);
                    result += adjacentParts.Sum();
                }
            }
            
            return result;
        }

        private IEnumerable<int> GetAdjacentPartNumbers(List<List<char>> grid, int xPosition, int yPosition)
        {
            for (var y = yPosition - 1; y <= yPosition + 1; y++)
            {
                for (var x = xPosition - 1; x <= xPosition + 1; x++)
                {
                    if (y < 0 || y >= grid.Count ||
                        x < 0 || x >= grid[y].Count ||
                        !grid[y][x].IsDigit())
                        continue;
                    
                    yield return DiscoverDigit(grid, x, y);
                    while (x < grid[y].Count && grid[y][x].IsDigit())
                        x++;
                }
            }
        }

        private int DiscoverDigit(List<List<char>> grid, int xPosition, int yPosition)
        {
            while (xPosition > 0 && grid[yPosition][xPosition - 1].IsDigit())
                xPosition--;

            var fullDigit = 0;
            while (xPosition < grid[yPosition].Count && grid[yPosition][xPosition].IsDigit())
            {
                fullDigit = fullDigit * 10 + grid[yPosition][xPosition].ToInt();
                xPosition++;
            }

            return fullDigit;
        }


        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            var result = 0;
            var grid = input.CreateGrid2D(x => x);

            for (var y = 0; y < grid.Count; y++)
            {
                for (var x = 0; x < grid[y].Count; x++)
                {
                    var character = grid[y][x];
                    if (character != '*')
                        continue;
                    
                    var adjacentParts = GetAdjacentPartNumbers(grid, x, y).ToList();
                    if (adjacentParts.Count == 2)
                        result += adjacentParts[0] * adjacentParts[1];
                }
            }
            
            return result;
        }
        
    }
}