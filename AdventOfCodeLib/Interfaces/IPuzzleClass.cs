using System.Collections.Generic;
using AdventOfCodeLib.Attributes;

namespace AdventOfCodeLib.Interfaces
{
    public interface IPuzzleClass
    {
        [PuzzleMethod]
        public object SolvePuzzle1(IEnumerable<string> input);
        [PuzzleMethod]
        public object SolvePuzzle2(IEnumerable<string> input);
    }
}
