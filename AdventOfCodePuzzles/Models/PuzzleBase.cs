using System.Collections.Generic;
using AdventOfCodePuzzles.Attributes;
using AdventOfCodePuzzles.Interfaces;

namespace AdventOfCodePuzzles.Models
{
    public abstract class PuzzleBase : IPuzzleClass
    {
        [PuzzleMethod]
        public abstract object SolvePuzzle1(IEnumerable<string> input);

        [PuzzleMethod]
        public abstract object SolvePuzzle2(IEnumerable<string> input);
    }
}