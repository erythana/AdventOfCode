using AdventOfCodeLib.Attributes;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles.Puzzles._2021
{
    [PuzzleType("Puzzle 7", 2021, 7)]
    public class AdventOfCodePuzzle7 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            
            var modifiedInput = input.First().Split(',').Select(int.Parse).ToList();
            var firstHorizontalPosition = modifiedInput.Min();
            var lastHorizontalPosition = modifiedInput.Max();
            var results = new List<int>();

            for (int i = firstHorizontalPosition; i <= lastHorizontalPosition; i++)
            {
                var totalFuel = 0;
                var resultCopy = modifiedInput.ToList();
                while (resultCopy.Any(x => x != i))
                {
                    for (var x = 0; x < resultCopy.Count; x++)
                    {
                        if (resultCopy[x] == i) continue;

                        var isHigher = resultCopy[x] > i;
                        resultCopy[x] += isHigher ? -1 : +1;
                        totalFuel++;
                    }
                }
                results.Add(totalFuel);
                
            }
            
            return results.Min();

        }

        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            var modifiedInput = input.First().Split(',').Select(int.Parse).ToList();
            var firstHorizontalPosition = modifiedInput.Min();
            var lastHorizontalPosition = modifiedInput.Max();
            var length = modifiedInput.Count;
            
            var results = new List<long>();

            for (int i = firstHorizontalPosition; i <= lastHorizontalPosition; i++)
            {
                var totalFuel = (long)0;
                var resultCopy = modifiedInput.ToList().Select(x => (position: x,nextFuelIncrement: 1)).ToList();
                while (resultCopy.Any(x => x.position != i))
                {
                    for (var x = 0; x < length; x++)
                    {
                        if (resultCopy[x].position == i) continue;

                        var isHigher = resultCopy[x].position > i;
                        var currentPosition = resultCopy[x].position;
                        var currentFuelIncrement = resultCopy[x].nextFuelIncrement;

                        totalFuel += currentFuelIncrement;
                        resultCopy[x] = (currentPosition + (isHigher ? -1 : +1), currentFuelIncrement+1);
                    }
                }
                results.Add(totalFuel);
                
            }
            
            return results.Min();
        }
    }
}