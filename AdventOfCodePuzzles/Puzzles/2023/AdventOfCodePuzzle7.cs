using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCodeLib;
using AdventOfCodeLib.Attributes;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles.Puzzles._2023
{
    [PuzzleType(null, 2023, 7)]
    public class AdventOfCode2023Puzzle7 : PuzzleBase
    {
        private static bool imLazyJokerMode;

        private double PointsForHand(IEnumerable<char> cards, bool level2 = false)
        {
            var allCards = cards.ToList();
            var hand = GetCardDistribution(allCards);

            if (level2)
            {
                var maxCombination = 0d;

                var allExceptJoker = allCards.Where(c => c != 'J');
                for (var i = 0; i < allCards.Count; i++)
                {
                    if (allCards[i] != 'J')
                        continue;
                    
                    foreach (var other in allExceptJoker)
                    {
                        var tempHand = cards.ToArray();
                        tempHand[i] = other;
                        var currentPoints = GetPointsForHand(GetCardDistribution(tempHand));
                        if (currentPoints > maxCombination)
                            maxCombination = currentPoints;
                    }
                    return maxCombination;
                }
                
                var jokerCards = allCards.Where(c => c == 'J').ToList();
                foreach (var joker in jokerCards)
                {
                    var otherCards = allCards.Except(jokerCards);
                    // var thisCombination = GetPointsForHand()
                }



                return maxCombination;
            }

            double GetPointsForHand(Dictionary<char, int> currentHand)
            {
                return currentHand switch
                {
                    { Count: 1 } => Math.Pow(10, 10), // five of a kind
                    { Count: 2 } d when d.Any(c => c.Value == 4) => Math.Pow(10, 9), //four of a kind
                    { Count: 2 } d when d.ContainsValue(3) && d.ContainsValue(2) => Math.Pow(10, 8), //fullhouse
                    { Count: 3 } d when d.ContainsValue(3) => Math.Pow(10, 7), //three of a kind
                    { Count: 3 } d when d.Values.Count(v => v == 2) == 2 => Math.Pow(10, 6), //two pair
                    { Count: 4 } => Math.Pow(10, 5), //one pair
                    { Count: 5 } _ => Math.Pow(10, 4), //high card
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            return GetPointsForHand(hand);
        }

        private static Dictionary<char, int> GetCardDistribution(IEnumerable<char> allCards)
        {

            var hand = new Dictionary<char, int>();
            foreach (var card in allCards)
            {
                if (!hand.TryAdd(card, 1))
                    hand[card] += 1;
            }

            return hand;
        }

        public override object SolvePuzzle1(IEnumerable<string> input)
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

                var points = PointsForHand(hand);
                handResults.Add(hand, (points, bid));
            }

            var orderedHandResults = handResults.OrderBy(r => r.Value).ToArray();
            var amountCards = orderedHandResults[0].Key.Length; // just to keep this sort of generic, is 5 though lol

            var currentMax = 0d;
            var bidMultiplier = 1;
            for (var i = 0; i < orderedHandResults.Length; i++)
            {
                var handResult = orderedHandResults[i];
                var thisValue = handResult.Value.CardValue;
                if (thisValue > currentMax)
                    currentMax = thisValue;

                var sameValues = orderedHandResults[i..].TakeWhile(c => c.Value.CardValue == thisValue).ToList();

                var sameValueIndexToRank = new Dictionary<int, double>();
                for (var y = 0; y < amountCards; y++)
                {
                    for (var k = 0; k < sameValues.Count; k++)
                    {
                        var handSet = sameValues[k];
                        var handSetValue = Math.Pow(10, 2 * (amountCards - y)) * StrengthDictionary[handSet.Key[y]];
                        if (!sameValueIndexToRank.TryAdd(k, handSetValue))
                            sameValueIndexToRank[k] += handSetValue;
                    }
                }

                foreach (var indexer in sameValueIndexToRank.OrderBy(c => c.Value))
                {
                    var valueIndex = indexer.Key;
                    result += sameValues[valueIndex].Value.Bid * bidMultiplier;
                    bidMultiplier++;
                }

                i += sameValues.Count - 1;
            }

            return result;
        }

        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            imLazyJokerMode = true;
            
            var result = 0;
            var handResults = new Dictionary<string, (double CardValue, int Bid)>();
            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var game = line.Split(" ");
                var hand = game[0];
                var bid = (int)game[1].ExtractNumber();

                var points = PointsForHand(hand);
                handResults.Add(hand, (points, bid));
            }

            var orderedHandResults = handResults.OrderBy(r => r.Value).ToArray();
            var amountCards = orderedHandResults[0].Key.Length; // just to keep this sort of generic, is 5 though lol

            var currentMax = 0d;
            var bidMultiplier = 1;
            for (var i = 0; i < orderedHandResults.Length; i++)
            {
                var handResult = orderedHandResults[i];
                var thisValue = handResult.Value.CardValue;
                if (thisValue > currentMax)
                    currentMax = thisValue;

                var sameValues = orderedHandResults[i..].TakeWhile(c => c.Value.CardValue == thisValue).ToList();

                var sameValueIndexToRank = new Dictionary<int, double>();
                for (var y = 0; y < amountCards; y++)
                {
                    for (var k = 0; k < sameValues.Count; k++)
                    {
                        var handSet = sameValues[k];
                        var handSetValue = Math.Pow(10, 2 * (amountCards - y)) * StrengthDictionary[handSet.Key[y]];
                        if (!sameValueIndexToRank.TryAdd(k, handSetValue))
                            sameValueIndexToRank[k] += handSetValue;
                    }
                }

                foreach (var indexer in sameValueIndexToRank.OrderBy(c => c.Value))
                {
                    var valueIndex = indexer.Key;
                    result += sameValues[valueIndex].Value.Bid * bidMultiplier;
                    bidMultiplier++;
                }

                i += sameValues.Count - 1;
            }

            return result;
        }
        
        private Dictionary<char, int> StrengthDictionary = new Dictionary<char, int>
        {
            {
                'A', 14
            },
            {
                'K', 13
            },
            {
                'Q', 12
            },
            {
                'J', imLazyJokerMode
                    ? 11
                    : 1
            },
            {
                'T', 10
            },
            {
                '9', 9
            },
            {
                '8', 8
            },
            {
                '7', 7
            },
            {
                '6', 6
            },
            {
                '5', 5
            },
            {
                '4', 4
            },
            {
                '3', 3
            },
            {
                '2', 2
            }
        };
    }
}