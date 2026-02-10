using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderRange
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Ingrese números separados por coma:");
            string input = Console.ReadLine();

            List<int> numbers = ParseInput(input);

            OrderRange order = new OrderRange();
            Result result = order.build(numbers);

            Console.WriteLine("Pares: " + string.Join(", ", result.Even));
            Console.WriteLine("Impares: " + string.Join(", ", result.Odd));
        }

        static List<int> ParseInput(string input)
        {
            List<int> list = new List<int>();
            string[] parts = input.Split(',');

            for (int i = 0; i < parts.Length; i++)
            {
                if (int.TryParse(parts[i].Trim(), out int value))
                    list.Add(value);
            }

            return list;
        }
    }
}
