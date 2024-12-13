using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCodeLib;
using AdventOfCodeLib.Attributes;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles.Puzzles._2024
{
    [PuzzleType("Puzzle 4", 2024, 4)]
    public class AdventOfCode2024Puzzle4 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            var result = 0;
            var inputList = input.Where(l => !string.IsNullOrWhiteSpace(l)).ToList();

            var x = inputList[0].Length;
            var y = inputList.Count;
            var searchLength = "XMAS".Length;
            
            var searchRight = x - 1 + "XMAS".Length < inputList[y].Length;
            var searchLeft = x + 1 - searchLength >= 0;
            var searchDown = y - 1 + searchLength < inputList.Count;
            var searchUp = y + 1 - searchLength >= 0;
            var searchUpRight = searchUp && searchRight;
            var searchUpLeft = searchUp && searchLeft;
            var searchDownRight = searchDown && searchRight;
            var searchDownLeft = searchDown && searchLeft;
            
            for (var i = 0; i < inputList.Count; i++)
            {
                for (var j = 0; j < inputList[i].Length; j++)
                {
                    if (inputList[i][j] is not 'X') continue;
                    
                    result += DiscoverWord("XMAS", inputList, i, j, SearchDirections.SearchDown | SearchDirections.SearchUp | SearchDirections.SearchRight | SearchDirections.SearchLeft);
                }
            }
            return result;
        }
        
        [Flags]
        private enum SearchDirections : byte
        {
            SearchRight,
            SearchLeft,
            SearchDown,
            SearchUp
        }

        private int DiscoverWord(string searchWord, List<string> inputList, int y, int x, SearchDirections searchDirection)
        {
            var count = 0;
            var searchLength = searchWord.Length;

            var searchRight = searchDirection.HasFlag(SearchDirections.SearchRight);
            var searchLeft = searchDirection.HasFlag(SearchDirections.SearchLeft);
            var searchDown = searchDirection.HasFlag(SearchDirections.SearchDown);
            var searchUp = searchDirection.HasFlag(SearchDirections.SearchUp);
            var searchUpRight = searchUp && searchRight;
            var searchUpLeft = searchUp && searchLeft;
            var searchDownRight = searchDown && searchRight;
            var searchDownLeft = searchDown && searchLeft;
            
            for (var i = 0; i < searchLength; i++)
            {
                if (searchRight)
                    if (inputList[y][x + i] != searchWord[i])
                        searchRight = false;

                if (searchLeft)
                    if (inputList[y][x - i] != searchWord[i])
                        searchLeft = false;

                if (searchDown)
                    if (inputList[y+i][x] != searchWord[i])
                        searchDown = false;

                if (searchUp)
                    if (inputList[y-i][x] != searchWord[i])
                        searchUp = false;
                
                if (searchUpRight)
                    if (inputList[y-i][x+i] != searchWord[i])
                        searchUpRight = false;
                
                if (searchUpLeft)
                    if (inputList[y-i][x-i] != searchWord[i])
                        searchUpLeft = false;
                
                if (searchDownRight)
                    if (inputList[y+i][x+i] != searchWord[i])
                        searchDownRight = false;
                
                if (searchDownLeft)
                    if (inputList[y+i][x-i] != searchWord[i])
                        searchDownLeft = false;
            }
            
            if (searchRight)
                count++;
            if (searchLeft)
                count++;
            if(searchDown)
                count++;
            if(searchUp)
                count++;
            if(searchUpRight)
                count++;
            if(searchUpLeft)
                count++;
            if(searchDownRight)
                count++;
            if(searchDownLeft)
                count++;
            return count;
        }

        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            var result = 0;
            // var inputList = input.Where(l => !string.IsNullOrWhiteSpace(l)).ToList();
            // for (var i = 0; i < inputList.Count; i++)
            // {
            //     for (var j = 0; j < inputList[i].Length; j++)
            //     {
            //         if (inputList[i][j] is 'A')
            //         {
            //             DiscoverWord("SAM", inputList, i, j, SearchDirections.SearchDown | SearchDirections.SearchUp);
            //             DiscoverWord("MAS", inputList, i, j);
            //         }
            //         
            //     }
            // }
            return result;
        }
    }
}