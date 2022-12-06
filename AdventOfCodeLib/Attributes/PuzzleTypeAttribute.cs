using System.ComponentModel;

namespace AdventOfCodeLib.Attributes;

public class PuzzleTypeAttribute : DescriptionAttribute
{
    public PuzzleTypeAttribute(string description, int year = 0, int day = 0) : base(description)
    {
        Year = year;
        Day = day;
    }
    
    public int Year { get; }
    public int Day { get; }
}