using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy
{
    public class ScoreCalculator
    {
        /*
        public int CalculateScore(int[] diceValues, string category)
        {
            switch (category.ToLower())
            {
                case "ones":
                    return CountOccurrences(diceValues, 1);
                case "twos":
                    return CountOccurrences(diceValues, 2) * 2;
                case "threes":
                    return CountOccurrences(diceValues, 3) * 3;
                case "fours":
                    return CountOccurrences(diceValues, 4) * 4;
                case "fives":
                    return CountOccurrences(diceValues, 5) * 5;
                case "sixes":
                    return CountOccurrences(diceValues, 6) * 6;
                default:
                    throw new ArgumentException("Invalid category");
            }
        }
        */
        public int Calculate(Category category, int[] dice)
        {
            var counts = dice.GroupBy(d => d)
                             .ToDictionary(g => g.Key, g => g.Count());

            return category switch
            {
                Category.Ones => counts.ContainsKey(1) ? counts[1] * 1 : 0,
                Category.Twos => counts.ContainsKey(2) ? counts[2] * 2 : 0,
                Category.Threes => counts.ContainsKey(3) ? counts[3] * 3 : 0,
                Category.Fours => counts.ContainsKey(4) ? counts[4] * 4 : 0,
                Category.Fives => counts.ContainsKey(5) ? counts[5] * 5 : 0,
                Category.Sixes => counts.ContainsKey(6) ? counts[6] * 6 : 0,
                Category.Pair => counts.Where(kv => kv.Value >= 2)
                                           .OrderByDescending(kv => kv.Key)
                                           .Select(kv => kv.Key * 2)
                                           .FirstOrDefault(),
                Category.TwoPairs => counts.Where(kv => kv.Value >= 2)
                                           .OrderByDescending(kv => kv.Key)
                                           .Take(2)
                                           .Sum(kv => kv.Key * 2),
                Category.ThreeOfAKind => counts.Where(kv => kv.Value >= 3)
                                               .Select(kv => kv.Key * 3)
                                               .FirstOrDefault(),
                Category.FourOfAKind => counts.Where(kv => kv.Value >= 4)
                                              .Select(kv => kv.Key * 4)
                                              .FirstOrDefault(),
                Category.SmallStraight => dice.OrderBy(x => x).SequenceEqual(new[] { 1, 2, 3, 4, 5 }) ? 15 : 0,
                Category.LargeStraight => dice.OrderBy(x => x).SequenceEqual(new[] { 2, 3, 4, 5, 6 }) ? 20 : 0,
                Category.FullHouse => (counts.Any(kv => kv.Value == 3) && counts.Any(kv => kv.Value == 2)) ? dice.Sum() : 0,
                Category.Chance => dice.Sum(),
                Category.Yatzy => counts.Any(kv => kv.Value == 5) ? 50 : 0,
                _ => 0
            };
        }
    }
}
