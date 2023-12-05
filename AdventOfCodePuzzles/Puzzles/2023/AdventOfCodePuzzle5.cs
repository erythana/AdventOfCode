using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdventOfCodeLib.Attributes;
using AdventOfCodeLib.Extensions;
using AdventOfCodePuzzles.Models;

namespace AdventOfCodePuzzles.Puzzles._2023
{
    [PuzzleType(null, 2023, 5)]
    public class AdventOfCode2023Puzzle5 : PuzzleBase
    {
        public override object SolvePuzzle1(IEnumerable<string> input)
        {
            var modifiedInput = input.ToArray();

            var seeds = new List<long>();
            var seedToSoilMap = new HashSet<(long DestinationRangeStart, long SourceRangeStart, long Length)>();
            var soilToFertilizerMap = new HashSet<(long DestinationRangeStart, long SourceRangeStart, long Length)>();
            var fertilizerToWaterMap = new HashSet<(long DestinationRangeStart, long SourceRangeStart, long Length)>();
            var waterToLightMap = new HashSet<(long DestinationRangeStart, long SourceRangeStart, long Length)>();
            var lightToTemperatureMap = new HashSet<(long DestinationRangeStart, long SourceRangeStart, long Length)>();
            var temperatureToHumidityMap = new HashSet<(long DestinationRangeStart, long SourceRangeStart, long Length)>();
            var humidityToLocationMap = new HashSet<(long DestinationRangeStart, long SourceRangeStart, long Length)>();


            for (var i = 0; i < modifiedInput.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(modifiedInput[i]))
                    continue;

                if (i == 0)
                    seeds.AddRange(modifiedInput[i][(modifiedInput[i].IndexOf(":") + 1)..].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(long.Parse));

                if (modifiedInput[i].StartsWith("seed-to-soil map:"))
                    MapSection(modifiedInput, i, seedToSoilMap);
                else if (modifiedInput[i].StartsWith("soil-to-fertilizer map:"))
                    MapSection(modifiedInput, i, soilToFertilizerMap);
                else if (modifiedInput[i].StartsWith("fertilizer-to-water map:"))
                    MapSection(modifiedInput, i, fertilizerToWaterMap);
                else if (modifiedInput[i].StartsWith("water-to-light map:"))
                    MapSection(modifiedInput, i, waterToLightMap);
                else if (modifiedInput[i].StartsWith("light-to-temperature map:"))
                    MapSection(modifiedInput, i, lightToTemperatureMap);
                else if (modifiedInput[i].StartsWith("temperature-to-humidity map:"))
                    MapSection(modifiedInput, i, temperatureToHumidityMap);
                else if (modifiedInput[i].StartsWith("humidity-to-location map:"))
                    MapSection(modifiedInput, i, humidityToLocationMap);
            }

            var lowestLocation = long.MaxValue;
            foreach (var seed in seeds)
            {
                var soil = GetMappedValue(seed, seedToSoilMap);
                var fertilizer = GetMappedValue(soil, soilToFertilizerMap);
                var water = GetMappedValue(fertilizer, fertilizerToWaterMap);
                var light = GetMappedValue(water, waterToLightMap);
                var temperature = GetMappedValue(light, lightToTemperatureMap);
                var humidity = GetMappedValue(temperature, temperatureToHumidityMap);
                var location = GetMappedValue(humidity, humidityToLocationMap);

                if (location < lowestLocation)
                    lowestLocation = location;
            }

            return lowestLocation;
        }

        private static long GetMappedValue(long value, HashSet<(long DestinationRangeStart, long SourceRangeStart, long Length)> mappings)
        {
            foreach (var mapping in mappings)
            {
                if (value >= mapping.SourceRangeStart && value < mapping.SourceRangeStart + mapping.Length)
                    return mapping.DestinationRangeStart - mapping.SourceRangeStart + value;
            }

            return value;
        }

