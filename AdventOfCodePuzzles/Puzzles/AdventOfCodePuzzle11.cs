using System.Drawing;
using AdventOfCodeLib.Extensions;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles;

public class AdventOfCodePuzzle11 : PuzzleBase
{
    private class Octopus
    {
        private int lightLevel;
        
        public Octopus(int lightLevel)
        {
            this.lightLevel = lightLevel;
            Locked = false;
        }

        public int FlashCounter { get; private set; }

        public void Flash()
        {
            if (Locked) return;
            
            Locked = true;
            FlashCounter++;
            lightLevel = 0;
        }

        public bool ShouldFlashAfterIncrease()
        {
            if (!Locked)
            {
                lightLevel++;
            }

            return lightLevel > 9;
        }

        public bool Locked { get; set; }
    }

    public override object SolvePuzzle1(IEnumerable<string> input)
    {
        var flashes = 0;
        var steps = 100;
        var grid = input.Select(x => x.Select(x => new Octopus(x - '0')).ToList()).ToList();
        var octopusToNeighbors = new Dictionary<Octopus, IEnumerable<Octopus>>();

        Func<List<List<Octopus>>, Point, IEnumerable<Point>> getNeighbors = (grid2d, current) =>
        {
            var neighbors = new List<Point>
            {
                new(current.X - 1, current.Y + 1),
                new(current.X - 1, current.Y),
                new(current.X - 1, current.Y - 1),
                new(current.X + 1, current.Y - 1),
                new(current.X + 1, current.Y),
                new(current.X + 1, current.Y + 1),
                new(current.X, current.Y - 1),
                new(current.X, current.Y + 1),
            };
            return neighbors.Where(p => p.Y >= 0 && p.Y < grid2d.Count && p.X >= 0 && p.X < grid2d[p.Y].Count);
        };

        for (int i = 0; i < steps; i++)
        {
            var octopusFlashList = new HashSet<Octopus>();
            for (int y = 0; y < grid.Count; y++)
            {
                for (int x = 0; x < grid[y].Count; x++)
                {
                    var currentPosition = new Point(x, y);
                    var octopus = grid[y][x];

                    if (!octopusToNeighbors.ContainsKey(octopus))
                    {
                        var neighborList = getNeighbors(grid, currentPosition)
                            .Select(oPoint => grid[oPoint.Y][oPoint.X]).ToList();
                        octopusToNeighbors.Add(octopus, neighborList);
                    }

                    if (octopus.ShouldFlashAfterIncrease())
                        octopusFlashList.Add(octopus);
                }
            }

            while (octopusFlashList.Any())
            {
                var octo = octopusFlashList.First();
                octo.Flash();
                octopusFlashList.Remove(octo);

                foreach (var neighbor in octopusToNeighbors[octo])
                {
                    if (neighbor.ShouldFlashAfterIncrease())
                        octopusFlashList.Add(neighbor);
                }
            }

            grid.ApplyActionToGrid(o => o.Locked = false);
        }

        grid.ApplyActionToGrid(o => flashes += o.FlashCounter);
        return flashes;
    }

    public override object SolvePuzzle2(IEnumerable<string> input)
    {
        var grid = input.CreateGrid2D(i => new Octopus(i - '0'));
        var octopusToNeighbors = new Dictionary<Octopus, IEnumerable<Octopus>>();
        var stepCounter = 0;

        Func<List<List<Octopus>>, Point, IEnumerable<Point>> getNeighbors = (grid2d, current) =>
        {
            var neighbors = new List<Point>
            {
                new(current.X - 1, current.Y + 1),
                new(current.X - 1, current.Y),
                new(current.X - 1, current.Y - 1),
                new(current.X + 1, current.Y - 1),
                new(current.X + 1, current.Y),
                new(current.X + 1, current.Y + 1),
                new(current.X, current.Y - 1),
                new(current.X, current.Y + 1),
            };
            return neighbors.Where(p => p.Y >= 0 && p.Y < grid2d.Count && p.X >= 0 && p.X < grid2d[p.Y].Count);
        };

        while (true)
        {
            stepCounter++;
            var flashCounter = 0;
            var octopusFlashList = new HashSet<Octopus>();
            for (int y = 0; y < grid.Count; y++)
            {
                for (int x = 0; x < grid[y].Count; x++)
                {
                    var currentPosition = new Point(x, y);
                    var octopus = grid[y][x];

                    if (!octopusToNeighbors.ContainsKey(octopus))
                    {
                        var neighborList = getNeighbors(grid, currentPosition)
                            .Select(oPoint => grid[oPoint.Y][oPoint.X]).ToList();
                        octopusToNeighbors.Add(octopus, neighborList);
                    }

                    if (octopus.ShouldFlashAfterIncrease())
                        octopusFlashList.Add(octopus);
                }
            }

            while (octopusFlashList.Any())
            {
                var octo = octopusFlashList.First();
                octo.Flash();
                flashCounter++;
                octopusFlashList.Remove(octo);

                foreach (var neighbor in octopusToNeighbors[octo])
                {
                    if (neighbor.ShouldFlashAfterIncrease())
                        octopusFlashList.Add(neighbor);
                }
            }
            if (flashCounter == grid.Count * grid.Count)
                break;

            grid.ApplyActionToGrid(o => o.Locked = false);
        }

        return stepCounter;
    }
}