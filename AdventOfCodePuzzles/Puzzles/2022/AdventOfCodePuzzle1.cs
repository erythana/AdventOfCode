using AdventOfCodeLib.Attributes;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles.Puzzles._2022
{
    [PuzzleType("Puzzle 1", 2022, 1)]
    public class AdventOfCode2022Puzzle1 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            var tmpCalories = 0;
            var maxCalories = 0;
            foreach (var line in input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    maxCalories = tmpCalories > maxCalories ? tmpCalories : maxCalories;
                    tmpCalories = 0;
                }
                else
                {
                    tmpCalories += int.Parse(line);
                }
            }
            return maxCalories;
        }
        
        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            var highestValues = new HashSet<int>();            
            var tmpCalories = 0;
            foreach (var line in input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    highestValues.Add(tmpCalories);
                    tmpCalories = 0;
                }
                else
                {
                    tmpCalories += int.Parse(line);
                }
                
            }
            return highestValues.OrderByDescending(x => x).Take(3).Sum();
        }
    }
}