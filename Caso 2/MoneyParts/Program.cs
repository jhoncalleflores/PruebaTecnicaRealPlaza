using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyParts
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Ingrese monto en soles:");
            string input = Console.ReadLine();

            MoneyParts money = new MoneyParts();
            List<List<double>> result = money.build(input);

            Console.WriteLine("\nCombinaciones:");

            for (int i = 0; i < result.Count; i++)
            {
                Console.WriteLine("[" + string.Join(", ", result[i]) + "]");
            }
        }
    }
}
