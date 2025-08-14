using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy
{
    public class ScoreBoard
    {
        public void PrintScores(List<Player> players)
        {
            Console.WriteLine("----- Scoreboard: -----");
            foreach (var player in players)
            {
                Console.WriteLine($"{player.Name} - Total: {player.TotalScore}");
                foreach (var entry in player.Scores)
                {
                    string scoreDisplay = entry.Value.HasValue ? entry.Value.Value.ToString() : "-";
                    Console.WriteLine($"  {entry.Key}: {scoreDisplay}");
                }
                Console.WriteLine();
            }
        }
    }
}
