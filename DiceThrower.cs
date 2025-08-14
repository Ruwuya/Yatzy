using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy
{
    public class DiceThrower
    {
        private Random _random = new Random();
        public int[] Values { get; private set; }

        public DiceThrower()
        {
            Values = new int[5];
        }

        public void ThrowDice(bool[] holdDice = null)
        {
            for (int i = 0; i < Values.Length; i++)
            {
                if (holdDice == null || !holdDice[i])
                {
                    Values[i] = _random.Next(1, 7); // Generates a number between 1 and 6
                }
            }
        }
        public void ShowHeldDice()
        {
            Console.WriteLine("Dice: " + string.Join(" ", Values.Select((v, i) => $"[{i + 1}:{v}]")));
        }
    }
}
