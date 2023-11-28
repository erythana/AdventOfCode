using AdventOfCodeLib.Attributes;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles.Puzzles._2021
{
    [PuzzleType("Puzzle 10", 2021, 10)]
    public class AdventOfCodePuzzle10 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            var result = 0;

            var openCharMapping = new Dictionary<char, char>()
            {
                ['('] = ')',
                ['['] = ']',
                ['{'] = '}',
                ['<'] = '>',
            };
            var closeCharMapping = new Dictionary<char, char>()
            {
                [')'] = '(',
                [']'] = '[',
                ['}'] = '{',
                ['>'] = '<',
            };

            var charPointsMapping = new Dictionary<char, int>()
            {
                [')'] = 3,
                [']'] = 57,
                ['}'] = 1197,
                ['>'] = 25137
            };

            foreach (var line in input)
            {
                var invalidBracketFound = false;
                var openedBrackets = new Stack<char>();
                for (var i = 0; i < line.Length && !invalidBracketFound; i++)
                {
                    if (openCharMapping.ContainsKey(line[i]))
                    {
                        openedBrackets.Push(line[i]);
                    }
                    else if (closeCharMapping.TryGetValue(line[i], out var value)) // it is a valid closing bracket
                    {
                        if (value == openedBrackets.Peek())
                            openedBrackets.Pop();
                        else
                        {
                            invalidBracketFound = true;
                            result += charPointsMapping[line[i]];
                        }
                    }
                }
            }

            return result;
        }

        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            
            var resultList = new List<ulong>();
            var openToCloseMapping = new Dictionary<char, char>()
            {
                ['('] = ')',
                ['['] = ']',
                ['{'] = '}',
                ['<'] = '>',
            };
            var closeToOpenMapping = new Dictionary<char, char>()
            {
                [')'] = '(',
                [']'] = '[',
                ['}'] = '{',
                ['>'] = '<',
            };
            var charPointsMapping = new Dictionary<char, uint>()
            {
                [')'] = 1,
                [']'] = 2,
                ['}'] = 3,
                ['>'] = 4
            };
            
            foreach (var line in input)
            {
                ulong result = 0;
                var openedBrackets = new Stack<char>();
                var closedBrackets = new Queue<char>();
                foreach (var chr in line)
                {
                    if (openToCloseMapping.ContainsKey(chr))
                        openedBrackets.Push(chr);
                    else if (closeToOpenMapping.TryGetValue(chr, out var openChar)) // it is a valid closing bracket
                    {
                        char c;
                        if (openedBrackets.TryPeek(out c) && c == openChar)
                            openedBrackets.Pop();
                        else
                            while (openedBrackets.TryPeek(out c) && c != openChar)
                            {
                                //We encountered a closing char, we need to pop all openchars and enque the closing char so we match the correct closing tag
                                openedBrackets.Pop();
                                closedBrackets.Enqueue(chr);
                            }
                    }
                }
                //There are some leftovers, as long as these match we can pop/dequeue them!
                while (openedBrackets.TryPeek(out var opened) && closedBrackets.TryPeek(out var closed) && opened == closed)
                {
                    openedBrackets.Pop();
                    closedBrackets.Dequeue();
                }
                
                //Calculate the results
                while (openedBrackets.Any())
                {
                    var current = openedBrackets.Pop();
                    result = result * 5 + charPointsMapping[openToCloseMapping[current]];
                }
                
                //No more open brackets but misaligned closing brackets - whatya doing?
                if (closedBrackets.Any())
                    continue;
                
                resultList.Add(result);
            }
            return resultList.OrderBy(x => x).ElementAt(resultList.Count/2); // Order and then take the "middle" result - input is assumed to be always uneven!
        }
    }
}