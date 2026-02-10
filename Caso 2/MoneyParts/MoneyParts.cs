using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyParts
{
    public class MoneyParts
    {
        private readonly int[] coins =
        {
            5, 10, 20, 50,
            100, 200, 500,
            1000, 2000, 5000,
            10000, 20000
        };

        public List<List<double>> build(string amountText)
        {
            if (!double.TryParse(amountText, NumberStyles.Any, CultureInfo.InvariantCulture, out double amount))
                throw new ArgumentException("Monto inválido");

            int target = (int)Math.Round(amount * 100);

            List<List<int>> temp = new List<List<int>>();
            Generate(target, 0, new List<int>(), temp);

            List<List<double>> result = new List<List<double>>();

            for (int i = 0; i < temp.Count; i++)
            {
                List<double> combo = new List<double>();

                for (int j = 0; j < temp[i].Count; j++)
                    combo.Add(temp[i][j] / 100.0);

                result.Add(combo);
            }

            return result;
        }

        private void Generate(int remaining, int start, List<int> current, List<List<int>> result)
        {
            if (remaining == 0)
            {
                result.Add(new List<int>(current));
                return;
            }

            for (int i = start; i < coins.Length; i++)
            {
                if (coins[i] > remaining)
                    continue;

                current.Add(coins[i]);
                Generate(remaining - coins[i], i, current, result);
                current.RemoveAt(current.Count - 1);
            }
        }
    }
}
