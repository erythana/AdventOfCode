using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AdventOfCodeLib;
using AdventOfCodeLib.Attributes;
using AdventOfCodeLib.Extensions;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles.Puzzles._2024
{
    [PuzzleType("Puzzle 6", 2024, 6)]
    public class AdventOfCode2024Puzzle6 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            var result = new HashSet<(int, int)>();

            var grid = input.Where(l => !string.IsNullOrWhiteSpace(l)).CreateGrid2D((c) => c);

            var (currentY, currentX) = FindGuardPosition(grid);
            result.Add((currentY, currentX));
            var currentDirectionModifier =
                GetOrModifyDirection(grid, currentX, currentY, (0, 0)); //find initial position

            do
            {
                var nextX = currentX + currentDirectionModifier.xModifier;
                var nextY = currentY + currentDirectionModifier.yModifier;

                if (grid.IsWithinBounds(nextX, nextY) && grid[nextY][nextX] == '#')
                    currentDirectionModifier = GetOrModifyDirection(grid, nextX, nextY, currentDirectionModifier);
                else
                {
                    if(grid.IsWithinBounds(currentX, currentY))
                        result.Add((currentY, currentX));
                    else
                        break;
                        
                    currentX = nextX;
                    currentY = nextY;
                }
            } while (true);

            return result.Count;
        }

        private (int xModifier, int yModifier) GetOrModifyDirection(List<List<char>> grid, int fromX, int fromY,
            (int xModifier, int yModifier) currentModifier)
        {
            var currentChar = grid[fromY][fromX];
            return currentChar switch
            {
                '<' => (-1, 0),
                '^' => (0, -1),
                '>' => (1, 0),
                'v' => (0, 1),
                '#' => SwapDirections(currentModifier),
                _ => (currentModifier.xModifier, currentModifier.yModifier)
            };

            (int xDirection, int yDirection) SwapDirections((int x, int y) direction)
            {
                return direction.x != 0
                    ? (0, direction.x == 1 ? 1 : -1) //we moved left or right, now up or down
                    : (direction.y == 1 ? -1 : 1, 0);
            }
        }

        private (int, int) FindGuardPosition(List<List<char>> grid)
        {
            for (var i = 0; i < grid.Count; i++)
            for (var j = 0; j < grid[i].Count; j++)
                if (grid[i][j] is '<' or '>' or '^' or 'v')
                    return (i, j);
            return (0, 0);
        }

        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            var result = 0L;

            /* Infinite loop checker
             maybe iterate over grid and find all boundaries (#), for each of them (might be reduceable when already encountered from a direction!?)
             check the sides and check whether there is a boundary which is one the same line*/
            
            
            
            return result;
        }
    }
}