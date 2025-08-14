using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy
{
    public class Game
    {
        private List<Player> _players;
        private DiceThrower _diceThrower;
        private ScoreCalculator _scoreCalculator;
        private ScoreBoard _scoreBoard;

        public Game()
        {
            _players = new List<Player>();
            _diceThrower = new DiceThrower();
            _scoreCalculator = new ScoreCalculator();
            _scoreBoard = new ScoreBoard();
        }

        public void Start()
        {
            Console.WriteLine("Welcome to Yatzy!\n");

            // Get players
            for (int i = 1; i <= 2; i++)
            {
                Console.Write($"Enter name for Player {i}: ");
                string name = Console.ReadLine();
                _players.Add(new Player(name));
            }

            _scoreBoard.PrintScores(_players);

            // 15 turns (one for each category)
            for (int round = 1; round <= Enum.GetValues(typeof(Category)).Length; round++)
            {
                foreach (var player in _players)
                {
                    Console.WriteLine($"\n--- {player.Name}'s turn ---");
                    bool[] holdDice = new bool[5];
                    for (int throwCount = 1; throwCount <= 3; throwCount++)
                    {
                        _diceThrower.ThrowDice(holdDice);
                        Console.WriteLine($"Throws: {throwCount}");
                        _diceThrower.ShowHeldDice();

                        if (throwCount < 3)
                        {
                            Console.WriteLine("Please enter dice numbers to hold, or press ENTER to reroll them all or type 'k' to keep current roll: ");
                            string input = Console.ReadLine()?.Trim().ToUpper();

                            if (input == "K")
                            {
                                break; // Keep current roll and proceed to category selection
                            }

                            if (!string.IsNullOrEmpty(input))
                            {
                                var holdIndices = input.Split(' ')
                                                        .Where(x => int.TryParse(x, out _))
                                                        .Select(x => int.Parse(x) - 1)
                                                        .Where(i => i >= 0 && i < 5)
                                                        .ToArray();
                                holdDice = new bool[5];
                                foreach (var index in holdIndices)
                                {
                                    holdDice[index] = true;
                                }
                            }
                            else
                            {
                                holdDice = new bool[5]; // Reset holdDice to false for all dice
                            }
                        }
                    }
                    //_diceThrower.ThrowDice();
                    Console.WriteLine("You rolled: " + string.Join(", ", _diceThrower.Values));

                    // Choose category
                    Category chosenCategory;
                    while (true)
                    {
                        Console.WriteLine("Choose a category:");
                        foreach (Category c in Enum.GetValues(typeof(Category)))
                        {
                            if (!player.HasUsedCategory(c))
                                Console.WriteLine($"{(int)c}. {c}");
                        }

                        if (int.TryParse(Console.ReadLine(), out int choice) &&
                            Enum.IsDefined(typeof(Category), choice) &&
                            !player.HasUsedCategory((Category)choice))
                        {
                            chosenCategory = (Category)choice;
                            break;
                        }
                        Console.WriteLine("Invalid choice, try again.");
                    }

                    int points = _scoreCalculator.Calculate(chosenCategory, _diceThrower.Values);
                    player.AddScore(chosenCategory, points);
                    Console.WriteLine($"{player.Name} scored {points} points in {chosenCategory}!");

                    _scoreBoard.PrintScores(_players);
                }
            }

            // Winner
            var winner = _players.OrderByDescending(p => p.TotalScore).First();
            Console.WriteLine($" {winner.Name} wins with {winner.TotalScore} points");
        }
    }
}
