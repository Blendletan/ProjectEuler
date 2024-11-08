namespace Problem17
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            for (Int32 i = 0; i < numberOfCases; i++)
            {
                Int64 input = Int64.Parse(Console.ReadLine());
                if (input == 0)
                {
                    Console.WriteLine("Zero");
                }
                else
                {
                    Console.WriteLine(SpellNumber(input));
                }
            }
        }
        static string SpellNumber(Int64 input)
        {
            if (input < 1000)
            {
                return SpellLessThanThousand(input);
            }
            if (input < 1000000)
            {
                Int64 thousandsPlace = input / 1000;
                Int64 remainder = input % 1000;
                return SpellLessThanThousand(thousandsPlace) + "Thousand " + SpellNumber(remainder);
            }
            if (input < 1000000000)
            {
                Int64 millionsPlace = input / 1000000;
                Int64 remainder = input % 1000000;
                return SpellLessThanThousand(millionsPlace) + "Million " + SpellNumber(remainder);
            }
            if (input < 1000000000000)
            {
                Int64 billionsPlace = input / 1000000000;
                Int64 remainder = input % 1000000000;
                return SpellLessThanThousand(billionsPlace) + "Billion " + SpellNumber(remainder);
            }
            if (input < 1000000000000000)
            {
                Int64 trillionsPlace = input / 1000000000000;
                Int64 remainder = input % 1000000000000;
                return SpellLessThanThousand(trillionsPlace) + "Trillion " + SpellNumber(remainder);
            }
            return "foo";
        }
        static string SpellLessThanThousand(Int64 input)
        {
            if (input < 100)
            {
                return SpellLessThanHundred(input);
            }
            Int64 hundredsPlace = input / 100;
            Int64 remainder = input % 100;
            return SpellDigit(hundredsPlace) + "Hundred " + SpellLessThanHundred(remainder);
        }
        static string SpellLessThanHundred(Int64 input)
        {
            if (input < 10)
            {
                return SpellDigit(input);
            }
            if (input < 20)
            {
                return SpellTeens(input);
            }
            Int64 tensPlace = input / 10;
            Int64 remainder = input % 10;
            return CreateSuffix(tensPlace) + SpellDigit(remainder);
        }
        static String SpellDigit(Int64 input)
        {
            switch (input)
            {
                case 0:
                    return "";
                case 1:
                    return "One ";
                case 2:
                    return "Two ";
                case 3:
                    return "Three ";
                case 4:
                    return "Four ";
                case 5:
                    return "Five ";
                case 6:
                    return "Six ";
                case 7:
                    return "Seven ";
                case 8:
                    return "Eight ";
                case 9:
                    return "Nine ";
                default:
                    return "foo";
            }
        }
        static string SpellTeens(Int64 input)
        {
            switch (input)
            {
                case 10:
                    return "Ten ";
                case 11:
                    return "Eleven ";
                case 12:
                    return "Twelve ";
                case 13:
                    return "Thirteen ";
                case 14:
                    return "Fourteen ";
                case 15:
                    return "Fifteen ";
                case 16:
                    return "Sixteen ";
                case 17:
                    return "Seventeen ";
                case 18:
                    return "Eighteen ";
                case 19:
                    return "Nineteen ";
                default:
                    return "foo";
            }
        }
        static string CreateSuffix(Int64 input)
        {
            switch (input)
            {
                case 2:
                    return "Twenty ";
                case 3:
                    return "Thirty ";
                case 4:
                    return "Forty ";
                case 5:
                    return "Fifty ";
                case 6:
                    return "Sixty ";
                case 7:
                    return "Seventy ";
                case 8:
                    return "Eighty ";
                case 9:
                    return "Ninety ";
                default:
                    return "foo";
            }
        }
    }
}
