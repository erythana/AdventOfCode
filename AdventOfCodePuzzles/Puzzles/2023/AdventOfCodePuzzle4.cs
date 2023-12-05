using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCodeLib.Attributes;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles.Puzzles._2023
{
    [PuzzleType(null, 2023, 4)]
    public class AdventOfCode2023Puzzle4 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            var result = 0;

            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var gameSeparatorIndex = line.IndexOf(":");
                var game = line[(gameSeparatorIndex + 2)..];

                var scratchCards = game.Split('|');
                var winningCards = scratchCards[0].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                var playerCards = scratchCards[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                ;

                var matchingNumber = playerCards.Count(pc => winningCards.Contains(pc));

                result += (int)Math.Pow(2, matchingNumber - 1); //-1 --> 0.5 --> rounded to 0
            }

            return result;
        }


        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            var result = 0;
            var solvedScratchcards = new Dictionary<int, int>();
            

            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var gameSeparatorIndex = line.IndexOf(":");
                var id = int.Parse(line.Substring(5, gameSeparatorIndex - 5));
                var game = line[(gameSeparatorIndex + 2)..];

                var scratchCards = game.Split('|');
                var winningCards = scratchCards[0].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                var playerCards = scratchCards[1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

                var matchingNumbers = playerCards.Count(pc => winningCards.Contains(pc));

                if (!solvedScratchcards.TryAdd(id, matchingNumbers))
                    solvedScratchcards[id] += matchingNumbers;
            }
            
            //This is probably a super stupid solution but after quite a lot of time i didn't find any other thing
            var cardQueue = new Queue<int>(solvedScratchcards.Keys);
            
            while (cardQueue.TryDequeue(out var card))
            {
                result += 1;
                var value = solvedScratchcards[card];
                for (var i = 1; i <= value; i++)
                    cardQueue.Enqueue(card+i);
            }

            return result;
        }

    }
}