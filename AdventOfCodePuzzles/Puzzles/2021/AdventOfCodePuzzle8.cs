using AdventOfCodeLib.Attributes;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles.Puzzles._2021
{
    [PuzzleType("Puzzle 8", 2021, 8)]
    public class AdventOfCodePuzzle8 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            var lengthToPossibleNumbersMap = new Dictionary<int, List<int>>
            {
                [2] = new() {1},
                [3] = new() {7},
                [4] = new() {4},
                [5] = new() {2, 3, 5},
                [6] = new() {0, 6, 9},
                [7] = new() {8},
            };

            var uniqueCounter = 0;
            foreach (var line in input)
            {
                var splittedInput = line.Split('|', StringSplitOptions.RemoveEmptyEntries);
                var outputEncodedNumbers = splittedInput[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (var encodedNumber in outputEncodedNumbers)
                {
                    var possibleDigits = lengthToPossibleNumbersMap[encodedNumber.Length];
                    if (possibleDigits.Count is not 1)
                        continue;
                    uniqueCounter++;
                }
            }

            return uniqueCounter;
        }


        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            var puzzleResult = 0;
            
            var lengthToPossibleNumbersMap = new Dictionary<int, List<int>>
            {
                [2] = new() {1},
                [3] = new() {7},
                [4] = new() {4},
                [5] = new() {2, 3, 5},
                [6] = new() {0, 6, 9},
                [7] = new() {8},
            };

            var charToUniqueValue = new Dictionary<char, int>
            {
                ['a'] = 1,
                ['b'] = 2,
                ['c'] = 4,
                ['d'] = 8,
                ['e'] = 16,
                ['f'] = 32,
                ['g'] = 64
            };
            Func<char[], int> charsToUniqueValue = chars => chars.Sum(c => charToUniqueValue[c]);
            var flagValueToDigitMapping = new Dictionary<int, int>();
            
            var dig2Chr = new Dictionary<int, char[]>();

            
            
            foreach (var line in input)
            {
                flagValueToDigitMapping.Clear();
                dig2Chr.Clear();
                
                var modifiedInput = line.Split('|', StringSplitOptions.RemoveEmptyEntries);
                var uniqueEncodedNumbers = modifiedInput[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).OrderByDescending(x => x.Length);
                var uniqueEncodedOutput = modifiedInput[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

                foreach (var encodedNumber in uniqueEncodedNumbers)
                {
                    var len = encodedNumber.Length;
                    var chars = encodedNumber.ToCharArray();

                    if (lengthToPossibleNumbersMap[len].Count != 1) continue;

                    var digit = lengthToPossibleNumbersMap[len][0];
                    flagValueToDigitMapping.Add(charsToUniqueValue(chars), digit);
                    dig2Chr.Add(digit, chars);
                }

                while (flagValueToDigitMapping.Count != 10)
                {
                    foreach (var encodedNumber in uniqueEncodedNumbers)
                    {
                        var len = encodedNumber.Length;
                        var possibleNumbers = lengthToPossibleNumbersMap[len];
                        if (possibleNumbers.Count == 1 ||
                            flagValueToDigitMapping.ContainsKey(charsToUniqueValue(encodedNumber.ToCharArray()))) continue;

                        var mask9 = dig2Chr[4].Union(dig2Chr[7]);
                        
                        var chars = encodedNumber.ToCharArray();
                        switch (len)
                        {
                            case 6:
                            {
                                var isNine = encodedNumber.Except(mask9).Count() == 1;
                                var isZero = encodedNumber.Except(dig2Chr[1]).Count() == 4;
                                if (isNine)
                                {
                                    flagValueToDigitMapping.Add(charsToUniqueValue(chars), 9);
                                    dig2Chr.Add(9, chars);
                                }
                                else if (isZero)
                                {
                                    flagValueToDigitMapping.Add(charsToUniqueValue(chars), 0);
                                    dig2Chr.Add(0, chars);
                                }
                                else
                                {
                                    flagValueToDigitMapping.Add(charsToUniqueValue(chars), 6);
                                    dig2Chr.Add(6, chars);
                                }

                                break;
                            }
                            case 5:
                            {
                                var isTwo = encodedNumber.Except(dig2Chr[9]).Count() == 1;
                                var isThree = encodedNumber.Except(dig2Chr[1]).Count() == 3;
                                if (isTwo)
                                {
                                    flagValueToDigitMapping.Add(charsToUniqueValue(chars), 2);
                                    dig2Chr.Add(2, chars);
                                }
                                else if (isThree)
                                {
                                    flagValueToDigitMapping.Add(charsToUniqueValue(chars), 3);
                                    dig2Chr.Add(3, chars);
                                }
                                else
                                {
                                    flagValueToDigitMapping.Add(charsToUniqueValue(chars), 5);
                                    dig2Chr.Add(5, chars);
                                }

                                break;
                            }
                        }
                    }
                }

                var result = 0;
                foreach (var output in uniqueEncodedOutput)
                {
                    var chars = output.ToCharArray();
                    result = result * 10 + flagValueToDigitMapping[charsToUniqueValue(chars)];
                }

                puzzleResult += result;
            }
            
            return puzzleResult;
        }
    }
}