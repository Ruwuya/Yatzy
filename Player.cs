using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy
{
    public class Player
    {
        public string Name { get; private set; }
        public Dictionary<Category, int?> Scores { get; private set; }

        public Player(string name)
        {
            Name = name;
            Scores = Enum.GetValues(typeof(Category))
                .Cast<Category>()
                .ToDictionary(category => category, category => (int?)null);
        }

        public int TotalScore => Scores.Values.Where(v => v.HasValue).Sum(v => v.Value);
        public bool HasUsedCategory(Category category) => Scores[category].HasValue;
        public void AddScore(Category category, int score)
        {
            if (Scores.ContainsKey(category) && !Scores[category].HasValue)
            {
                Scores[category] = score;
            }
            else
            {
                throw new InvalidOperationException($"Category {category} has already been used or does not exist.");
            }
        }
    }
}
