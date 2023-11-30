using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCodeLib.Attributes;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles.Puzzles._2022
{
    [PuzzleType("Puzzle 2", 2022, 2)]
    public class AdventOfCode2022Puzzle2 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            var resultPoints = 0;
            
            Func<char, int> pointMapping = c => c switch
            {
                'A' or 'X' => 1,//rock
                'B' or 'Y' => 2,//paper
                'C' or 'Z' => 3,//scissors
            };
            
            foreach (var line in input.Where(s => !string.IsNullOrWhiteSpace(s)))
            {
                var enemyPoints = pointMapping(line[0]);
                var ownPoints = pointMapping(line[2]);
                
                if (enemyPoints == ownPoints) //draw
                    resultPoints += ownPoints + 3;
                else if (enemyPoints == ownPoints +1 || enemyPoints == ownPoints -2)//we loose
                    resultPoints += ownPoints + 0;
                else // we won
                    resultPoints += ownPoints + 6;
            }

            return resultPoints;
        }

        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            var resultPoints = 0;
            
            Func<char, int> pointMapping = c => c switch
            {
                'A' => 1,//rock
                'B' => 2,//paper
                'C' => 3,//scissors
            };

            foreach (var line in input.Where(s => !string.IsNullOrWhiteSpace(s)))
            {
                var enemyPoints = pointMapping(line[0]);
                var endGoal = line[2];

                if (endGoal == 'Y') //draw
                    resultPoints += enemyPoints + 3;
                else if (endGoal == 'X') //loose
                    resultPoints += (enemyPoints == 1 ? 3 : enemyPoints - 1) + 0;
                else if (endGoal == 'Z') //win
                    resultPoints += (enemyPoints == 3 ? 1 : enemyPoints + 1) + 6;
            }
            return resultPoints;
        }
    }
}