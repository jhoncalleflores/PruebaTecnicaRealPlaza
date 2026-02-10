using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderRange
{
    public class OrderRange
    {
        public Result build(List<int> numbers)
        {
            if (numbers == null)
                throw new ArgumentNullException(nameof(numbers));

            List<int> even = new List<int>();
            List<int> odd = new List<int>();

            for (int i = 0; i < numbers.Count; i++)
            {
                int value = numbers[i];

                if (value % 2 == 0)
                    even.Add(value);
                else
                    odd.Add(value);
            }

            SortAscending(even);
            SortAscending(odd);

            return new Result(even, odd);
        }

        private void SortAscending(List<int> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                for (int j = 0; j < list.Count - i - 1; j++)
                {
                    if (list[j] > list[j + 1])
                    {
                        int temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }
            }
        }
    }

    public class Result
    {
        public List<int> Even { get; }
        public List<int> Odd { get; }

        public Result(List<int> even, List<int> odd)
        {
            Even = even;
            Odd = odd;
        }
    }
}
