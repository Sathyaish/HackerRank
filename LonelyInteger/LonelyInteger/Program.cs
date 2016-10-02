using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LonelyInteger
{
    /*
     * https://www.hackerrank.com/challenges/lonely-integer
     * Consider an array of integers where all but one of the integers 
     * occur in pairs. In other words, every element in occurs exactly 
     * twice except for one unique element.
     * 
     * Find and print the unique element.
     */

    // I could have used a Dictionary<T, K> or a Set<T>
    // but that would have been cheating and would have defeated
    // the whole purpose of this exercise.
    // The purpose is to perform low-level operations oneself
    // so as to appreciate the cost of each alternative solution.
    // Therefore, I am using arrays.
    class Program
    {
        static void Main(string[] args)
        {
            var array = GetUserInput();

            if (array == null) return;

            var lonelyInteger = FindLonelyInteger(array);

            array.Print(leadingMessage: "Input: ");
            Console.WriteLine($"Lonely integer: {lonelyInteger}");
            Console.ReadKey();
        }

        private static int FindLonelyInteger(int[] array)
        {
            var len = array.Length;

            Debug.Assert(len.IsOdd(), 
                "Array length not odd",
                "One of the problem's premise is that the length of the array must be odd. Please try with an odd length array.");

            var median = (len - 1) / 2;
            int backward = median - 1, forward = median + 1;

            var tempArray = new int[median];
            tempArray[0] = array[median];
            var tempArrayCursor = 0;

            while (backward > 0 || forward < len)
            {
                if (array[backward] == array[forward])
                {
                    backward--;
                    forward++;
                    continue;
                }

                var backwardFoundAt = tempArray.Find(array[backward]);
                if (backwardFoundAt >= 0)
                {
                    tempArray.RemoveElementAt(backwardFoundAt);
                    tempArrayCursor--;
                }
                else
                {
                    tempArray[++tempArrayCursor] = array[backward];
                }

                backward--;

                var forwardFoundAt = tempArray.Find(array[forward]);
                if (forwardFoundAt >= 0)
                {
                    tempArray.RemoveElementAt(forwardFoundAt);
                    tempArrayCursor--;
                }
                else
                {
                    tempArray[++tempArrayCursor] = array[forward];
                }

                forward++;
            }

            return tempArray[0];
        }

        private static int[] GetUserInput()
        {
            var s =
                @"* Consider an array of integers where all but one of the integers 
* occur in pairs. In other words, every element in occurs exactly 
* twice except for one unique element.
* Find and print the unique element.";

            Console.WriteLine("The premise of this problem is as follows:");
            Console.WriteLine(s);

            Console.WriteLine("\nType in a bunch of numbers conforming to the array pattern described above, each number separated by a space:");

            var list = new List<int>();

            Console.ReadLine().Split()
                .ToList()
                .ForEach(t =>
                {
                    int n = 0;
                    var b = int.TryParse(t, out n);
                    if (b) list.Add(n);
                });

            if (list.Count == 0) return null;

            return list.ToArray();
        }
    }
}
