using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCodeLib;
using AdventOfCodeLib.Attributes;
using AdventOfCodeLib.Extensions;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles.Puzzles._2023
{
    [PuzzleType(null, 2023, 7)]
    public class AdventOfCode2023Puzzle7 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input) 
            => CalculateCards(input, false);

        public override object SolvePuzzle2(IEnumerable<string> input) 
            => CalculateCards(input, true);

        private int CalculateCards(IEnumerable<string> input, bool level2)
        {
            var result = 0;
            var handResults = new Dictionary<string, (double CardValue, int Bid)>();
            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var game = line.Split(" ");
                var hand = game[0];
                var bid = (int)game[1].ExtractNumber();

                var points = PointsForHand(hand, level2);
                handResults.Add(hand, (points, bid));
            }

            var orderedHandResults = handResults.OrderBy(r => r.Value).ToArray();
            var amountCards = orderedHandResults[0].Key.Length; // just to keep this sort of generic, is 5 though lol

            var newMaximalValue = 0d;
            var bidMultiplier = 1;
            for (var i = 0; i < orderedHandResults.Length;)
            {
                var currentCardValue = orderedHandResults[i].Value.CardValue;
                if (currentCardValue > newMaximalValue)
                    newMaximalValue = currentCardValue;

                var sameValues = orderedHandResults[i..]
                    .TakeWhile(c => c.Value.CardValue == currentCardValue)
                    .ToList();

                var indexToHandValue = new Dictionary<int, double>();
                for (var y = 0; y < amountCards; y++)
                {
                    for (var k = 0; k < sameValues.Count; k++)
                    {
                        var hand = sameValues[k];
                        var adjustedHandValue = Math.Pow(10, 3 * (amountCards - y)) * GetCardStrength(hand.Key[y], level2);
                        if (!indexToHandValue.TryAdd(k, adjustedHandValue))
                            indexToHandValue[k] += adjustedHandValue;
                    }
                }

                foreach (var kvp in indexToHandValue.OrderBy(c => c.Value))
                {
                    var index = kvp.Key;
                    result += sameValues[index].Value.Bid * bidMultiplier;
                    bidMultiplier++;
                }

                i += sameValues.Count;//skip the same values (or, at least the current value)
            }
            return result;
        }

        private static int GetCardStrength(char card, bool level2) => card switch
        {
            'A' => 14,
            'K' => 13,
            'Q' => 12,
            'J' => level2
                ? 1
                : 11,
            'T' => 10,
            _ => card.ToInt()
        };


        private double PointsForHand(IEnumerable<char> cards, bool level2 = false)
        {
            var allCards = cards.ToList();

            var hand = new Dictionary<char, int>();
            foreach (var card in allCards)
                if (!hand.TryAdd(card, 1))
                    hand[card] += 1;

            var jokerCards = 0;
            if (level2)
                hand.Remove('J', out jokerCards);

            if (hand.Any(c => c.Value + jokerCards == 5) || jokerCards == 5) // five of a kind
                return Math.Pow(10, 10);
            if (hand.Any(c => c.Value + jokerCards == 4)) //four of a kind
                return Math.Pow(10, 9);
            if (hand.ContainsValue(3) && hand.ContainsValue(2) ||
                hand.Count(c => c.Value == 2) == 2 && jokerCards == 1) //fullhouse
                return Math.Pow(10, 8);
            if (hand.Any(c => c.Value + jokerCards == 3)) //three of a kind
                return Math.Pow(10, 7);
            if (hand.Count(c => c.Value == 2) == 2 || //two pair
                hand.Count(c => c.Value == 2) == 1 && jokerCards == 1)
                return Math.Pow(10, 6);
            if (hand.Count(c => c.Value == 2) == 1 || jokerCards == 1) //one pair
                return Math.Pow(10, 5);
            return Math.Pow(10, 4); //high card
        }
    }
}