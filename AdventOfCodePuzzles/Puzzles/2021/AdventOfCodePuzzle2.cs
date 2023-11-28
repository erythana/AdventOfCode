using AdventOfCodeLib.Attributes;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles.Puzzles._2021
{
    [PuzzleType("Puzzle 2", 2021, 2)]
    public class AdventOfCodePuzzle2 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            var positionX = 0;
            var positionY = 0;
            foreach (var line in input)
            {
                var lineResult = ParseInput(line);
                switch (lineResult.directions)
                {
                    case Directions.Up:
                        positionY += lineResult.count;
                        break;
                    case Directions.Down:
                        positionY -= lineResult.count;
                        break;
                    case Directions.Forward:
                        positionX += lineResult.count;
                        break;
                }
            }

            return Math.Abs(positionX * positionY);
        }

        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            var positionX = 0;
            var positionY = 0;
            var aim = 0;

            foreach (var line in input)
            {
                var lineResult = ParseInput(line);
                switch (lineResult.directions)
                {
                    case Directions.Up:
                        aim -= lineResult.count;
                        break;
                    case Directions.Down:
                        aim += lineResult.count;
                        break;
                    case Directions.Forward:
                        positionX += lineResult.count;
                        positionY += (aim * lineResult.count);
                        break;
                }
            }

            return Math.Abs(positionX * positionY);
        }

        private static (Directions directions, int count) ParseInput(string line)
        {
            var inputArray = line.Split(' ');
            var enumResult = Enum.Parse<Directions>(inputArray[0], true);
            return (enumResult, int.Parse(inputArray[1]));
        }

        private enum Directions
        {
            Up,
            Down,
            Forward
        }
    }
}