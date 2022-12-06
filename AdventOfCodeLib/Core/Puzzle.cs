using System;
using System.Collections.Generic;
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
            Year =  PuzzleType.GetCustomAttribute(typeof(PuzzleTypeAttribute)) is PuzzleTypeAttribute typeAttribute 
                ? typeAttribute.Year : 0;
            Day =  PuzzleType.GetCustomAttribute(typeof(PuzzleTypeAttribute)) is PuzzleTypeAttribute typeAttribute2 
                ? typeAttribute2.Day : 0;
            Name =  PuzzleType.GetCustomAttribute(typeof(DescriptionAttribute)) is DescriptionAttribute description 
                ? $"{Year} - {description.Description}"  : PuzzleType.Name;
            Methods = methods;
        }
        public string Name { get; }
        public int Year { get; }
        public int Day { get; }
        public Type PuzzleType { get; }
        public IEnumerable<MethodInfo> Methods { get; set; } 
    }
}