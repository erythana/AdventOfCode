using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace AdventOfCodeLib.Core
{
    public class Puzzle
    {
        public Puzzle(Type puzzleType, IEnumerable<MethodInfo> methods)
        {
            PuzzleType = puzzleType;
            Name =  PuzzleType.GetCustomAttribute(typeof(DescriptionAttribute)) is DescriptionAttribute description 
                ? description.Description : PuzzleType.Name;
            Methods = methods;
        }
        public string Name { get; }
        public Type PuzzleType { get; }
        public IEnumerable<MethodInfo> Methods { get; set; } 
    }
}