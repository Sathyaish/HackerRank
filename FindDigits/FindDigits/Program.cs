using System;

namespace FindDigits
{
    class Program
    {
        static void Main(string[] args)
        {
            int t = Convert.ToInt32(Console.ReadLine());

            for (int a0 = 0; a0 < t; a0++)
            {
                int n = Convert.ToInt32(Console.ReadLine());

                var count = 0;

                if (n < 0) n = -1 * n;

                string s = n.ToString();

                foreach (var c in s)
                {
                    int digit = Convert.ToInt32(c.ToString());

                    if (digit == 0) continue;

                    if (n % digit == 0)
                    {
                        count++;
                    }
                }

                Console.WriteLine(count);
            }
        }        
    }
}