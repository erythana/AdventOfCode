using System.ComponentModel;
using System.Reflection;
using AdventOfCodeLib.Attributes;

namespace AdventOfCodeLib.Core
{
    public class Puzzle
    {
        public Puzzle(Type puzzleType, IEnumerable<MethodInfo> methods)
        {
            PuzzleType = puzzleType;

            var puzzleTypeAttribute = PuzzleType.GetCustomAttribute(typeof(PuzzleTypeAttribute)) as PuzzleTypeAttribute;
            
            Year =  puzzleTypeAttribute?.Year ?? 0;
            Day =  puzzleTypeAttribute?.Day ?? 0;
            Name = !string.IsNullOrWhiteSpace(puzzleTypeAttribute?.Description)
                ? $"{Year} - {puzzleTypeAttribute.Description}" : PuzzleType.Name;
            Methods = methods;
        }
        public string Name { get; }
        public int Year { get; }
        public int Day { get; }
        public Type PuzzleType { get; }
        public IEnumerable<MethodInfo> Methods { get; set; } 
    }
}