        private void MapSection(string[] modifiedInput, long currentPosition, HashSet<(long, long, long)> targetMapping)
        {
            currentPosition++;
            while (currentPosition != modifiedInput.Length && modifiedInput[currentPosition] != "")
            {
                var mapping = RangeMapper(modifiedInput[currentPosition]);
                targetMapping.Add((mapping.DestinationRangeStart, mapping.SourceRangeStart, mapping.Length));
                currentPosition++;
            }

            (long DestinationRangeStart, long SourceRangeStart, long Length) RangeMapper(string mappableString)
            {
                var values = mappableString.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
                return (DestinationRangeStart: values[0], SourceRangeStart: values[1], Length: values[2]);
            }
        }


        public override object SolvePuzzle2(IEnumerable<string> input)
        {
            var modifiedInput = input.ToArray();

            var seedToSoilMap = new HashSet<(long DestinationRangeStart, long SourceRangeStart, long Length)>();
            var soilToFertilizerMap = new HashSet<(long DestinationRangeStart, long SourceRangeStart, long Length)>();
            var fertilizerToWaterMap = new HashSet<(long DestinationRangeStart, long SourceRangeStart, long Length)>();
            var waterToLightMap = new HashSet<(long DestinationRangeStart, long SourceRangeStart, long Length)>();
            var lightToTemperatureMap = new HashSet<(long DestinationRangeStart, long SourceRangeStart, long Length)>();
            var temperatureToHumidityMap = new HashSet<(long DestinationRangeStart, long SourceRangeStart, long Length)>();
            var humidityToLocationMap = new HashSet<(long DestinationRangeStart, long SourceRangeStart, long Length)>();


            for (var i = 0; i < modifiedInput.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(modifiedInput[i]))
                    continue;

                if (modifiedInput[i].StartsWith("seed-to-soil map:"))
                    MapSection(modifiedInput, i, seedToSoilMap);
                else if (modifiedInput[i].StartsWith("soil-to-fertilizer map:"))
                    MapSection(modifiedInput, i, soilToFertilizerMap);
                else if (modifiedInput[i].StartsWith("fertilizer-to-water map:"))
                    MapSection(modifiedInput, i, fertilizerToWaterMap);
                else if (modifiedInput[i].StartsWith("water-to-light map:"))
                    MapSection(modifiedInput, i, waterToLightMap);
                else if (modifiedInput[i].StartsWith("light-to-temperature map:"))
                    MapSection(modifiedInput, i, lightToTemperatureMap);
                else if (modifiedInput[i].StartsWith("temperature-to-humidity map:"))
                    MapSection(modifiedInput, i, temperatureToHumidityMap);
                else if (modifiedInput[i].StartsWith("humidity-to-location map:"))
                    MapSection(modifiedInput, i, humidityToLocationMap);
            }

            var lowestValues = new ConcurrentBag<long>();
            var inputRange = modifiedInput[0][(modifiedInput[0].IndexOf(":") + 1)..].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

            //Computer goes BRRRRRRRRR. Probably better the other way around (search from lowest location(s) to the fitting seed) but don't want to hassle with that at this time.
            //Learned a bit of Paralell.For, so thats a plus...
            Parallel.For(0, inputRange.Length / 2, () => long.MaxValue,
                (i, _, lowest) =>
                {
                    var startNumber = inputRange[2 * i];
                    for (var seed = startNumber; seed < startNumber + inputRange[2 * i + 1]; seed++)
                    {
                        var soil = GetMappedValue(seed, seedToSoilMap);
                        var fertilizer = GetMappedValue(soil, soilToFertilizerMap);
                        var water = GetMappedValue(fertilizer, fertilizerToWaterMap);
                        var light = GetMappedValue(water, waterToLightMap);
                        var temperature = GetMappedValue(light, lightToTemperatureMap);
                        var humidity = GetMappedValue(temperature, temperatureToHumidityMap);
                        var location = GetMappedValue(humidity, humidityToLocationMap);

                        if (location < lowest)
                            lowest = location;
                    }

                    return lowest;
                },
                lowest => lowestValues.Add(lowest));
            return lowestValues.Min();
        }
    }
}