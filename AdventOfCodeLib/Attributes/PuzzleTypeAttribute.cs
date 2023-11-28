using System.ComponentModel;

namespace AdventOfCodeLib.Attributes;

public class PuzzleTypeAttribute(string description, int year = 0, int day = 0) : DescriptionAttribute(description)
{
    public int Year { get; } = year;
    public int Day { get; } = day;
}