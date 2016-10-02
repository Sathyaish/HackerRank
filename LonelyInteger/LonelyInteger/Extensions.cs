using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LonelyInteger
{
    public static class Extensions
    {
        public static void Print<T>(this IEnumerable<T> source,
            TextWriter writer = null,
            string separator = ", ",
            string leadingMessage = null,
            bool newLineAfterSequence = true,
            bool throwOnEmptySource = true,
            string emptySequenceMessage = "No items to print.",
            string bracket = "[]")
        {
            TextWriter w = writer ?? Console.Out;
            IEnumerable<string> mutated = null;
            var count = 0;
            var builder = new StringBuilder();
            string openingBracket = null;
            string closingBracket = null;

            if (source == null || source.Count() == 0)
            {
                if (throwOnEmptySource) throw new ArgumentNullException("source");

                emptySequenceMessage.Print(w, newLineAfterSequence);
                return;
            }

            if (bracket != null && bracket.Length != 2)
            {
                throw new ArgumentException("Invalid bracket");
            }

            openingBracket = bracket[0].ToString();
            closingBracket = bracket[1].ToString();

            count = source.Count();
            if (count == 1)
            {
                builder.AppendFormat($"{leadingMessage}{openingBracket}{source.First()?.ToString()}{closingBracket}");
                builder.ToString().Print(w, newLineAfterSequence);
                return;
            }

            mutated = source.Take(count - 1)
                .Select(item => string.Format($"{item.ToString()}{separator}"))
                .Concat(Enumerable.Repeat(source.Last().ToString(), 1));

            var s = string.Concat(leadingMessage, openingBracket,
                mutated.Aggregate((current, next) => string.Format($"{current}{next}")),
                closingBracket,
                (newLineAfterSequence ? Environment.NewLine : null));

            s.Print(w, false);
            return;
        }

        public static void Print(this string s, TextWriter writer, bool newLine = true)
        {
            if (s == null) throw new ArgumentNullException("s");

            if (newLine) writer.WriteLine(s); else writer.Write(s);
        }

        public static bool IsOdd(this int n)
        {
            return (n & 1) == 1;
        }

        public static int Find<T>(this IEnumerable<T> source, T elementToSearchFor)
        {
            if (source == null) throw new ArgumentNullException("source");

            var count = source.LongCount();
            if (count == 0) return -1;

            int pos = -1;

            while (++pos < count)
                if (source.ElementAt(pos).Equals(elementToSearchFor))
                    return pos;

            return -1;
        }
        
        public static void RemoveElementAt<T>(this T[] source, int index)
        {
            if (source == null) throw new ArgumentNullException("source");

            var count = source.LongCount();

            if (count == 0) return;

            if (index >= count) throw new IndexOutOfRangeException();

            if (index == count - 1)
            {
                source[count - 1] = default(T);
                return;
            }

            for(int i = index + 1; i < count; i++)
                source[i - 1] = source[i];

            source[count - 1] = default(T);
        }
    }
}