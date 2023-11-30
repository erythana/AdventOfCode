using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AdventOfCodeLib.Attributes;
using AdventOfCodeLib.Extensions;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles.Puzzles._2021;

[PuzzleType("Puzzle 15", 2021, 15)]
public class AdventOfCodePuzzle15 : PuzzleBase
{
    public override object SolvePuzzle1(IEnumerable<string> input)
    {
        var square = input.CreateGrid2D(x => x - '0');
        var pointToEndMap = new Dictionary<Point, int>();
        var movesDict = new Dictionary<Point, List<Point>>();
        var gridXSize = square[0].Count - 1;
        var gridYSize = square.Count - 1;

        Func<Point, int> PointValueMap = point => square[point.Y][point.X];
        Func<Point, List<Point>> possibleMoves = point =>
        {
            if (movesDict.ContainsKey(point))
                return movesDict[point];

            var moves = new List<Point>();
            if (point.X < gridXSize)
                moves.Add(new Point(point.X + 1, point.Y));
            if (point.Y < gridYSize)
                moves.Add(new Point(point.X, point.Y + 1));

            if (point.X > 0)
                moves.Add(new Point(point.X - 1, point.Y));
            if (point.Y > 0)
                moves.Add(new Point(point.X, point.Y - 1));

            movesDict.Add(point, moves);
            return moves;
        };

        var start = new Point(0, 0);
        var end = new Point(gridXSize, gridYSize);
        var visited = new Dictionary<Point, (int Distance, Point Previous)> {{start, (0, start)}};
        Func<Point, int> straightDistanceToEnd = (current) =>
        {
            if (pointToEndMap.ContainsKey(current))
                return pointToEndMap[current];

            var dist = Math.Abs(end.X - current.X) + Math.Abs(end.Y - current.Y);
            pointToEndMap.Add(current, dist);
            return dist;
        };

        var shortPQ = new PriorityQueue<Point, int>();
        shortPQ.Enqueue(start, int.MaxValue);

        while (shortPQ.Count > 0)
        {
            var point = shortPQ.Dequeue();
            var moves = possibleMoves(point).Where(x =>
                !visited.ContainsKey(x) || visited[point].Distance + PointValueMap(x) < visited[x].Distance);

            foreach (var move in moves)
            {
                var accumulatedRisk = visited[point].Distance + PointValueMap(move);
                if (!visited.ContainsKey(move))
                    visited.Add(move, (accumulatedRisk, point));
                else if (accumulatedRisk < visited[move].Distance)
                    visited[move] = (accumulatedRisk, point);

                shortPQ.Enqueue(move, PointValueMap(move) + straightDistanceToEnd(move));
            }
        }

        if (visited.TryGetValue(end, out var value))
            return value.Distance;

        return "Should not arrive here!";
    }

    public override object SolvePuzzle2(IEnumerable<string> input)
    {
        var square = input.CreateGrid2D(x => x - '0');
        

        var newSquare = new List<List<int>>();
        var repeating = 5;
        for (int y = 0; y < square.Count * repeating; y++)
        {
            newSquare.Add(new List<int>());
            for (int x = 0; x < square[y % square.Count].Count * repeating; x++)
            {
                var oldValue = square[y % square.Count][x % square.Count];
                var newValue = oldValue + x / square.Count + y/square.Count;
                if (newValue > 9)
                    newValue -= 9;

                newSquare[^1].Add(newValue);
            }
        }
        
        var pointToEndMap = new Dictionary<Point, int>();
        var movesDict = new Dictionary<Point, List<Point>>();
        var gridYSize = newSquare.Count - 1;
        var gridXSize = newSquare[0].Count - 1;

        Func<Point, int> PointValueMap = point => newSquare[point.Y][point.X];
        Func<Point, List<Point>> possibleMoves = point =>
        {
            if (movesDict.ContainsKey(point))
                return movesDict[point];

            var moves = new List<Point>();
            if (point.X < gridXSize)
                moves.Add(new Point(point.X + 1, point.Y));
            if (point.Y < gridYSize)
                moves.Add(new Point(point.X, point.Y + 1));

            if (point.X > 0)
                moves.Add(new Point(point.X - 1, point.Y));
            if (point.Y > 0)
                moves.Add(new Point(point.X, point.Y - 1));

            movesDict.Add(point, moves);
            return moves;
        };

        var start = new Point(0, 0);
        var end = new Point(gridXSize, gridYSize);
        var visited = new Dictionary<Point, (int Distance, Point Previous)> {{start, (0, start)}};
        Func<Point, int> straightDistanceToEnd = (current) =>
        {
            if (pointToEndMap.ContainsKey(current))
                return pointToEndMap[current];

            var dist = Math.Abs(end.X - current.X) + Math.Abs(end.Y - current.Y);
            pointToEndMap.Add(current, dist);
            return dist;
        };

        var shortPQ = new PriorityQueue<Point, int>();
        shortPQ.Enqueue(start, int.MaxValue);

        while (shortPQ.Count > 0)
        {
            var point = shortPQ.Dequeue();
            var moves = possibleMoves(point).Where(x =>
                !visited.ContainsKey(x) || visited[point].Distance + PointValueMap(x) < visited[x].Distance);

            foreach (var move in moves)
            {
                var accumulatedRisk = visited[point].Distance + PointValueMap(move);
                if (!visited.ContainsKey(move))
                    visited.Add(move, (accumulatedRisk, point));
                else if (accumulatedRisk < visited[move].Distance)
                    visited[move] = (accumulatedRisk, point);

                shortPQ.Enqueue(move, PointValueMap(move) + straightDistanceToEnd(move));
            }
        }

        if (visited.TryGetValue(end, out var value))
            return value.Distance;

        return "Should not arrive here!";
    }
}