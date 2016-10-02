using System;

// Hacker Rank challenge here: https://www.hackerrank.com/challenges/library-fine

namespace HackerRank.Assignment.LibraryFine
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] tokens_d1 = Console.ReadLine().Split(' ');
            int d1 = Convert.ToInt32(tokens_d1[0]);
            int m1 = Convert.ToInt32(tokens_d1[1]);
            int y1 = Convert.ToInt32(tokens_d1[2]);
            string[] tokens_d2 = Console.ReadLine().Split(' ');
            int d2 = Convert.ToInt32(tokens_d2[0]);
            int m2 = Convert.ToInt32(tokens_d2[1]);
            int y2 = Convert.ToInt32(tokens_d2[2]);

            var returnDate = new ReturnDate(y1, m1, d1, y2, m2, d2);
            var libraryFine = CalculateLibraryFine(returnDate);
            Console.WriteLine("The fine is: {0}.", libraryFine);
        }

        // Although an int will suffice here as the return type, it is
        // better from the point-of-view of extensibility of this design
        // to have a decimal as the return type.
        private static decimal CalculateLibraryFine(ReturnDate returnDate)
        {
            if (returnDate.ActualDate < returnDate.ExpectedDate)
                return 0m;

            if (returnDate.ActualYear > returnDate.ExpectedYear)
                return 10000m;

            if (returnDate.ActualMonth > returnDate.ExpectedMonth)
                return 500m * (returnDate.ActualMonth - returnDate.ExpectedMonth);

            return 15m * (returnDate.ActualDay - returnDate.ExpectedDay);
        }
    }

    public struct ReturnDate
    {
        // Date part validations such as the date range, month rage
        // etc. are taken care of by the System.DateTime constructor
        // The only validation we need to do it that of the year range
        public ReturnDate(int actualYear, int actualMonth, int actualDay,
            int expectedYear, int expectedMonth, int expectedDay) :
                this(new DateTime(actualYear, actualMonth, actualDay),
                    new DateTime(expectedYear, expectedMonth, expectedDay))
        {
        }

        public ReturnDate(DateTime actualDate, DateTime expectedDate)
        {
            if (actualDate.Year < 1 || actualDate.Year > 3000)
                throw new ArgumentException("Actual return date must have a year between 1 and 3000.");

            if (expectedDate.Year < 1 || expectedDate.Year > 3000)
                throw new ArgumentException("Expected return date must have a year between 1 and 3000.");

            ActualDate = actualDate;
            ActualYear = actualDate.Year;
            ActualMonth = actualDate.Month;
            ActualDay = actualDate.Day;

            ExpectedDate = expectedDate;
            ExpectedYear = expectedDate.Year;
            ExpectedMonth = expectedDate.Month;
            ExpectedDay = expectedDate.Day;
        }

        public DateTime ActualDate { get; private set; }
        public DateTime ExpectedDate { get; private set; }

        public int ActualYear { get; private set; }
        public int ExpectedYear { get; private set; }

        public int ActualMonth { get; private  set; }
        public int ExpectedMonth { get; private set; }

        public int ActualDay { get; private set; }
        public int ExpectedDay { get; private set; }
    }
}