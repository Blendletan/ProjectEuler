using System.Text;

namespace Problem89
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Int32 numberOfCases = Int32.Parse(Console.ReadLine());
            for(Int32 i = 0; i < numberOfCases; i++)
            {
                string input = Console.ReadLine();
                Int32 number = ParseInput(input);
                Console.WriteLine(FormatAsRomanNumeral(number));
            }
        }
        static Int32 GetValueOfDigit(char input)
        {
            switch (input)
            {
                case 'I':
                    return 1;
                case 'V':
                    return 5;
                case 'X':
                    return 10;
                case 'L':
                    return 50;
                case 'C':
                    return 100;
                case 'D':
                    return 500;
                case 'M':
                    return 1000;
                default:
                    throw new Exception("Invalid roman numeral");
            }
        }
        static Int32 ParseInput(string input)
        {
            char[] inputChars = input.ToCharArray();
            Int32 output = 0;
            foreach(var v in inputChars)
            {
                output += GetValueOfDigit(v);
            }
            return output;
        }
        static string FormatAsRomanNumeral(Int32 input)
        {
            if (input == 0)
            {
                return String.Empty;
            }
            if (input == 1)
            {
                return "I";
            }
            if (input == 2)
            {
                return "II";
            }
            if (input == 3)
            {
                return "III";
            }
            if (input == 4)
            {
                return "IV";
            }
            if (input == 5)
            {
                return "V";
            }
            if (input < 9)
            {
                return "V" + FormatAsRomanNumeral(input - 5);
            }
            if (input == 9)
            {
                return "IX";
            }
            if (input == 10)
            {
                return "X";
            }
            if (input < 40)
            {
                Int32 tensPlace = input / 10;
                Int32 remainder = input % 10;
                StringBuilder s = new StringBuilder();
                for(Int32 i = 0; i < tensPlace; i++)
                {
                    s.Append("X");
                }
                return s.ToString() + FormatAsRomanNumeral(remainder);
            }
            if (input < 50)
            {
                return "XL" + FormatAsRomanNumeral(input % 10);
            }
            if (input < 90)
            {
                return "L" + FormatAsRomanNumeral(input % 50);
            }
            if (input < 100)
            {
                return "XC" + FormatAsRomanNumeral(input % 10);
            }
            if (input < 400)
            {
                Int32 hundredsPlace = input / 100;
                Int32 remainder = input % 100;
                StringBuilder s = new StringBuilder();
                for (Int32 i = 0; i < hundredsPlace; i++)
                {
                    s.Append("C");
                }
                return s.ToString() + FormatAsRomanNumeral(remainder);
            }
            if (input < 500)
            {
                return "CD" + FormatAsRomanNumeral(input % 100);
            }
            if (input < 900)
            {
                return "D"+ FormatAsRomanNumeral(input % 500);
            }
            if (input < 1000)
            {
                return "CM" + FormatAsRomanNumeral(input % 100);
            }
            else
            {
                Int32 thousandsPlace = input / 1000;
                Int32 remainder = input % 1000;
                StringBuilder s = new StringBuilder();
                for (Int32 i = 0; i < thousandsPlace; i++)
                {
                    s.Append("M");
                }
                return s.ToString() + FormatAsRomanNumeral(remainder);
            }
        }
    }
}
