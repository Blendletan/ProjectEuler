namespace Problem19
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                (Int64, Int32, Int32) startDay = ParseInput();
                (Int64, Int32, Int32) endDay = ParseInput();
                Int64 output = HowManySundays(startDay.Item1, startDay.Item2, startDay.Item3, endDay.Item1, endDay.Item2);
                Console.WriteLine(output);
            }
        }
        static (Int64, Int32, Int32) ParseInput()
        {
            string[] yearMonthDay = Console.ReadLine().Split();
            Int64 year = Int64.Parse(yearMonthDay[0]);
            Int32 month = Int32.Parse(yearMonthDay[1]);
            Int32 day = Int32.Parse(yearMonthDay[2]);
            return (year, month, day);
        }
        static Int64 HowManySundays(Int64 firstYear, Int32 month, Int32 firstDay, Int64 secondYear, Int64 secondMonth)
        {
            Int64 sundays = 0;
            if (firstDay > 1)
            {
                month++;
                if (month == 13)
                {
                    month = 1;
                    firstYear++;
                }
            }
            if (firstYear == secondYear)
            {
                for (Int32 m = month; m <= secondMonth; m++)
                {
                    if (IsSunday(firstYear, m)) { sundays++; }
                }
                return sundays;
            }
            for (Int32 i = month; i <= 12; i++)
            {
                if (IsSunday(firstYear, i)) { sundays++; }
            }
            for (Int64 y = firstYear + 1; y < secondYear; y++)
            {
                for (Int32 m = 1; m <= 12; m++)
                {
                    if (IsSunday(y, m)) { sundays++; }
                }
            }
            for (Int32 m = 1; m <= secondMonth; m++)
            {
                if (IsSunday(secondYear, m)) { sundays++; }
            }
            return sundays;
        }
        static bool IsSunday(Int64 year, Int32 month)
        {
            Int32 adjustedYear = (Int32)(year % 400);
            adjustedYear += 2000;
            DateOnly d = new DateOnly(adjustedYear, month, 1);
            if (d.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }
            return false;
        }
    }
}
