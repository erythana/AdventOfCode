using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles
{
    public class AdventOfCodePuzzle3 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            var inputCount = 0;
            var length = input.FirstOrDefault().Length;
            var intFounds = new int[length];
            
            foreach (var line in input)
            {
                inputCount++;
                for (int i = 0; i < length; i++)
                {
                    var currentNumber = line[i] - '0';
                    if (currentNumber == 1)
                        intFounds[i] += currentNumber;
                }
            }
            
            var binaryRepresentationGamma = string.Empty;
            for (int i = 0; i < intFounds.Length; i++)
            {
                binaryRepresentationGamma += (inputCount / 2 < intFounds[i]) ? 1 : 0;
            }
            var maxInt = Math.Pow(2, length) - 1;
            var gamma = Convert.ToInt32(binaryRepresentationGamma, 2);
            var epsilon = maxInt - gamma;
            
            return gamma*epsilon;
        }

        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            return null;
        }
    }
}