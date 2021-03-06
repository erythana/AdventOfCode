using System.Collections.Generic;
using AdventOfCodePuzzles.Attributes;

namespace AdventOfCodePuzzles.Interfaces
{
    public interface IPuzzleClass
    {
        [PuzzleMethod]
        public abstract object SolvePuzzle1(IEnumerable<string> input);
        [PuzzleMethod]
        public abstract object SolvePuzzle2(IEnumerable<string> input);
    }
}
