using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles
{
    [Description("2021 - Puzzle 6")]
    public class AdventOfCodePuzzle6 : PuzzleBase
    {
        
        
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            var modifiedInput = input.SelectMany(x => x.Split(',').Select(int.Parse));
            var days = 80;
            var resultArr = new int[9]; //eachIndex represents the reproduction Rate in days
            //fill the array
            foreach (var fish in modifiedInput)
            {
                resultArr[fish] += 1;
            }
            
            for (int i = 0; i < days; i++)
            {
                var lastValue = resultArr[0];
                Array.Copy(resultArr,1, resultArr, 0, resultArr.Length-1);
                resultArr[^1] = lastValue; //the fish which reproduced has now a new reproduction rate of 8 (last index)
                resultArr[^3] += resultArr[^1];//We add the number of reproduced fish to the 6th position
            }

            return resultArr.Sum();
        }
        
        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            var modifiedInput = input.SelectMany(x => x.Split(',').Select(int.Parse));
            var days = 256;
            var resultArr = new double[9]; //eachIndex represents the reproduction Rate in days
            //fill the array
            foreach (var fish in modifiedInput)
            {
                resultArr[fish] += 1;
            }
            
            for (int i = 0; i < days; i++)
            {
                var lastValue = resultArr[0];
                Array.Copy(resultArr,1, resultArr, 0, resultArr.Length-1);
                resultArr[^1] = lastValue; //the fish which reproduced has now a new reproduction rate of 8 (last index)
                resultArr[^3] += resultArr[^1];//We add the number of reproduced fish to the 6th position
            }

            return resultArr.Sum();
        }
    }
}