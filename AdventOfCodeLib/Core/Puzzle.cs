using System;
using System.Collections.Generic;
using System.Reflection;

namespace AdventOfCodeLib.Core
{
    public class Puzzle
    {
        public Puzzle(Type puzzleType, IEnumerable<MethodInfo> methods)
        {
            PuzzleType = puzzleType;
            Methods = methods;
        }
        public Type PuzzleType { get; set; }
        public IEnumerable<MethodInfo> Methods { get; set; } 
    }
}