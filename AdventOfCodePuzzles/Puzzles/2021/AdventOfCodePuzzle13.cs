using System.ComponentModel;
using System.Drawing;
using System.Text;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles;

[Description("2021 - Puzzle 13")]
public class AdventOfCodePuzzle13 : PuzzleBase
{
    public override object SolvePuzzle1(IEnumerable<string> input)
    {
        var dots = new List<Point>();
        var instructions = new List<(char axis, int length)>();
        var maxY = 0;
        var maxX = 0;
        foreach (var line in input)
        {
            if (line.StartsWith("fold"))
            {
                var lastSeparator = line.LastIndexOf('=');
                var axis = line[lastSeparator - 1];
                var length = int.Parse(line[(lastSeparator + 1)..line.Length]);
                instructions.Add((axis, length));
            }
            else if (!string.IsNullOrEmpty(line))
            {
                var values = line.Split(',');
                var x = int.Parse(values[0]);
                var y = int.Parse(values[1]);
                if (x > maxX)
                    maxX = x;

                if (y > maxY)
                    maxY = y;
                dots.Add(new Point(x, y));
            }
        }

        var grid = new int[maxY + 1, maxX + 1];
        foreach (var dot in dots)
        {
            grid[dot.Y, dot.X] += 1;
        }

        foreach (var foldInstruction in instructions.Take(1))//Only first because of P1
        {
            if (foldInstruction.axis == 'y')
            {
                for (int i = 1; i <= foldInstruction.length; i++)
                {
                    for (int x = 0; x < grid.GetLength(1); x++)
                    {
                        grid[foldInstruction.length - i, x] += grid[foldInstruction.length + i, x];

                        //remove the stuff from the previously folded areas
                        grid[foldInstruction.length, x] = 0;
                        grid[foldInstruction.length + i, x] = 0;
                    }
                }
            }
            else
            {
                for (int i = 1; i <= foldInstruction.length; i++)
                {
                    for (int y = 0; y < grid.GetLength(0); y++)
                    {
                        grid[y, foldInstruction.length - i] += grid[y, foldInstruction.length + i];

                        //remove the stuff from the previously folded areas
                        grid[y, foldInstruction.length] = 0;
                        grid[y, foldInstruction.length + i] = 0;
                    }
                }
            }
        }

        var counter = 0;
        foreach (var slot in grid)
        {
            if (slot > 0)
                counter++;
        }

        return counter;
    }

    public override object SolvePuzzle2(IEnumerable<string> input)
    {
       var dots = new List<Point>();
        var instructions = new List<(char axis, int length)>();
        var maxY = 0;
        var maxX = 0;
        foreach (var line in input)
        {
            if (line.StartsWith("fold"))
            {
                var lastSeparator = line.LastIndexOf('=');
                var axis = line[lastSeparator - 1];
                var length = int.Parse(line[(lastSeparator + 1)..line.Length]);
                instructions.Add((axis, length));
            }
            else if (!string.IsNullOrEmpty(line))
            {
                var values = line.Split(',');
                var x = int.Parse(values[0]);
                var y = int.Parse(values[1]);
                if (x > maxX)
                    maxX = x;

                if (y > maxY)
                    maxY = y;
                dots.Add(new Point(x, y));
            }
        }

        var grid = new int[maxY + 1, maxX + 1];
        foreach (var dot in dots)
            grid[dot.Y, dot.X] += 1;

        foreach (var foldInstruction in instructions)//Only first because of P1
        {
            var yDimLength = grid.GetLength(0);
            var xDimLength = grid.GetLength(1);
            if (foldInstruction.axis == 'y')
            {
                for (int i = 1; i <= foldInstruction.length; i++)
                {
                    for (int x = 0; x < xDimLength && foldInstruction.length+i<yDimLength; x++)
                    {
                        grid[foldInstruction.length - i, x] += grid[foldInstruction.length + i, x];

                        //remove the stuff from the previously folded areas
                        grid[foldInstruction.length, x] = 0;
                        grid[foldInstruction.length + i, x] = 0;
                    }
                }
            }
            else
            {
                for (int i = 1; i <= foldInstruction.length; i++)
                {
                    for (int y = 0; y < yDimLength && foldInstruction.length+i<xDimLength; y++)
                    {
                        grid[y, foldInstruction.length - i] += grid[y, foldInstruction.length + i];

                        //remove the stuff from the previously folded areas
                        grid[y, foldInstruction.length] = 0;
                        grid[y, foldInstruction.length + i] = 0;
                    }
                }
            }
        }
        
        var results = new List<Point>();
        for (int i = 1; i <= instructions.Count; i++)
        {
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    if (grid[y, x] >= 1)
                        results.Add(new Point(x, y));
                }
            }
        }
        var miY = results.Min(x => x.Y);
        var maY = results.Max(x => x.Y);
        var miX = results.Min(x => x.X);
        var maX = results.Max(x => x.X);

        var output = new StringBuilder();
        for (int newY = miY; newY <= maY; newY++)
        {
            for (int newX = miX; newX <= maX; newX++)
                output.Append(results.Contains(new Point(newX, newY)) ? '⬛' : '⬜');
            output.AppendLine();
        }
        return output.ToString();
    }
}