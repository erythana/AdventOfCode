using System.Collections;
using System.Collections.Concurrent;
using System.Drawing;
using AdventOfCodeLib.Extensions;
using AdventOfCodePuzzles.Models;
using static System.Double;

namespace AdventOfCodePuzzles;

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
            var moves = possibleMoves(point).Where(x => !visited.ContainsKey(x) || visited[point].Distance + PointValueMap(x) < visited[x].Distance);
            
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
        throw new NotImplementedException();
    }
}